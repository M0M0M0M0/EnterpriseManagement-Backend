namespace EnterpriseManagement.Application.DTOs;

public class CreateDepartmentRequest
{
    public string DepartmentCode { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public long? ManagerId { get; set; }
    public string? Description { get; set; }
}
