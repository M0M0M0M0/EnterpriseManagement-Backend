namespace EnterpriseManagement.Domain.Entities.Identity;

public class Permission
{
    public long Id { get; set; }
    public string PermissionCode { get; set; } = string.Empty;
    public string PermissionName { get; set; } = string.Empty;
    public string Module { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public ICollection<MenuPermission> MenuPermissions { get; set; } = new List<MenuPermission>();
}
