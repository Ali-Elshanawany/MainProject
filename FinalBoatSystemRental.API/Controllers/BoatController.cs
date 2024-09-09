namespace FinalBoatSystemRental.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BoatController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;



    public BoatController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;

    }


    #region Owner
    // Owner
    [HttpGet("View-Boats")]
    [ApiExplorerSettings(GroupName = GlobalVariables.Owner)]
    public async Task<IActionResult> GetAllBoats()
    {
        try
        {
            var userId = User.FindFirstValue("uid");

            if (userId == null)
            {
                return Unauthorized();
            }
            Log.Information($"{userId} View boats");

            var boat = new ListBoatsQuery(userId);
            var boats = await _mediator.Send(boat);
            return Ok(boats);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex.Message);
            return BadRequest(ex.Message);
        }
    }


    // Owner Get specific Owner Boat
    [HttpGet("GetBoatById{id}")]
    [ApiExplorerSettings(GroupName = GlobalVariables.Owner)]
    public async Task<IActionResult> GetBoat(int id)
    {
        try
        {
            var userId = User.FindFirstValue("uid");

            if (userId == null)
            {
                return Unauthorized();
            }

            Log.Information($"{userId} View Boat info ");

            var boat = new GetBoatQuery(id, userId);
            var boats = await _mediator.Send(boat);
            return Ok(boats);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex.Message);
            return BadRequest(ex.Message);
        }
    }
    //Owner
    [HttpPost("Add-Boat")]
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


    //Owner
    [HttpDelete("Delete-Boat")]
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
    #endregion

    #region Customer
    // Customer
    [HttpGet("View-AvailableBoats")]
    [ApiExplorerSettings(GroupName = GlobalVariables.Customer)]

    public async Task<IActionResult> GetAllAvailableBoats()
    {

        try
        {
            Log.Information("Customer View Available Boats");
            var boat = new ListAvailableBoatsQuery();
            var boats = await _mediator.Send(boat);
            return Ok(boats);

        }
        catch (Exception ex)
        {
            Log.Fatal(ex.Message);
            return BadRequest(ex.Message);
        }
    }
    #endregion


    #region Admin 
    [HttpPost("Approve-Boat")]
    [ApiExplorerSettings(GroupName = GlobalVariables.Admin)]
    public async Task<IActionResult> ApproveBoat(UpdateBoatStatusCommand command)
    {
        try
        {
            if (!User.IsInRole(GlobalVariables.Admin))
            {
                throw new UnauthorizedAccessException("You can't Access this Action");
            }

            Log.Information("Approve Boat");
            if (command == null)
                return BadRequest("Boat Data is required");

            var result = await _mediator.Send(command);

            return Ok(result);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex.Message);
            return BadRequest(ex.Message);
        }


    }

    [HttpGet("View-PendingBoats")]
    [ApiExplorerSettings(GroupName = GlobalVariables.Admin)]

    public async Task<IActionResult> GetAllPendingBoats()
    {

        try
        {
            Log.Information("Admin View Available Boats");
            var boat = new ListPendingBoatsQuery();
            var boats = await _mediator.Send(boat);
            return Ok(boats);

        }
        catch (Exception ex)
        {
            Log.Fatal(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    #endregion

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





}



