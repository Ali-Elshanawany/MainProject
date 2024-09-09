
namespace FinalBoatSystemRental.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = GlobalVariables.Owner)]
    public class OwnerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OwnerController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // Owner
        [HttpGet("View-Profile")]
        public async Task<IActionResult> GetOwner()
        {
            try
            {
                var userId = User.FindFirstValue("uid");

                if (userId == null)
                {
                    return Unauthorized();
                }
                Log.Information($"{userId} Get Details ");
                var owner = new GetOwnerDetailsQuery(userId);
                var result = await _mediator.Send(owner);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        //Owner
        [HttpPost("PayRefund")]
        public async Task<IActionResult> PayRefund(PayRefundCommand command)
        {
            try
            {
                var userId = User.FindFirstValue("uid");

                if (userId == null)
                {
                    return Unauthorized();
                }

                Log.Information($"{userId} Pay Refund");

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

        // Owner
        [HttpPut("Update-Profile")]
        public async Task<IActionResult> UpdateOwnerDetails(UpdateOwnerDetailsCommand command)
        {
            try
            {
                var userId = User.FindFirstValue("uid");

                if (userId == null)
                {
                    return Unauthorized();
                }
                Log.Information($"Update owner details {userId}");
                command.UserId = userId;
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception e)
            {
                Log.Fatal(e.Message);
                return BadRequest(e.Message);
            }
        }
        //Owner
        [HttpPut("Add-WalletBalance")]
        public async Task<IActionResult> UpdateOwnerBalance(UpdateOwnerWalletCommand command)
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
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest($"{ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
                return BadRequest(ex.Message);
            }


        }

    }
}
