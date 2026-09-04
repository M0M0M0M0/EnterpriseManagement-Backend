namespace EnterpriseManagement.Domain.Entities.HR;

public class PositionSalary
{
    public long Id { get; set; }

    public long PositionId { get; set; }
    public Position Position { get; set; } = null!;

    public decimal StandardSalary { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
