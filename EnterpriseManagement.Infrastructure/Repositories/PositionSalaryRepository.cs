using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Domain.Entities.HR;
using EnterpriseManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseManagement.Infrastructure.Repositories;

public class PositionSalaryRepository : IPositionSalaryRepository
{
    private readonly ApplicationDbContext _context;

    public PositionSalaryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PositionSalary?> GetByPositionIdAsync(long positionId) =>
        await _context.PositionSalaries.FirstOrDefaultAsync(x => x.PositionId == positionId);

    public async Task AddAsync(PositionSalary positionSalary) =>
        await _context.PositionSalaries.AddAsync(positionSalary);

    public Task<int> SaveChangesAsync() =>
        _context.SaveChangesAsync();
}
