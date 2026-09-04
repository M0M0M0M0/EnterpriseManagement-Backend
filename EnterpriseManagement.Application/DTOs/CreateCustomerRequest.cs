namespace EnterpriseManagement.Application.DTOs;

public class CreateCustomerRequest
{
    public string EmployeeCode { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
}
