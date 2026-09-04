using EnterpriseManagement.Application.DTOs;
using EnterpriseManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseManagement.Api.Controllers;

[ApiController]
[Route("api/leave-balances")]
public class LeaveBalancesController : ControllerBase
{
    private readonly ILeaveBalanceService _leaveBalanceService;

    public LeaveBalancesController(ILeaveBalanceService leaveBalanceService)
    {
        _leaveBalanceService = leaveBalanceService;
    }

    [HttpGet("{employeeCode}")]
    public async Task<ActionResult<IEnumerable<LeaveBalanceDto>>> GetByEmployee(string employeeCode, [FromQuery] int year)
    {
        try
        {
            var balances = await _leaveBalanceService.GetByEmployeeAsync(employeeCode, year);
            return Ok(balances);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPut]
    public async Task<ActionResult<LeaveBalanceDto>> SetAllocatedDays(SetLeaveBalanceRequest request)
    {
        try
        {
            var result = await _leaveBalanceService.SetAllocatedDaysAsync(request);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
