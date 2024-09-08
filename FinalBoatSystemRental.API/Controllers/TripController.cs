
namespace FinalBoatSystemRental.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TripController : ControllerBase
{

    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;


    public TripController(IMediator mediator, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _mapper = mapper;
        _userManager = userManager;
    }



    #region Customer
    // Customer
    [HttpGet("AvailableTrips")]
    [ApiExplorerSettings(GroupName = GlobalVariables.Customer)]
    public async Task<IActionResult> GetAllAvailableBoats()
    {
        try
        {
            var trip = new ListAvailableTripQuery();
            var result = await _mediator.Send(trip);
            Log.Information("View Available Trips");
            return Ok(result);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex.Message);
            return BadRequest(ex.Message);
        }

    }

    #endregion


    #region Owner

    // Owner Return All Owner Trips
    [HttpGet]
    [ApiExplorerSettings(GroupName = GlobalVariables.Owner)]
    public async Task<IActionResult> GetAllTrips()
    {
        try
        {
            var userId = User.FindFirstValue("uid");

            if (userId == null)
            {
                return Unauthorized();
            }

            Log.Information($"{userId} View Trips");

            var boat = new ListTripQuery(userId);
            var boats = await _mediator.Send(boat);
            return Ok(boats);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    // Owner Return Specific trip
    [HttpGet("{tripId}")]
    [ApiExplorerSettings(GroupName = GlobalVariables.Owner)]
    public async Task<IActionResult> GetTrip(int tripId)
    {
        try
        {
            var userId = User.FindFirstValue("uid");

            if (userId == null)
            {
                return Unauthorized();
            }

            Log.Information($"{userId} view Trip ");

            var trip = new GetTripQuery(tripId, userId);
            var tripView = await _mediator.Send(trip);
            return Ok(tripView);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    //Owner
    [HttpPost]
    [ApiExplorerSettings(GroupName = GlobalVariables.Owner)]
    public async Task<IActionResult> Add([FromBody] AddTripCommand? command)
    {

        try
        {
            var userId = User.FindFirstValue("uid");

            if (userId == null)
            {
                return Unauthorized();
            }

            Log.Information($"{userId} Add Trip");

            if (command == null)
                return BadRequest("Trip Data is required");
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

    #endregion


}
