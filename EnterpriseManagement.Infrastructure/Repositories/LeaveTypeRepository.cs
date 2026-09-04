using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Domain.Entities.Leave;
using EnterpriseManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseManagement.Infrastructure.Repositories;

public class LeaveTypeRepository : ILeaveTypeRepository
{
    private readonly ApplicationDbContext _context;

    public LeaveTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LeaveType?> GetByCodeAsync(string leaveTypeCode) =>
        await _context.LeaveTypes.FirstOrDefaultAsync(l => l.LeaveTypeCode == leaveTypeCode);

    public async Task<LeaveType?> GetByIdAsync(long id) =>
        await _context.LeaveTypes.FirstOrDefaultAsync(l => l.Id == id);

    public async Task<IEnumerable<LeaveType>> GetAllAsync() =>
        await _context.LeaveTypes.ToListAsync();

    public async Task AddAsync(LeaveType leaveType) =>
        await _context.LeaveTypes.AddAsync(leaveType);

    public Task<int> SaveChangesAsync() =>
        _context.SaveChangesAsync();
}
