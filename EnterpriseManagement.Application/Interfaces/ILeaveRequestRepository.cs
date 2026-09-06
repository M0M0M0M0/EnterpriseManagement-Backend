using EnterpriseManagement.Domain.Entities.Leave;

namespace EnterpriseManagement.Application.Interfaces;

public interface ILeaveRequestRepository
{
    Task<IEnumerable<LeaveRequest>> GetApprovedUnpaidByEmployeeAndPeriodAsync(
        long employeeId, DateOnly periodStart, DateOnly periodEnd);

    Task<LeaveRequest?> GetByIdAsync(long id);
    Task<IEnumerable<LeaveRequest>> GetByEmployeeIdAsync(long employeeId);
    Task<IEnumerable<LeaveRequest>> GetPendingAsync();
    Task<IEnumerable<LeaveRequest>> GetAllAsync();
    Task AddAsync(LeaveRequest leaveRequest);
    Task<int> SaveChangesAsync();
}
