using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Domain.Entities.Attendance;
using EnterpriseManagement.Domain.Enums;
using EnterpriseManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseManagement.Infrastructure.Repositories;

public class AttendanceAdjustmentRepository : IAttendanceAdjustmentRepository
{
    private readonly ApplicationDbContext _context;

    public AttendanceAdjustmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AttendanceAdjustment?> GetByIdAsync(long id) =>
        await _context.AttendanceAdjustments
            .Include(a => a.Attendance)
            .Include(a => a.Requester)
            .FirstOrDefaultAsync(a => a.Id == id);

    public async Task<IEnumerable<AttendanceAdjustment>> GetPendingAsync() =>
        await _context.AttendanceAdjustments
            .Include(a => a.Attendance)
            .Include(a => a.Requester)
            .Where(a => a.Status == ApprovalStatus.Pending)
            .ToListAsync();

    public async Task<IEnumerable<AttendanceAdjustment>> GetByEmployeeIdAsync(long employeeId) =>
        await _context.AttendanceAdjustments
            .Include(a => a.Attendance)
            .Include(a => a.Requester)
            .Include(a => a.Approver)
            .Where(a => a.RequestedBy == employeeId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();

    public async Task AddAsync(AttendanceAdjustment adjustment) =>
        await _context.AttendanceAdjustments.AddAsync(adjustment);

    public Task<int> SaveChangesAsync() =>
        _context.SaveChangesAsync();
}
