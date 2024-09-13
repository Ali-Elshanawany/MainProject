
namespace FinalBoatSystemRental.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AdditionController : ControllerBase
{
    private readonly IMediator _mediator;


    public AdditionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //Owner
    [ApiExplorerSettings(GroupName = GlobalVariables.Owner)]
    [HttpPost("Add")]
    public async Task<IActionResult> Add(AddAdditionCommand command)
    {
        try
        {
            var userId = User.FindFirstValue("uid");
            if (userId == null)
                return Unauthorized("Can't Access This Data ");

            Log.Information($"{userId} Add Addition");

            if (command == null)
                return BadRequest("Data is Required");

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


}
