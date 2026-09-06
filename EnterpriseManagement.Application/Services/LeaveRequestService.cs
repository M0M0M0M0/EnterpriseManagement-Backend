using EnterpriseManagement.Application.Common;
using EnterpriseManagement.Application.DTOs;
using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Domain.Entities.Leave;
using EnterpriseManagement.Domain.Enums;

namespace EnterpriseManagement.Application.Services;

public class LeaveRequestService : ILeaveRequestService
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveBalanceRepository _leaveBalanceRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public LeaveRequestService(
        ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveBalanceRepository leaveBalanceRepository,
        IEmployeeRepository employeeRepository)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveBalanceRepository = leaveBalanceRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<LeaveRequestDto> SubmitAsync(SubmitLeaveRequest request, string employeeCode)
    {
        var employee = await _employeeRepository.GetByEmployeeCodeAsync(employeeCode)
            ?? throw new InvalidOperationException($"Employee code '{employeeCode}' not found.");

        var leaveType = await _leaveTypeRepository.GetByCodeAsync(request.LeaveTypeCode)
            ?? throw new InvalidOperationException($"Leave type code '{request.LeaveTypeCode}' not found.");

        var totalDays = DateRangeHelper.CountWeekdays(request.StartDate, request.EndDate);

        // Chỉ chặn khi ĐÃ có LeaveBalance được cấp cho loại nghỉ/năm này mà không đủ ngày còn lại.
        // Loại nghỉ không cần quản lý hạn mức (vd nghỉ không lương) thì không có LeaveBalance, không bị chặn ở đây.
        var balance = await _leaveBalanceRepository.GetAsync(employee.Id, leaveType.Id, request.StartDate.Year);
        if (balance is not null && balance.RemainingDays < totalDays)
        {
            throw new InvalidOperationException(
                $"Insufficient leave balance: remaining {balance.RemainingDays} day(s), requested {totalDays} day(s).");
        }

        var leaveRequest = new LeaveRequest
        {
            EmployeeId = employee.Id,
            LeaveTypeId = leaveType.Id,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TotalDays = totalDays,
            Reason = request.Reason,
            Status = LeaveRequestStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        await _leaveRequestRepository.AddAsync(leaveRequest);
        await _leaveRequestRepository.SaveChangesAsync();

        var created = await _leaveRequestRepository.GetByIdAsync(leaveRequest.Id);
        return ToDto(created!);
    }

    public async Task<IEnumerable<LeaveRequestDto>> GetByEmployeeAsync(string employeeCode)
    {
        var employee = await _employeeRepository.GetByEmployeeCodeAsync(employeeCode)
            ?? throw new InvalidOperationException($"Employee code '{employeeCode}' not found.");

        var requests = await _leaveRequestRepository.GetByEmployeeIdAsync(employee.Id);
        return requests.Select(ToDto);
    }

    public async Task<LeaveRequestDto> CancelAsync(long leaveRequestId, string employeeCode)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(leaveRequestId)
            ?? throw new InvalidOperationException($"Leave request {leaveRequestId} not found.");

        if (leaveRequest.Employee.EmployeeCode != employeeCode)
        {
            throw new InvalidOperationException("Only the employee who submitted this leave request can cancel it.");
        }

        if (leaveRequest.Status != LeaveRequestStatus.Pending)
        {
            throw new InvalidOperationException("Only pending leave requests can be cancelled.");
        }

        leaveRequest.Status = LeaveRequestStatus.Cancelled;
        leaveRequest.UpdatedAt = DateTime.UtcNow;

        await _leaveRequestRepository.SaveChangesAsync();

        return ToDto(leaveRequest);
    }

    public async Task<IEnumerable<LeaveRequestDto>> GetPendingAsync()
    {
        var requests = await _leaveRequestRepository.GetPendingAsync();
        return requests.Select(ToDto);
    }

    public async Task<IEnumerable<LeaveRequestDto>> GetHistoryAsync()
    {
        var requests = await _leaveRequestRepository.GetAllAsync();
        return requests.Select(ToDto);
    }

    public async Task<LeaveRequestDto> ApproveAsync(long leaveRequestId, string approverEmployeeCode)
    {
        var approver = await _employeeRepository.GetByEmployeeCodeAsync(approverEmployeeCode)
            ?? throw new InvalidOperationException($"Employee code '{approverEmployeeCode}' not found.");

        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(leaveRequestId)
            ?? throw new InvalidOperationException($"Leave request {leaveRequestId} not found.");

        if (leaveRequest.Status != LeaveRequestStatus.Pending)
        {
            throw new InvalidOperationException("Only pending leave requests can be approved.");
        }

        leaveRequest.Status = LeaveRequestStatus.Approved;
        leaveRequest.ApprovedBy = approver.Id;
        leaveRequest.Approver = approver;
        leaveRequest.ApprovedAt = DateTime.UtcNow;
        leaveRequest.UpdatedAt = DateTime.UtcNow;

        // Trừ vào LeaveBalance (nếu loại nghỉ này có quản lý hạn mức), cùng 1 SaveChangesAsync với leaveRequest.
        var balance = await _leaveBalanceRepository.GetAsync(leaveRequest.EmployeeId, leaveRequest.LeaveTypeId, leaveRequest.StartDate.Year);
        if (balance is not null)
        {
            balance.UsedDays += leaveRequest.TotalDays;
            balance.RemainingDays -= leaveRequest.TotalDays;
            balance.UpdatedAt = DateTime.UtcNow;
        }

        await _leaveRequestRepository.SaveChangesAsync();

        return ToDto(leaveRequest);
    }

    public async Task<LeaveRequestDto> RejectAsync(long leaveRequestId, string approverEmployeeCode, string? rejectionReason)
    {
        var approver = await _employeeRepository.GetByEmployeeCodeAsync(approverEmployeeCode)
            ?? throw new InvalidOperationException($"Employee code '{approverEmployeeCode}' not found.");

        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(leaveRequestId)
            ?? throw new InvalidOperationException($"Leave request {leaveRequestId} not found.");

        if (leaveRequest.Status != LeaveRequestStatus.Pending)
        {
            throw new InvalidOperationException("Only pending leave requests can be rejected.");
        }

        leaveRequest.Status = LeaveRequestStatus.Rejected;
        leaveRequest.ApprovedBy = approver.Id;
        leaveRequest.Approver = approver;
        leaveRequest.ApprovedAt = DateTime.UtcNow;
        leaveRequest.RejectionReason = rejectionReason;
        leaveRequest.UpdatedAt = DateTime.UtcNow;

        await _leaveRequestRepository.SaveChangesAsync();

        return ToDto(leaveRequest);
    }

    private static LeaveRequestDto ToDto(LeaveRequest leaveRequest) => new()
    {
        Id = leaveRequest.Id,
        EmployeeCode = leaveRequest.Employee.EmployeeCode,
        EmployeeName = $"{leaveRequest.Employee.FirstName} {leaveRequest.Employee.LastName}",
        LeaveTypeCode = leaveRequest.LeaveType.LeaveTypeCode,
        LeaveTypeName = leaveRequest.LeaveType.LeaveTypeName,
        StartDate = leaveRequest.StartDate,
        EndDate = leaveRequest.EndDate,
        TotalDays = leaveRequest.TotalDays,
        Reason = leaveRequest.Reason,
        Status = leaveRequest.Status.ToString(),
        ApproverName = leaveRequest.Approver is null ? null : $"{leaveRequest.Approver.FirstName} {leaveRequest.Approver.LastName}",
        ApprovedAt = leaveRequest.ApprovedAt,
        RejectionReason = leaveRequest.RejectionReason
    };
}
