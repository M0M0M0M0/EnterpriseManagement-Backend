using EnterpriseManagement.Domain.Entities.HR;

namespace EnterpriseManagement.Application.Interfaces;

public interface IPositionSalaryRepository
{
    Task<PositionSalary?> GetByPositionIdAsync(long positionId);
    Task AddAsync(PositionSalary positionSalary);
    Task<int> SaveChangesAsync();
}
