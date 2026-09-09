namespace EnterpriseManagement.Application.DTOs;

public class CreateUserResult
{
    public UserDto User { get; set; } = new();
    public string GeneratedPassword { get; set; } = string.Empty;
}
