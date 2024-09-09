

namespace FinalBoatSystemRental.API.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CancellationController : ControllerBase
{

    private readonly IMediator _mediator;

    public CancellationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region Customer
    // Customer
    [HttpPost("Cancel-TripReservation")]
    [ApiExplorerSettings(GroupName = GlobalVariables.Customer)]
    public async Task<IActionResult> AddTripCancellation([FromBody] AddCancellationTripCommand command)
    {
        try
        {

            if (command == null)
                return BadRequest(" Data is required");

            Log.Information("Customer Cancel A trip");
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex.Message);
            return BadRequest(ex.Message);
        }

    }
    // Customer
    [HttpPost("Cancel-BoatReservation")]
    [ApiExplorerSettings(GroupName = GlobalVariables.Customer)]

    public async Task<IActionResult> AddBoatCancellation([FromBody] AddCancellationBoatCommand command)
    {
        try
        {
            if (command == null)
                return BadRequest(" Data is required");
            Log.Information("Customer Cancel A Boat");
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex.Message);
            return BadRequest(ex.Message);
        }

    }
    #endregion



}
