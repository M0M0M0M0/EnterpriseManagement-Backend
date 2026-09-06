using EnterpriseManagement.Application.DTOs;

namespace EnterpriseManagement.Application.Interfaces;

public interface IDashboardService
{
    Task<EmployeeDashboardDto> GetEmployeeDashboardAsync(string employeeCode);
    Task<ManagerDashboardDto> GetManagerDashboardAsync(string managerEmployeeCode);
}
