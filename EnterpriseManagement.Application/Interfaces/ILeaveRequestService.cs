using EnterpriseManagement.Application.DTOs;

namespace EnterpriseManagement.Application.Interfaces;

public interface ILeaveRequestService
{
    Task<LeaveRequestDto> SubmitAsync(SubmitLeaveRequest request, string employeeCode);
    Task<IEnumerable<LeaveRequestDto>> GetByEmployeeAsync(string employeeCode);
    Task<LeaveRequestDto> CancelAsync(long leaveRequestId, string employeeCode);
    Task<IEnumerable<LeaveRequestDto>> GetPendingAsync();
    Task<IEnumerable<LeaveRequestDto>> GetHistoryAsync();
    Task<LeaveRequestDto> ApproveAsync(long leaveRequestId, string approverEmployeeCode);
    Task<LeaveRequestDto> RejectAsync(long leaveRequestId, string approverEmployeeCode, string? rejectionReason);
}
