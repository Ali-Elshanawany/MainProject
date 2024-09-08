
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



    [HttpGet]
    public async Task<IActionResult> GetCustomersDetails(GetCustomerDetailsQuery query)
    {
        var userId = User.FindFirstValue("uid");

        if (userId == null)
        {
            return Unauthorized();
        }
        var customer = new GetCustomerDetailsQuery(userId);
        var result = await _mediator.Send(customer);
        return Ok(result);

    }



    [HttpPut("Details")]
    public async Task<IActionResult> UpdateCustomerDetails(UpdateCustomerDetailsCommand command)
    {
        var userId = User.FindFirstValue("uid");

        if (userId == null)
        {
            return Unauthorized();
        }
        command.UserId = userId;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("Wallet")]
    public async Task<IActionResult> UpdateOwnerBalance(UpdateCustomerWalletCommand command)
    {
        try
        {
            var userId = User.FindFirstValue("uid");

            if (userId == null)
            {
                return Unauthorized();
            }
            command.UserId = userId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

}
