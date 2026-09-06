namespace EnterpriseManagement.Application.DTOs;

public class SubmitLeaveRequest
{
    public string LeaveTypeCode { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string? Reason { get; set; }
}
