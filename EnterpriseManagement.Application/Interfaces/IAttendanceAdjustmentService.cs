using EnterpriseManagement.Application.DTOs;

namespace EnterpriseManagement.Application.Interfaces;

public interface IAttendanceAdjustmentService
{
    Task<AttendanceAdjustmentDto> SubmitAsync(SubmitAdjustmentRequest request, string employeeCode);
    Task<IEnumerable<AttendanceAdjustmentDto>> GetPendingAsync();
    Task<IEnumerable<AttendanceAdjustmentDto>> GetByEmployeeAsync(string employeeCode);
    Task<AttendanceAdjustmentDto> ApproveAsync(long adjustmentId, string approverEmployeeCode);
    Task<AttendanceAdjustmentDto> RejectAsync(long adjustmentId, string approverEmployeeCode);
}
