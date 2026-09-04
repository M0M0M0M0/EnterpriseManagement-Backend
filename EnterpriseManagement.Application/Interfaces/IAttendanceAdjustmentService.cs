using EnterpriseManagement.Application.DTOs;

namespace EnterpriseManagement.Application.Interfaces;

public interface IAttendanceAdjustmentService
{
    Task<AttendanceAdjustmentDto> SubmitAsync(SubmitAdjustmentRequest request);
    Task<IEnumerable<AttendanceAdjustmentDto>> GetPendingAsync();
    Task<AttendanceAdjustmentDto> ApproveAsync(long adjustmentId, string approverEmployeeCode);
    Task<AttendanceAdjustmentDto> RejectAsync(long adjustmentId, string approverEmployeeCode);
}
