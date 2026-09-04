namespace EnterpriseManagement.Application.DTOs;

public class AttendanceRecordDto
{
    public string EmployeeCode { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public DateOnly AttendanceDate { get; set; }
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public decimal? WorkingHours { get; set; }
    public string Status { get; set; } = string.Empty;
}
