using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Domain.Entities.Payroll;
using EnterpriseManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseManagement.Infrastructure.Repositories;

public class SalaryStructureRepository : ISalaryStructureRepository
{
    private readonly ApplicationDbContext _context;

    public SalaryStructureRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SalaryStructure?> GetActiveByEmployeeIdAsync(long employeeId) =>
        await _context.SalaryStructures
            .Where(s => s.EmployeeId == employeeId && s.IsActive)
            .OrderByDescending(s => s.EffectiveFrom)
            .FirstOrDefaultAsync();
}
