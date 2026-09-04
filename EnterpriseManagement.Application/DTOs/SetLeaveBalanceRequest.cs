namespace EnterpriseManagement.Application.DTOs;

public class SetLeaveBalanceRequest
{
    public string EmployeeCode { get; set; } = string.Empty;
    public string LeaveTypeCode { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal AllocatedDays { get; set; }
}
