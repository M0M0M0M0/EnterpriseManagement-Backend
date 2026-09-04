using EnterpriseManagement.Domain.Entities.HR;

namespace EnterpriseManagement.Application.Interfaces;

public interface IPositionRepository
{
    Task<Position?> GetByCodeAsync(string positionCode);
    Task<IEnumerable<Position>> GetAllAsync();
    Task AddAsync(Position position);
    Task<int> SaveChangesAsync();
}
