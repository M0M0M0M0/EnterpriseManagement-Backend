using EnterpriseManagement.Domain.Entities.Attendance;

namespace EnterpriseManagement.Application.Interfaces;

public interface IAttendanceAdjustmentRepository
{
    Task<AttendanceAdjustment?> GetByIdAsync(long id);
    Task<IEnumerable<AttendanceAdjustment>> GetPendingAsync();
    Task AddAsync(AttendanceAdjustment adjustment);
    Task<int> SaveChangesAsync();
}
