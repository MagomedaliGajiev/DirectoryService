using DirectoryService.Contracts.Locations.Commands.CreateLocation;
using DirectoryService.Contracts.Locations.Dtos;
using DirectoryService.WebApi.EndpointResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.WebApi.Controllers;

[ApiController]
[Route("api/locations")]
public class LocationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LocationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<EndpointResult<LocationDto>> CreateLocation(
        [FromBody] CreateLocationCommand command,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(command, cancellationToken);
    }
}
