using EnterpriseManagement.Domain.Entities.Attendance;

namespace EnterpriseManagement.Application.Interfaces;

public interface IAttendanceRepository
{
    Task<AttendanceRecord?> GetByEmployeeAndDateAsync(long employeeId, DateOnly date);
    Task<IEnumerable<AttendanceRecord>> GetByEmployeeAsync(long employeeId);
    Task<IEnumerable<AttendanceRecord>> GetByEmployeeAndPeriodAsync(long employeeId, DateOnly startDate, DateOnly endDate);
    Task AddAsync(AttendanceRecord record);
    Task<int> SaveChangesAsync();
}
