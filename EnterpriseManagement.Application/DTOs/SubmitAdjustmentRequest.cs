namespace EnterpriseManagement.Application.DTOs;

public class SubmitAdjustmentRequest
{
    public DateOnly AttendanceDate { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime? NewCheckInTime { get; set; }
    public DateTime? NewCheckOutTime { get; set; }
}
