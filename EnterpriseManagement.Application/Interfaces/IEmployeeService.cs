using EnterpriseManagement.Application.DTOs;

namespace EnterpriseManagement.Application.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllAsync();
    Task<EmployeeDto?> GetByIdAsync(long id);
    Task<EmployeeDto> CreateAsync(CreateEmployeeRequest request);
}
