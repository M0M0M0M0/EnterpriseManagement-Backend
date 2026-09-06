namespace EnterpriseManagement.Application.DTOs;

public class LoginResult
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string? EmployeeCode { get; set; }
    public List<string> Roles { get; set; } = new();
}
