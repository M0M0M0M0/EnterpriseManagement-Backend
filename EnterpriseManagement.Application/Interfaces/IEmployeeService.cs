using EnterpriseManagement.Application.DTOs;

namespace EnterpriseManagement.Application.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllAsync();
    Task<EmployeeDto?> GetByCodeAsync(string employeeCode);
    Task<EmployeeDto> CreateAsync(CreateEmployeeRequest request);
}
