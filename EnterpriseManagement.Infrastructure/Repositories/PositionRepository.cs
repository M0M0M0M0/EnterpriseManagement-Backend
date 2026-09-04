using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Domain.Entities.HR;
using EnterpriseManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseManagement.Infrastructure.Repositories;

public class PositionRepository : IPositionRepository
{
    private readonly ApplicationDbContext _context;

    public PositionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Position?> GetByCodeAsync(string positionCode) =>
        await _context.Positions.FirstOrDefaultAsync(p => p.PositionCode == positionCode);

    public async Task<IEnumerable<Position>> GetAllAsync() =>
        await _context.Positions.ToListAsync();

    public async Task AddAsync(Position position) =>
        await _context.Positions.AddAsync(position);

    public Task<int> SaveChangesAsync() =>
        _context.SaveChangesAsync();
}
