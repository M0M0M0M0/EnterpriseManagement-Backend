namespace EnterpriseManagement.Application.DTOs;

public class UpdateDepartmentRequest
{
    public string DepartmentName { get; set; } = string.Empty;
    public long? ManagerId { get; set; }
    public string? Description { get; set; }
}
