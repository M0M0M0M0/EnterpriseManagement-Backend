using EnterpriseManagement.Domain.Entities.Leave;

namespace EnterpriseManagement.Application.Interfaces;

public interface ILeaveTypeRepository
{
    Task<LeaveType?> GetByCodeAsync(string leaveTypeCode);
    Task<LeaveType?> GetByIdAsync(long id);
    Task<IEnumerable<LeaveType>> GetAllAsync();
    Task AddAsync(LeaveType leaveType);
    Task<int> SaveChangesAsync();
}
