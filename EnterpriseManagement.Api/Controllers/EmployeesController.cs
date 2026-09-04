using EnterpriseManagement.Application.DTOs;
using EnterpriseManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
    {
        var employees = await _employeeService.GetAllAsync();
        return Ok(employees);
    }

    [HttpGet("{employeeCode}")]
    public async Task<ActionResult<EmployeeDto>> GetByCode(string employeeCode)
    {
        var employee = await _employeeService.GetByCodeAsync(employeeCode);
        return employee is null ? NotFound() : Ok(employee);
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> Create(CreateEmployeeRequest request)
    {
        var created = await _employeeService.CreateAsync(request);
        return CreatedAtAction(nameof(GetByCode), new { employeeCode = created.EmployeeCode }, created);
    }
}
