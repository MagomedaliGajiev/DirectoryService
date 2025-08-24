using DirectoryService.Contracts.Locations.Commands.CreateLocation;
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
    public async Task<IActionResult> CreateLocation(
        [FromBody] CreateLocationCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }
}
