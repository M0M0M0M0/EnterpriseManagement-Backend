using EnterpriseManagement.Domain.Entities.Identity;

namespace EnterpriseManagement.Application.Interfaces;

public interface IRoleRepository
{
    Task<Role?> GetByCodeAsync(string roleCode);
}
