using EnterpriseManagement.Application.DTOs;
using EnterpriseManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseManagement.Api.Controllers;

[ApiController]
[Route("api/leave-requests")]
public class LeaveRequestsController : ControllerBase
{
    private readonly ILeaveRequestService _leaveRequestService;

    public LeaveRequestsController(ILeaveRequestService leaveRequestService)
    {
        _leaveRequestService = leaveRequestService;
    }

    [HttpPost]
    public async Task<ActionResult<LeaveRequestDto>> Submit(SubmitLeaveRequest request)
    {
        try
        {
            var result = await _leaveRequestService.SubmitAsync(request);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("mine")]
    public async Task<ActionResult<IEnumerable<LeaveRequestDto>>> GetMine([FromQuery] string employeeCode)
    {
        try
        {
            var requests = await _leaveRequestService.GetByEmployeeAsync(employeeCode);
            return Ok(requests);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPut("{id}/cancel")]
    public async Task<ActionResult<LeaveRequestDto>> Cancel(long id, CancelLeaveRequest request)
    {
        try
        {
            var result = await _leaveRequestService.CancelAsync(id, request.EmployeeCode);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("pending")]
    public async Task<ActionResult<IEnumerable<LeaveRequestDto>>> GetPending()
    {
        var pending = await _leaveRequestService.GetPendingAsync();
        return Ok(pending);
    }

    [HttpGet("history")]
    public async Task<ActionResult<IEnumerable<LeaveRequestDto>>> GetHistory()
    {
        var history = await _leaveRequestService.GetHistoryAsync();
        return Ok(history);
    }

    [HttpPut("{id}/approve")]
    public async Task<ActionResult<LeaveRequestDto>> Approve(long id, ApprovalRequest request)
    {
        try
        {
            var result = await _leaveRequestService.ApproveAsync(id, request.ApproverEmployeeCode);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}/reject")]
    public async Task<ActionResult<LeaveRequestDto>> Reject(long id, RejectLeaveRequest request)
    {
        try
        {
            var result = await _leaveRequestService.RejectAsync(id, request.ApproverEmployeeCode, request.RejectionReason);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
