using EnterpriseManagement.Domain.Entities.Identity;

namespace EnterpriseManagement.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
    Task<int> SaveChangesAsync();
}
