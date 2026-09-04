namespace EnterpriseManagement.Domain.Entities.Identity;

public class RolePermission
{
    public long RoleId { get; set; }
    public Role Role { get; set; } = null!;

    public long PermissionId { get; set; }
    public Permission Permission { get; set; } = null!;

    public DateTime GrantedAt { get; set; }
    public long? GrantedBy { get; set; }
    public User? GrantedByUser { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
