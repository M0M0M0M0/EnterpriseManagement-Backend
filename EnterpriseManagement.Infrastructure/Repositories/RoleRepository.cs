using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Domain.Entities.Identity;
using EnterpriseManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseManagement.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetByCodeAsync(string roleCode) =>
        await _context.Roles.FirstOrDefaultAsync(r => r.RoleCode == roleCode);
}
