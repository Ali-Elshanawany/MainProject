﻿

namespace FinalBoatSystemRental.API.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CancellationController : ControllerBase
{

    private readonly IMediator _mediator;

    public CancellationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region Customer
    // Customer
    [HttpPost("Trip")]
    [ApiExplorerSettings(GroupName = GlobalVariables.Customer)]
    public async Task<IActionResult> AddTripCancellation([FromBody] AddCancellationTripCommand command)
    {
        try
        {

            if (command == null)
                return BadRequest(" Data is required");
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    // Customer
    [HttpPost("Boat")]
    [ApiExplorerSettings(GroupName = GlobalVariables.Customer)]

    public async Task<IActionResult> AddBoatCancellation([FromBody] AddCancellationBoatCommand command)
    {
        try
        {
            if (command == null)
                return BadRequest(" Data is required");
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    #endregion



}
