



namespace FinalBoatSystemRental.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BoatController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;


    public BoatController(IMediator mediator, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _mapper = mapper;
        _userManager = userManager;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllBoats()
    {
        var userId = User.FindFirstValue("uid");

        if (userId == null)
        {
            return Unauthorized();
        }

        var boat = new ListBoatsQuery(userId);
        var boats = await _mediator.Send(boat);
        return Ok(boats);
    }

    [HttpGet("AvailableBoats")]
    public async Task<IActionResult> GetAllAvailableBoats()
    {

        var boat = new ListAvailableBoatsQuery();
        var boats = await _mediator.Send(boat);
        return Ok(boats);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBoat(int id)
    {
        var userId = User.FindFirstValue("uid");

        if (userId == null)
        {
            return Unauthorized();
        }
        var boat = new GetBoatQuery(id, userId);
        var boats = await _mediator.Send(boat);
        return Ok(boats);
    }
    //Owner
    [HttpPost]
    [ApiExplorerSettings(GroupName = GlobalVariables.Owner)]
    public async Task<IActionResult> Add([FromBody] AddBoatCommand command)
    {
        try
        {
            var userId = User.FindFirstValue("uid");
            Log.Information($"{userId} Adding Boat");
            if (userId == null)
            {
                return Unauthorized();
            }
            if (command == null)
                return BadRequest("Boat Data is required");
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

    #region StopUpdateBoatForNow
    //[HttpPut]
    //public async Task<IActionResult> Update(UpdateBoatCommand command)
    //{
    //   var result= await _mediator.Send(command);
    //    if (result == null)
    //        return BadRequest("Something Went Wrong Please Try Later");
    //    return Ok(result);  
    //}
    #endregion 

    //Owner
    [HttpDelete]
    [ApiExplorerSettings(GroupName = GlobalVariables.Owner)]
    public async Task<IActionResult> Delete(DeleteBoatCommand command)
    {
        try
        {

            var userId = User.FindFirstValue("uid");
            if (userId == null)
            {
                return Unauthorized();
            }
            Log.Information($"{userId} Delete Boat ");
            if (command == null)
                return BadRequest("Boat Data is required");
            command.UserId = userId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Log.Fatal($"{ex.Message}");
            return BadRequest(ex.Message);
        }
    }




}



