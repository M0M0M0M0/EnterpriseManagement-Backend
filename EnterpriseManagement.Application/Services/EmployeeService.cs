using EnterpriseManagement.Application.Common;
using EnterpriseManagement.Application.DTOs;
using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Domain.Entities.HR;
using EnterpriseManagement.Domain.Enums;

namespace EnterpriseManagement.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return employees.Select(ToDto);
    }

    public async Task<EmployeeDto?> GetByCodeAsync(string employeeCode)
    {
        var employee = await _employeeRepository.GetByEmployeeCodeAsync(employeeCode);
        return employee is null ? null : ToDto(employee);
    }

    public async Task<EmployeeDto> CreateAsync(CreateEmployeeRequest request)
    {
        var employee = new Employee
        {
            EmployeeCode = await GenerateUniqueEmployeeCodeAsync(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            DepartmentId = request.DepartmentId,
            PositionId = request.PositionId,
            ManagerId = request.ManagerId,
            HireDate = request.HireDate,
            EmploymentStatus = EmploymentStatus.Probation,
            CreatedAt = DateTime.UtcNow
        };

        await _employeeRepository.AddAsync(employee);
        await _employeeRepository.SaveChangesAsync();

        var created = await _employeeRepository.GetByIdAsync(employee.Id);
        return ToDto(created!);
    }

    public async Task<IEnumerable<EmployeeDto>> GetTeamAsync(string managerEmployeeCode)
    {
        var manager = await _employeeRepository.GetByEmployeeCodeAsync(managerEmployeeCode)
            ?? throw new InvalidOperationException($"Employee code '{managerEmployeeCode}' not found.");

        var team = await _employeeRepository.GetByManagerIdAsync(manager.Id);
        return team.Select(ToDto);
    }

    private async Task<string> GenerateUniqueEmployeeCodeAsync()
    {
        string code;
        do
        {
            code = RandomCodeGenerator.Generate(8);
        }
        while (await _employeeRepository.GetByEmployeeCodeAsync(code) is not null);

        return code;
    }

    private static EmployeeDto ToDto(Employee employee) => new()
    {
        EmployeeCode = employee.EmployeeCode,
        FullName = $"{employee.FirstName} {employee.LastName}",
        DepartmentName = employee.Department.DepartmentName,
        PositionName = employee.Position.PositionName,
        EmploymentStatus = employee.EmploymentStatus.ToString(),
        HireDate = employee.HireDate
    };
}
