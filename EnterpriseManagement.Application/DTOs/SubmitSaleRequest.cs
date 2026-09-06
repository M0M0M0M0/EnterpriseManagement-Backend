namespace EnterpriseManagement.Application.DTOs;

public class SubmitSaleRequest
{
    public string CustomerCode { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? Note { get; set; }
}
