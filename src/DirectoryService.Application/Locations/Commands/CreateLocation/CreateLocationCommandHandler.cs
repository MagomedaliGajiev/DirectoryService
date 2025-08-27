using CSharpFunctionalExtensions;
using DirectoryService.Application.Extensions;
using DirectoryService.Application.Locations.Interfaces;
using DirectoryService.Contracts.Locations.Commands.CreateLocation;
using DirectoryService.Contracts.Locations.Dtos;
using DirectoryService.Domain.Locations;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared;
using TimeZone = DirectoryService.Domain.Locations.TimeZone;

namespace DirectoryService.Application.Locations.Commands.CreateLocation;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Result<LocationDto, Errors>>
{
    private readonly ILocationRepository _locationRepository;
    private readonly IValidator<CreateLocationCommand> _validator;
    private readonly ILogger<CreateLocationCommandHandler> _logger;

    public CreateLocationCommandHandler(
        ILocationRepository locationRepository,
        IValidator<CreateLocationCommand> validator,
        ILogger<CreateLocationCommandHandler> logger)
    {
        _locationRepository = locationRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<LocationDto, Errors>> Handle(
        CreateLocationCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        try
        {
            var nameResult = LocationName.Create(request.Name);

            var timezoneResult = TimeZone.Create(request.Timezone);

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
                return saveResult.Error.ToErrors();
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

            return locationDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error creating location");
            return GeneralErrors.Failure("An unexpected error occurred").ToErrors();
        }
    }
}
