namespace EnterpriseManagement.Application.DTOs;

public class EmployeeDto
{
    public string EmployeeCode { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public string PositionName { get; set; } = string.Empty;
    public string EmploymentStatus { get; set; } = string.Empty;
    public DateOnly HireDate { get; set; }
}
