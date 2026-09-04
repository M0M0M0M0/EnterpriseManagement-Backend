namespace EnterpriseManagement.Application.DTOs;

public class SalaryCalculationResult
{
    public string EmployeeCode { get; set; } = string.Empty;
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal BaseSalary { get; set; }
    public int StandardWorkingDays { get; set; }
    public int DeductedDays { get; set; }
    public decimal DailyRate { get; set; }
    public decimal DeductionAmount { get; set; }
    public decimal NetSalary { get; set; }
}
