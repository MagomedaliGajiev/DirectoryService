using DirectoryService.Application.Models;
using DirectoryService.Contracts.Locations.Dtos;
using MediatR;

namespace DirectoryService.Contracts.Locations.Commands.CreateLocation;

public class CreateLocationCommand : IRequest<OperationResult<LocationDto>>
{
    public string Name { get; set; } = null!;

    public AddressModel Address { get; set; } = null!;

    public string Timezone { get; set; } = null!;
}
