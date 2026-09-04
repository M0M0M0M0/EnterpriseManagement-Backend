using EnterpriseManagement.Domain.Entities.Leave;

namespace EnterpriseManagement.Application.Interfaces;

public interface ILeaveRequestRepository
{
    Task<IEnumerable<LeaveRequest>> GetApprovedUnpaidByEmployeeAndPeriodAsync(
        long employeeId, DateOnly periodStart, DateOnly periodEnd);
}
