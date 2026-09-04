using EnterpriseManagement.Application.DTOs;
using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Domain.Entities.Leave;

namespace EnterpriseManagement.Application.Services;

public class LeaveBalanceService : ILeaveBalanceService
{
    private readonly ILeaveBalanceRepository _leaveBalanceRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public LeaveBalanceService(
        ILeaveBalanceRepository leaveBalanceRepository,
        IEmployeeRepository employeeRepository,
        ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveBalanceRepository = leaveBalanceRepository;
        _employeeRepository = employeeRepository;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<IEnumerable<LeaveBalanceDto>> GetByEmployeeAsync(string employeeCode, int year)
    {
        var employee = await _employeeRepository.GetByEmployeeCodeAsync(employeeCode)
            ?? throw new InvalidOperationException($"Employee code '{employeeCode}' not found.");

        var balances = await _leaveBalanceRepository.GetByEmployeeAndYearAsync(employee.Id, year);
        return balances.Select(ToDto);
    }

    public async Task<LeaveBalanceDto> SetAllocatedDaysAsync(SetLeaveBalanceRequest request)
    {
        var employee = await _employeeRepository.GetByEmployeeCodeAsync(request.EmployeeCode)
            ?? throw new InvalidOperationException($"Employee code '{request.EmployeeCode}' not found.");

        var leaveType = await _leaveTypeRepository.GetByCodeAsync(request.LeaveTypeCode)
            ?? throw new InvalidOperationException($"Leave type code '{request.LeaveTypeCode}' not found.");

        var balance = await _leaveBalanceRepository.GetAsync(employee.Id, leaveType.Id, request.Year);
        if (balance is null)
        {
            balance = new LeaveBalance
            {
                EmployeeId = employee.Id,
                LeaveTypeId = leaveType.Id,
                Year = request.Year,
                AllocatedDays = request.AllocatedDays,
                UsedDays = 0,
                RemainingDays = request.AllocatedDays,
                CreatedAt = DateTime.UtcNow
            };
            await _leaveBalanceRepository.AddAsync(balance);
        }
        else
        {
            var delta = request.AllocatedDays - balance.AllocatedDays;
            balance.AllocatedDays = request.AllocatedDays;
            balance.RemainingDays += delta;
            balance.UpdatedAt = DateTime.UtcNow;
        }

        await _leaveBalanceRepository.SaveChangesAsync();

        return ToDto(balance, leaveType);
    }

    private static LeaveBalanceDto ToDto(LeaveBalance balance) => ToDto(balance, balance.LeaveType);

    private static LeaveBalanceDto ToDto(LeaveBalance balance, LeaveType leaveType) => new()
    {
        LeaveTypeCode = leaveType.LeaveTypeCode,
        LeaveTypeName = leaveType.LeaveTypeName,
        Year = balance.Year,
        AllocatedDays = balance.AllocatedDays,
        UsedDays = balance.UsedDays,
        RemainingDays = balance.RemainingDays
    };
}
