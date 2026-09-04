using EnterpriseManagement.Domain.Entities.HR;

namespace EnterpriseManagement.Application.Interfaces;

public interface IDepartmentRepository
{
    Task<Department?> GetByCodeAsync(string departmentCode);
    Task<IEnumerable<Department>> GetAllAsync();
    Task AddAsync(Department department);
    Task<int> SaveChangesAsync();
}
