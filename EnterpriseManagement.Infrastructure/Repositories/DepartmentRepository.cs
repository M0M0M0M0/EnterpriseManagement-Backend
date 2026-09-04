using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Domain.Entities.HR;
using EnterpriseManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseManagement.Infrastructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _context;

    public DepartmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Department?> GetByCodeAsync(string departmentCode) =>
        await _context.Departments
            .Include(d => d.Manager)
            .FirstOrDefaultAsync(d => d.DepartmentCode == departmentCode);

    public async Task<IEnumerable<Department>> GetAllAsync() =>
        await _context.Departments
            .Include(d => d.Manager)
            .ToListAsync();

    public async Task AddAsync(Department department) =>
        await _context.Departments.AddAsync(department);

    public Task<int> SaveChangesAsync() =>
        _context.SaveChangesAsync();
}
