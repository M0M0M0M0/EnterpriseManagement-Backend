using EnterpriseManagement.Application.DTOs;
using EnterpriseManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseManagement.Api.Controllers;

[ApiController]
[Route("api/attendance")]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceService _attendanceService;
    private readonly IAttendanceAdjustmentService _attendanceAdjustmentService;

    public AttendanceController(IAttendanceService attendanceService, IAttendanceAdjustmentService attendanceAdjustmentService)
    {
        _attendanceService = attendanceService;
        _attendanceAdjustmentService = attendanceAdjustmentService;
    }

    [HttpPost("punch")]
    public async Task<ActionResult<AttendanceRecordDto>> Punch(PunchRequest request)
    {
        try
        {
            var result = await _attendanceService.PunchAsync(request);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("{employeeCode}/history")]
    public async Task<ActionResult<IEnumerable<AttendanceRecordDto>>> GetHistory(string employeeCode)
    {
        try
        {
            var history = await _attendanceService.GetHistoryAsync(employeeCode);
            return Ok(history);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("adjustments")]
    public async Task<ActionResult<AttendanceAdjustmentDto>> SubmitAdjustment(SubmitAdjustmentRequest request)
    {
        try
        {
            var result = await _attendanceAdjustmentService.SubmitAsync(request);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("adjustments/pending")]
    public async Task<ActionResult<IEnumerable<AttendanceAdjustmentDto>>> GetPendingAdjustments()
    {
        var pending = await _attendanceAdjustmentService.GetPendingAsync();
        return Ok(pending);
    }

    [HttpPut("adjustments/{id}/approve")]
    public async Task<ActionResult<AttendanceAdjustmentDto>> ApproveAdjustment(long id, ApprovalRequest request)
    {
        try
        {
            var result = await _attendanceAdjustmentService.ApproveAsync(id, request.ApproverEmployeeCode);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("adjustments/{id}/reject")]
    public async Task<ActionResult<AttendanceAdjustmentDto>> RejectAdjustment(long id, ApprovalRequest request)
    {
        try
        {
            var result = await _attendanceAdjustmentService.RejectAsync(id, request.ApproverEmployeeCode);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
