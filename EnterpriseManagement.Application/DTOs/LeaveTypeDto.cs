namespace EnterpriseManagement.Application.DTOs;

public class LeaveTypeDto
{
    public string LeaveTypeCode { get; set; } = string.Empty;
    public string LeaveTypeName { get; set; } = string.Empty;
    public decimal? DefaultDays { get; set; }
    public bool IsPaid { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
