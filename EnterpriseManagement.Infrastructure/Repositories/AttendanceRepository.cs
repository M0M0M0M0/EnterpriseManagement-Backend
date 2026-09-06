using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Domain.Entities.Attendance;
using EnterpriseManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseManagement.Infrastructure.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly ApplicationDbContext _context;

    public AttendanceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AttendanceRecord?> GetByEmployeeAndDateAsync(long employeeId, DateOnly date) =>
        await _context.AttendanceRecords
            .FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.AttendanceDate == date);

    public async Task<IEnumerable<AttendanceRecord>> GetByEmployeeAsync(long employeeId) =>
        await _context.AttendanceRecords
            .Where(a => a.EmployeeId == employeeId)
            .OrderByDescending(a => a.AttendanceDate)
            .ToListAsync();

    public async Task<IEnumerable<AttendanceRecord>> GetByEmployeeAndPeriodAsync(long employeeId, DateOnly startDate, DateOnly endDate) =>
        await _context.AttendanceRecords
            .Where(a => a.EmployeeId == employeeId && a.AttendanceDate >= startDate && a.AttendanceDate <= endDate)
            .ToListAsync();

    public async Task<IEnumerable<AttendanceRecord>> GetByDepartmentAndPeriodAsync(long departmentId, DateOnly startDate, DateOnly endDate) =>
        await _context.AttendanceRecords
            .Include(a => a.Employee)
            .Where(a => a.Employee.DepartmentId == departmentId && a.AttendanceDate >= startDate && a.AttendanceDate <= endDate)
            .OrderBy(a => a.AttendanceDate)
            .ToListAsync();

    public async Task AddAsync(AttendanceRecord record) =>
        await _context.AttendanceRecords.AddAsync(record);

    public Task<int> SaveChangesAsync() =>
        _context.SaveChangesAsync();
}
