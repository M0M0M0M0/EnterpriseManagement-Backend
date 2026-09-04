using EnterpriseManagement.Application.DTOs;

namespace EnterpriseManagement.Application.Interfaces;

public interface ILeaveBalanceService
{
    Task<IEnumerable<LeaveBalanceDto>> GetByEmployeeAsync(string employeeCode, int year);
    Task<LeaveBalanceDto> SetAllocatedDaysAsync(SetLeaveBalanceRequest request);
}
