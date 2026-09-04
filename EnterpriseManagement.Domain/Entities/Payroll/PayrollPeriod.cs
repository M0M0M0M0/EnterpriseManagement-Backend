using EnterpriseManagement.Domain.Enums;

namespace EnterpriseManagement.Domain.Entities.Payroll;

public class PayrollPeriod
{
    public long Id { get; set; }
    public string PeriodName { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public PayrollPeriodStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();
}
