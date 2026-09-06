using EnterpriseManagement.Application.DTOs;

namespace EnterpriseManagement.Application.Interfaces;

public interface IUserService
{
    Task<LoginResult> LoginAsync(LoginRequest request);
    Task<UserDto> CreateAsync(CreateUserRequest request);
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto> SetActiveAsync(string username, bool isActive);
}
