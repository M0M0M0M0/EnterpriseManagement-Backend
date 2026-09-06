using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseManagement.Infrastructure.Persistence;

public static class DataSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        if (!await context.Roles.AnyAsync())
        {
            context.Roles.AddRange(
                new Role { RoleCode = "ADMIN", RoleName = "Administrator", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Role { RoleCode = "MANAGER", RoleName = "Manager", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Role { RoleCode = "EMPLOYEE", RoleName = "Employee", IsActive = true, CreatedAt = DateTime.UtcNow });
            await context.SaveChangesAsync();
        }

        if (!await context.Users.AnyAsync())
        {
            var adminRole = await context.Roles.FirstAsync(r => r.RoleCode == "ADMIN");
            var admin = new User
            {
                Username = "admin",
                Email = "admin@enterprise.local",
                PasswordHash = passwordHasher.Hash("Admin@123"),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            admin.UserRoles.Add(new UserRole { Role = adminRole, AssignedAt = DateTime.UtcNow });

            context.Users.Add(admin);
            await context.SaveChangesAsync();
        }
    }
}
