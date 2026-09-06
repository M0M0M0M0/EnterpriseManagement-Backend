using EnterpriseManagement.Application.DTOs;

namespace EnterpriseManagement.Application.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDto>> GetAllAsync();
    Task<DepartmentDto?> GetByCodeAsync(string departmentCode);
    Task<DepartmentDto> CreateAsync(CreateDepartmentRequest request);
    Task<DepartmentDto> UpdateAsync(string departmentCode, UpdateDepartmentRequest request);
    Task<DepartmentDto> SetActiveAsync(string departmentCode, bool isActive);
}
