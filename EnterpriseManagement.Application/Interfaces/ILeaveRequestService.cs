using EnterpriseManagement.Application.DTOs;

namespace EnterpriseManagement.Application.Interfaces;

public interface ILeaveRequestService
{
    Task<LeaveRequestDto> SubmitAsync(SubmitLeaveRequest request);
    Task<IEnumerable<LeaveRequestDto>> GetPendingAsync();
    Task<IEnumerable<LeaveRequestDto>> GetHistoryAsync();
    Task<LeaveRequestDto> ApproveAsync(long leaveRequestId, string approverEmployeeCode);
    Task<LeaveRequestDto> RejectAsync(long leaveRequestId, string approverEmployeeCode, string? rejectionReason);
}
