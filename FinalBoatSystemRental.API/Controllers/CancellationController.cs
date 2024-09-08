

namespace FinalBoatSystemRental.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CancellationController : ControllerBase
{

    private readonly IMediator _mediator;

    public CancellationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Trip")]
    public async Task<IActionResult> AddTripCancellation([FromBody] AddCancellationTripCommand command)
    {
        try
        {

            if (command == null)
                return BadRequest(" Data is required");
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPost("Boat")]
    public async Task<IActionResult> AddBoatCancellation([FromBody] AddCancellationBoatCommand command)
    {
        try
        {
            if (command == null)
                return BadRequest(" Data is required");
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

}
