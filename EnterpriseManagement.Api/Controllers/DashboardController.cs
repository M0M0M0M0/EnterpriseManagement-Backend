using EnterpriseManagement.Application.DTOs;
using EnterpriseManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseManagement.Api.Controllers;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("employee/{employeeCode}")]
    public async Task<ActionResult<EmployeeDashboardDto>> GetEmployeeDashboard(string employeeCode)
    {
        try
        {
            var dashboard = await _dashboardService.GetEmployeeDashboardAsync(employeeCode);
            return Ok(dashboard);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("manager/{managerEmployeeCode}")]
    public async Task<ActionResult<ManagerDashboardDto>> GetManagerDashboard(string managerEmployeeCode)
    {
        try
        {
            var dashboard = await _dashboardService.GetManagerDashboardAsync(managerEmployeeCode);
            return Ok(dashboard);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
