using EnterpriseManagement.Domain.Entities.Attendance;

namespace EnterpriseManagement.Application.Interfaces;

public interface IAttendanceAdjustmentRepository
{
    Task<AttendanceAdjustment?> GetByIdAsync(long id);
    Task<IEnumerable<AttendanceAdjustment>> GetPendingAsync();
    Task<IEnumerable<AttendanceAdjustment>> GetByEmployeeIdAsync(long employeeId);
    Task AddAsync(AttendanceAdjustment adjustment);
    Task<int> SaveChangesAsync();
}
