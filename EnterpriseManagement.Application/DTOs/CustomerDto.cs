namespace EnterpriseManagement.Application.DTOs;

public class CustomerDto
{
    public string CustomerCode { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? AssignedEmployeeName { get; set; }
    public string Status { get; set; } = string.Empty;
}
