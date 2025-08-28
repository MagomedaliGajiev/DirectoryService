using CSharpFunctionalExtensions;
using DirectoryService.Contracts.Locations.Dtos;
using MediatR;
using Shared;

namespace DirectoryService.Contracts.Locations.Commands.CreateLocation;

public class CreateLocationCommand : IRequest<Result<LocationDto, Errors>>
{
    public string Name { get; set; } = null!;

    public AddressDto Address { get; set; } = null!;

    public string Timezone { get; set; } = null!;
}
