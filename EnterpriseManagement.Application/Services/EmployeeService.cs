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

    public async Task<EmployeeDto?> GetByIdAsync(long id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        return employee is null ? null : ToDto(employee);
    }

    public async Task<EmployeeDto> CreateAsync(CreateEmployeeRequest request)
    {
        var existing = await _employeeRepository.GetByEmployeeCodeAsync(request.EmployeeCode);
        if (existing is not null)
        {
            throw new InvalidOperationException($"Employee code '{request.EmployeeCode}' already exists.");
        }

        var employee = new Employee
        {
            EmployeeCode = request.EmployeeCode,
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

    private static EmployeeDto ToDto(Employee employee) => new()
    {
        Id = employee.Id,
        EmployeeCode = employee.EmployeeCode,
        FullName = $"{employee.FirstName} {employee.LastName}",
        DepartmentName = employee.Department.DepartmentName,
        PositionName = employee.Position.PositionName,
        EmploymentStatus = employee.EmploymentStatus.ToString(),
        HireDate = employee.HireDate
    };
}
