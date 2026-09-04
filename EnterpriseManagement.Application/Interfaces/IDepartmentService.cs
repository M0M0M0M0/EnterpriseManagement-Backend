using EnterpriseManagement.Application.DTOs;

namespace EnterpriseManagement.Application.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDto>> GetAllAsync();
    Task<DepartmentDto?> GetByCodeAsync(string departmentCode);
    Task<DepartmentDto> CreateAsync(CreateDepartmentRequest request);
}
