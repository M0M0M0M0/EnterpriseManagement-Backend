namespace EnterpriseManagement.Application.DTOs;

public class CreateEmployeeRequest
{
    public string EmployeeCode { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public long DepartmentId { get; set; }
    public long PositionId { get; set; }
    public long? ManagerId { get; set; }
    public DateOnly HireDate { get; set; }
}
