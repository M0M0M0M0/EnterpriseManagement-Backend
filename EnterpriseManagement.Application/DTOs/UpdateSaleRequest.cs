namespace EnterpriseManagement.Application.DTOs;

public class UpdateSaleRequest
{
    public string EmployeeCode { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? Note { get; set; }
}
