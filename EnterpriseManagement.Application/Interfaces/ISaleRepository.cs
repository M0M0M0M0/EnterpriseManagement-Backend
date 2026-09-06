using EnterpriseManagement.Domain.Entities.Sales;

namespace EnterpriseManagement.Application.Interfaces;

public interface ISaleRepository
{
    Task<Sale?> GetByIdAsync(long id);
    Task<IEnumerable<Sale>> GetPendingAsync();
    Task<IEnumerable<Sale>> GetAllAsync();
    Task<Sale?> GetByCodeAsync(string saleCode);
    Task<IEnumerable<Sale>> GetByEmployeeIdAsync(long employeeId);
    Task AddAsync(Sale sale);
    Task<int> SaveChangesAsync();
}
