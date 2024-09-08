
namespace FinalBoatSystemRental.API.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BoatbookingController : ControllerBase
{

    private readonly IMediator _mediator;

    public BoatbookingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region Owner 
    // Owners
    [HttpGet("OwnerBoatReservation")]
    [ApiExplorerSettings(GroupName = GlobalVariables.Owner)]
    public async Task<IActionResult> GetOwnerBoatReservation()
    {
        try
        {
            var userId = User.FindFirstValue("uid");

            if (userId == null)
            {
                return Unauthorized();
            }
            Log.Information($"{userId} Get all  Boat Reservation");
            var trip = new ListBoatBookingOwnerQuery(userId);
            var trips = await _mediator.Send(trip);
            return Ok(trips);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return BadRequest(ex.Message);
        }


    }
    // Owners
    [HttpGet("OwnerCanceledBoatReservation")]
    [ApiExplorerSettings(GroupName = GlobalVariables.Owner)]
    public async Task<IActionResult> GetCanceledOwnerBoatReservation()
    {
        try
        {
            var userId = User.FindFirstValue("uid");

            if (userId == null)
            {
                return Unauthorized();
            }
            Log.Information($"{userId} Get all Canceled Boat Reservation");

            var trip = new ListCanceledBoatBookingOwnersQuery(userId);
            var trips = await _mediator.Send(trip);
            return Ok(trips);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex.Message);
            return BadRequest(ex.Message);
        }


    }
    #endregion

    #region Customer
    //Customer
    [HttpGet("BoatReservationHistory")]
    [ApiExplorerSettings(GroupName = GlobalVariables.Customer)]
    public async Task<IActionResult> GetBoatReservationCustomerHistory()
    {
        try
        {
            var userId = User.FindFirstValue("uid");

            if (userId == null)
            {
                return Unauthorized();
            }

            Log.Information($"{userId} View Boat Reservation History");

            var trip = new ListBoatBookingHistoryQuery(userId);
            var trips = await _mediator.Send(trip);
            return Ok(trips);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex.Message);
            return BadRequest(ex.Message);
        }


    }

    //Customer
    [HttpPost]
    [ApiExplorerSettings(GroupName = GlobalVariables.Customer)]
    public async Task<IActionResult> Add([FromBody] AddBoatBookingCommand command)
    {
        try
        {
            var userId = User.FindFirstValue("uid");

            if (userId == null)
            {
                return Unauthorized();
            }
            Log.Information($"{userId} Book a boat");
            if (command == null)
                return BadRequest("Boat Booking Data is required");
            command.UserId = userId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return BadRequest(ex.Message);
        }

    }
    #endregion


}
