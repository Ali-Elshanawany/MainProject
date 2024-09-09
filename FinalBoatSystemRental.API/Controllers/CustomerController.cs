
namespace FinalBoatSystemRental.API.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = GlobalVariables.Customer)]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }



    [HttpGet("View-Profile")]
    public async Task<IActionResult> GetCustomersDetails(GetCustomerDetailsQuery query)
    {
        try
        {
            var userId = User.FindFirstValue("uid");

            if (userId == null)
            {
                return Unauthorized();
            }
            Log.Information($"{userId} View Profile");
            var customer = new GetCustomerDetailsQuery(userId);
            var result = await _mediator.Send(customer);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex.Message);
            return BadRequest(ex.Message);
        }

    }



    [HttpPut("Update-Profile")]
    public async Task<IActionResult> UpdateCustomerDetails(UpdateCustomerDetailsCommand command)
    {
        try
        {
            var userId = User.FindFirstValue("uid");

            if (userId == null)
            {
                return Unauthorized();
            }
            Log.Information($"{userId} Update Profile");
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

    [HttpPut("Add-WalletBalance")]
    public async Task<IActionResult> UpdateOwnerBalance(UpdateCustomerWalletCommand command)
    {
        try
        {
            var userId = User.FindFirstValue("uid");

            if (userId == null)
            {
                return Unauthorized();
            }
            Log.Information($"{userId} Add Balance");
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
