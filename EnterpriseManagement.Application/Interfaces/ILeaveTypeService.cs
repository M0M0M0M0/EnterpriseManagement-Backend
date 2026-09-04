using EnterpriseManagement.Application.DTOs;

namespace EnterpriseManagement.Application.Interfaces;

public interface ILeaveTypeService
{
    Task<IEnumerable<LeaveTypeDto>> GetAllAsync();
    Task<LeaveTypeDto?> GetByCodeAsync(string leaveTypeCode);
    Task<LeaveTypeDto> CreateAsync(CreateLeaveTypeRequest request);
}
