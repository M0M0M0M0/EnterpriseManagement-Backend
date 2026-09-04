using EnterpriseManagement.Domain.Enums;

namespace EnterpriseManagement.Domain.Entities.Payroll;

public class PayrollDetail
{
    public long Id { get; set; }

    public long PayrollId { get; set; }
    public Payroll Payroll { get; set; } = null!;

    public string ComponentName { get; set; } = string.Empty;
    public PayrollComponentType ComponentType { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
}
