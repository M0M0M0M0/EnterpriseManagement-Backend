using EnterpriseManagement.Domain.Entities.Payroll;

namespace EnterpriseManagement.Application.Interfaces;

public interface ISalaryStructureRepository
{
    Task<SalaryStructure?> GetActiveByEmployeeIdAsync(long employeeId);
}
