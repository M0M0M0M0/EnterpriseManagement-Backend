using EnterpriseManagement.Application.DTOs;

namespace EnterpriseManagement.Application.Interfaces;

public interface IPayrollCalculationService
{
    Task<SalaryCalculationResult> CalculateAsync(string employeeCode, int year, int month);
}
