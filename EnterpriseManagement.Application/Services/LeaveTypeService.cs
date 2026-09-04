using EnterpriseManagement.Application.DTOs;
using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Domain.Entities.Leave;

namespace EnterpriseManagement.Application.Services;

public class LeaveTypeService : ILeaveTypeService
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public LeaveTypeService(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<IEnumerable<LeaveTypeDto>> GetAllAsync()
    {
        var leaveTypes = await _leaveTypeRepository.GetAllAsync();
        return leaveTypes.Select(ToDto);
    }

    public async Task<LeaveTypeDto?> GetByCodeAsync(string leaveTypeCode)
    {
        var leaveType = await _leaveTypeRepository.GetByCodeAsync(leaveTypeCode);
        return leaveType is null ? null : ToDto(leaveType);
    }

    public async Task<LeaveTypeDto> CreateAsync(CreateLeaveTypeRequest request)
    {
        var existing = await _leaveTypeRepository.GetByCodeAsync(request.LeaveTypeCode);
        if (existing is not null)
        {
            throw new InvalidOperationException($"Leave type code '{request.LeaveTypeCode}' already exists.");
        }

        var leaveType = new LeaveType
        {
            LeaveTypeCode = request.LeaveTypeCode,
            LeaveTypeName = request.LeaveTypeName,
            DefaultDays = request.DefaultDays,
            IsPaid = request.IsPaid,
            Description = request.Description,
            IsActive = true
        };

        await _leaveTypeRepository.AddAsync(leaveType);
        await _leaveTypeRepository.SaveChangesAsync();

        return ToDto(leaveType);
    }

    private static LeaveTypeDto ToDto(LeaveType leaveType) => new()
    {
        LeaveTypeCode = leaveType.LeaveTypeCode,
        LeaveTypeName = leaveType.LeaveTypeName,
        DefaultDays = leaveType.DefaultDays,
        IsPaid = leaveType.IsPaid,
        Description = leaveType.Description,
        IsActive = leaveType.IsActive
    };
}
