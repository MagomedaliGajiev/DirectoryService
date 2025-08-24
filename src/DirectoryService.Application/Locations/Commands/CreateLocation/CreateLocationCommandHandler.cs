using DirectoryService.Application.Locations.Interfaces;
using DirectoryService.Application.Models;
using DirectoryService.Contracts.Locations.Commands.CreateLocation;
using DirectoryService.Contracts.Locations.Dtos;
using DirectoryService.Domain.Locations;
using DirectoryService.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using TimeZone = DirectoryService.Domain.Locations.TimeZone;

namespace DirectoryService.Application.Locations.Commands.CreateLocation;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, OperationResult<LocationDto>>
{
    private readonly ILocationRepository _locationRepository;
    private readonly ILogger<CreateLocationCommandHandler> _logger;

    public CreateLocationCommandHandler(
        ILocationRepository locationRepository,
        ILogger<CreateLocationCommandHandler> logger)
    {
        _locationRepository = locationRepository;
        _logger = logger;
    }

    public async Task<OperationResult<LocationDto>> Handle(
        CreateLocationCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var nameResult = LocationName.Create(request.Name);
            if (nameResult.IsFailure)
                return OperationResult<LocationDto>.Failure(nameResult.Error);

            var timezoneResult = TimeZone.Create(request.Timezone);
            if (timezoneResult.IsFailure)
                return OperationResult<LocationDto>.Failure(timezoneResult.Error);

            var address = new Address(
                request.Address.City,
                request.Address.Street,
                request.Address.HouseNumber,
                request.Address.Number);

            var location = new Location(
                nameResult.Value,
                timezoneResult.Value,
                address);

            await _locationRepository.AddAsync(location, cancellationToken);
            var saveResult = await _locationRepository.SaveChangesAsync(cancellationToken);

            if (saveResult.IsFailure)
            {
                _logger.LogError("Failed to save location: {Error}", saveResult.Error);
                return OperationResult<LocationDto>.Failure(saveResult.Error);
            }

            var locationDto = new LocationDto
            {
                Id = location.Id.Value,
                Name = location.Name.Value,
                Address = new AddressDto
                {
                    City = location.Addresses.First().City,
                    Street = location.Addresses.First().Street,
                    HouseNumber = location.Addresses.First().HouseNumber,
                    Number = location.Addresses.First().Number,
                },
                Timezone = location.TimeZone.Value,
                IsActive = location.IsActive,
                CreatedAt = location.CreatedAt,
                UpdatedAt = location.UpdatedAt,
            };

            return OperationResult<LocationDto>.Success(locationDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error creating location");
            return OperationResult<LocationDto>.Failure(
                Error.Failure("location.unexpected", "An unexpected error occurred"));
        }
    }
}
