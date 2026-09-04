using EnterpriseManagement.Domain.Entities.Sales;

namespace EnterpriseManagement.Application.Interfaces;

public interface ICustomerRepository
{
    Task<Customer?> GetByCodeAsync(string customerCode);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task AddAsync(Customer customer);
    Task<int> SaveChangesAsync();
}
