namespace EnterpriseManagement.Application.DTOs;

public class RejectLeaveRequest
{
    public string ApproverEmployeeCode { get; set; } = string.Empty;
    public string? RejectionReason { get; set; }
}
