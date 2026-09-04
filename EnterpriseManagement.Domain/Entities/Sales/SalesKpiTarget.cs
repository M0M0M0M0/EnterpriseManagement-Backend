namespace EnterpriseManagement.Domain.Entities.Sales;

public class SalesKpiTarget
{
    public long Id { get; set; }
    public string TargetName { get; set; } = string.Empty;
    public decimal MinimumRevenue { get; set; }
    public decimal CommissionRate { get; set; }
    public DateOnly EffectiveFrom { get; set; }
    public DateOnly? EffectiveTo { get; set; }
    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
