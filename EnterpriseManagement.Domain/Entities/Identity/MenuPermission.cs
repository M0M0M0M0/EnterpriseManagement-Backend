namespace EnterpriseManagement.Domain.Entities.Identity;

public class MenuPermission
{
    public long MenuId { get; set; }
    public Menu Menu { get; set; } = null!;

    public long PermissionId { get; set; }
    public Permission Permission { get; set; } = null!;
}
