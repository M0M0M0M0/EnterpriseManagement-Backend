using EnterpriseManagement.Application.DTOs;
using EnterpriseManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseManagement.Api.Controllers;

[ApiController]
[Route("api/leave-requests")]
[Authorize]
public class LeaveRequestsController : ControllerBase
{
    private readonly ILeaveRequestService _leaveRequestService;
    private readonly ICurrentUserService _currentUserService;

    public LeaveRequestsController(ILeaveRequestService leaveRequestService, ICurrentUserService currentUserService)
    {
        _leaveRequestService = leaveRequestService;
        _currentUserService = currentUserService;
    }

    [HttpPost]
    [Authorize(Roles = "EMPLOYEE")]
    public async Task<ActionResult<LeaveRequestDto>> Submit(SubmitLeaveRequest request)
    {
        try
        {
            var result = await _leaveRequestService.SubmitAsync(request, _currentUserService.EmployeeCode!);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("mine")]
    [Authorize(Roles = "EMPLOYEE")]
    public async Task<ActionResult<IEnumerable<LeaveRequestDto>>> GetMine()
    {
        var requests = await _leaveRequestService.GetByEmployeeAsync(_currentUserService.EmployeeCode!);
        return Ok(requests);
    }

    [HttpPut("{id}/cancel")]
    [Authorize(Roles = "EMPLOYEE")]
    public async Task<ActionResult<LeaveRequestDto>> Cancel(long id)
    {
        try
        {
            var result = await _leaveRequestService.CancelAsync(id, _currentUserService.EmployeeCode!);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("pending")]
    [Authorize(Roles = "MANAGER,ADMIN")]
    public async Task<ActionResult<IEnumerable<LeaveRequestDto>>> GetPending()
    {
        var pending = await _leaveRequestService.GetPendingAsync();
        return Ok(pending);
    }

    [HttpGet("history")]
    [Authorize(Roles = "MANAGER,ADMIN")]
    public async Task<ActionResult<IEnumerable<LeaveRequestDto>>> GetHistory()
    {
        var history = await _leaveRequestService.GetHistoryAsync();
        return Ok(history);
    }

    [HttpPut("{id}/approve")]
    [Authorize(Roles = "MANAGER,ADMIN")]
    public async Task<ActionResult<LeaveRequestDto>> Approve(long id)
    {
        try
        {
            var result = await _leaveRequestService.ApproveAsync(id, _currentUserService.EmployeeCode!);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}/reject")]
    [Authorize(Roles = "MANAGER,ADMIN")]
    public async Task<ActionResult<LeaveRequestDto>> Reject(long id, RejectLeaveRequest request)
    {
        try
        {
            var result = await _leaveRequestService.RejectAsync(id, _currentUserService.EmployeeCode!, request.RejectionReason);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
