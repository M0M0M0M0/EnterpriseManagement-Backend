using EnterpriseManagement.Application.DTOs;

namespace EnterpriseManagement.Application.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDto>> GetAllAsync();
    Task<CustomerDto?> GetByCodeAsync(string customerCode);
    Task<CustomerDto> CreateAsync(CreateCustomerRequest request);
}
