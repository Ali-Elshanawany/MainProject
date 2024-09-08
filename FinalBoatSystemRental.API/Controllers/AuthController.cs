namespace FinalBoatSystemRental.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMediator _mediator;

    public AuthController(IAuthService authService, IMediator mediator)
    {
        _authService = authService;
        _mediator = mediator;
    }

    [ApiExplorerSettings(GroupName = GlobalVariables.Owner)]
    // Owner
    [HttpPost("Register-Owner")]
    public async Task<IActionResult> RegisterOwner(RegisterOwnerCommand command)
    {
        try
        {
            Log.Information("Accessed Register-Owner Function");
            var result = await _mediator.Send(command);
            if (result is null)
                return BadRequest(result.Message);


            if (result.Message == GlobalVariables.EmailIsRegistered || result.Message == GlobalVariables.UserNameIsRegistered)
                return BadRequest(result.Message);

            if (!ModelState.IsValid)
            {
                return BadRequest("error");
            }


            return Ok(result.Message);
        }
        catch (Exception ex)
        {
            Log.Error($"{ex.Message}");
            return BadRequest(ex.Message);
        }

    }



    [HttpPost("verify-owner")]
    public async Task<IActionResult> VerifyOwner(VerifyOwnerCommand command)
    {
        var result = await _mediator.Send(command);
        if (result == null)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.IsSuccess);
    }

    //All Owner-Customer-Admin
    [HttpPost("Login")]
    public async Task<IActionResult> login([FromBody] LoginCommand command)
    {
        try
        {
            Log.Information("Accessed Login Action");
            var result = await _mediator.Send(command);
            if (result is null)
                return BadRequest(result.Message);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);

        }
        catch (Exception ex)
        {
            Log.Error($"{ex.Message}");
            return BadRequest(ex.Message);
        }



    }


    // Customer
    [HttpPost("Register-Customer")]
    [ApiExplorerSettings(GroupName = GlobalVariables.Customer)]
    public async Task<IActionResult> RegisterCustomer(RegisterCustomerCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            if (result is null)
                return BadRequest(result.Message);

            Log.Information("Register Customer");
            if (result.Message == GlobalVariables.EmailIsRegistered || result.Message == GlobalVariables.UserNameIsRegistered)
                return BadRequest(result.Message);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex.Message);
            return BadRequest(ex.Message);
        }
    }





}
