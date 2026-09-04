using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Domain.Entities.Leave;
using EnterpriseManagement.Domain.Enums;
using EnterpriseManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseManagement.Infrastructure.Repositories;

public class LeaveRequestRepository : ILeaveRequestRepository
{
    private readonly ApplicationDbContext _context;

    public LeaveRequestRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LeaveRequest>> GetApprovedUnpaidByEmployeeAndPeriodAsync(
        long employeeId, DateOnly periodStart, DateOnly periodEnd) =>
        await _context.LeaveRequests
            .Include(l => l.LeaveType)
            .Where(l => l.EmployeeId == employeeId
                && l.Status == LeaveRequestStatus.Approved
                && !l.LeaveType.IsPaid
                && l.StartDate <= periodEnd
                && l.EndDate >= periodStart)
            .ToListAsync();
}
