using EnterpriseManagement.Application.DTOs;
using EnterpriseManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseManagement.Api.Controllers;

[ApiController]
[Route("api/attendance")]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceService _attendanceService;

    public AttendanceController(IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
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
}
