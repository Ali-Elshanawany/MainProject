
namespace FinalBoatSystemRental.API.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ReservationController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReservationController(IMediator mediator)
    {
        _mediator = mediator;
    }



    #region Customer
    // Customer
    [HttpPost]
    [ApiExplorerSettings(GroupName = GlobalVariables.Customer)]
    public async Task<IActionResult> Add([FromBody] AddReservationCommand command)
    {
        try
        {
            var userId = User.FindFirstValue("uid");

            if (userId == null)
            {
                return Unauthorized();
            }

            Log.Information($"{userId} book a trip");

            if (command == null)
                return BadRequest("Boat Booking Data is required");
            command.UserId = userId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex.Message);
            return BadRequest(ex.Message);
        }

    }

    //Customer
    [HttpGet("TripHistory")]
    [ApiExplorerSettings(GroupName = GlobalVariables.Customer)]
    public async Task<IActionResult> GetAllBoats()
    {
        try
        {
            var userId = User.FindFirstValue("uid");

            if (userId == null)
            {
                return Unauthorized();
            }

            Log.Information($"{userId} View Trip History");

            var trip = new ListReservationCustomerHistoryQuery(userId);
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


    #region Owner
    //Owner
    [HttpGet("OwnerReservations")]
    [ApiExplorerSettings(GroupName = GlobalVariables.Owner)]
    public async Task<IActionResult> GetOwnerReservations()
    {
        try
        {
            var userId = User.FindFirstValue("uid");

            if (userId == null)
            {
                return Unauthorized();
            }
            Log.Information($"{userId} Get all Trip reservation");
            var trip = new ListOwnerReservationQuery(userId);
            var trips = await _mediator.Send(trip);
            return Ok(trips);
        }
        catch (Exception e)
        {
            Log.Fatal(e.Message);
            return BadRequest(e.Message);
        }
    }
    //Owner
    [HttpGet("OwnerCanceledReservations")]
    [ApiExplorerSettings(GroupName = GlobalVariables.Owner)]
    public async Task<IActionResult> GetOwnerCanceledReservations()
    {
        try
        {
            var userId = User.FindFirstValue("uid");

            if (userId == null)
            {
                return Unauthorized();
            }

            Log.Information($"{userId} get all canceled Tips");

            var trip = new ListCanceledOwnerReservationQuery(userId);
            var trips = await _mediator.Send(trip);
            return Ok(trips);
        }
        catch (Exception e)
        {
            Log.Fatal(e.Message);
            return BadRequest(e.Message);
        }
    }

    #endregion



}
