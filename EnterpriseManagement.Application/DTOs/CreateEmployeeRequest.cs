namespace EnterpriseManagement.Application.DTOs;

public class CreateEmployeeRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string DepartmentCode { get; set; } = string.Empty;
    public string PositionCode { get; set; } = string.Empty;
    public string? ManagerCode { get; set; }
    public DateOnly HireDate { get; set; }
}
