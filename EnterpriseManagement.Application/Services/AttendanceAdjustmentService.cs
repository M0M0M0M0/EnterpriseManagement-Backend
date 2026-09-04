using EnterpriseManagement.Application.DTOs;
using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Domain.Entities.Attendance;
using EnterpriseManagement.Domain.Enums;

namespace EnterpriseManagement.Application.Services;

public class AttendanceAdjustmentService : IAttendanceAdjustmentService
{
    private readonly IAttendanceAdjustmentRepository _adjustmentRepository;
    private readonly IAttendanceRepository _attendanceRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public AttendanceAdjustmentService(
        IAttendanceAdjustmentRepository adjustmentRepository,
        IAttendanceRepository attendanceRepository,
        IEmployeeRepository employeeRepository)
    {
        _adjustmentRepository = adjustmentRepository;
        _attendanceRepository = attendanceRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<AttendanceAdjustmentDto> SubmitAsync(SubmitAdjustmentRequest request)
    {
        var employee = await _employeeRepository.GetByEmployeeCodeAsync(request.EmployeeCode)
            ?? throw new InvalidOperationException($"Employee code '{request.EmployeeCode}' not found.");

        var record = await _attendanceRepository.GetByEmployeeAndDateAsync(employee.Id, request.AttendanceDate)
            ?? throw new InvalidOperationException($"No attendance record found for {request.AttendanceDate}.");

        var adjustment = new AttendanceAdjustment
        {
            AttendanceId = record.Id,
            RequestedBy = employee.Id,
            Reason = request.Reason,
            OldCheckInTime = record.CheckInTime,
            NewCheckInTime = request.NewCheckInTime,
            OldCheckOutTime = record.CheckOutTime,
            NewCheckOutTime = request.NewCheckOutTime,
            Status = ApprovalStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        await _adjustmentRepository.AddAsync(adjustment);
        await _adjustmentRepository.SaveChangesAsync();

        return ToDto(adjustment, record.AttendanceDate, employee.EmployeeCode, $"{employee.FirstName} {employee.LastName}");
    }

    public async Task<IEnumerable<AttendanceAdjustmentDto>> GetPendingAsync()
    {
        var adjustments = await _adjustmentRepository.GetPendingAsync();
        return adjustments.Select(a => ToDto(a, a.Attendance.AttendanceDate, a.Requester.EmployeeCode, $"{a.Requester.FirstName} {a.Requester.LastName}"));
    }

    public async Task<AttendanceAdjustmentDto> ApproveAsync(long adjustmentId, string approverEmployeeCode)
    {
        var approver = await _employeeRepository.GetByEmployeeCodeAsync(approverEmployeeCode)
            ?? throw new InvalidOperationException($"Employee code '{approverEmployeeCode}' not found.");

        var adjustment = await _adjustmentRepository.GetByIdAsync(adjustmentId)
            ?? throw new InvalidOperationException($"Adjustment {adjustmentId} not found.");

        if (adjustment.Status != ApprovalStatus.Pending)
        {
            throw new InvalidOperationException("Only pending adjustments can be approved.");
        }

        adjustment.Status = ApprovalStatus.Approved;
        adjustment.ApprovedBy = approver.Id;
        adjustment.ApprovedAt = DateTime.UtcNow;

        // Cùng 1 DbContext (Scoped) đang theo dõi cả adjustment lẫn adjustment.Attendance,
        // nên SaveChangesAsync() dưới đây commit cả 2 thay đổi trong 1 transaction duy nhất.
        if (adjustment.NewCheckInTime.HasValue)
        {
            adjustment.Attendance.CheckInTime = adjustment.NewCheckInTime;
        }

        if (adjustment.NewCheckOutTime.HasValue)
        {
            adjustment.Attendance.CheckOutTime = adjustment.NewCheckOutTime;
        }

        if (adjustment.Attendance.CheckInTime.HasValue && adjustment.Attendance.CheckOutTime.HasValue)
        {
            adjustment.Attendance.WorkingHours =
                (decimal)(adjustment.Attendance.CheckOutTime.Value - adjustment.Attendance.CheckInTime.Value).TotalHours;
        }

        adjustment.Attendance.UpdatedAt = DateTime.UtcNow;

        await _adjustmentRepository.SaveChangesAsync();

        return ToDto(adjustment, adjustment.Attendance.AttendanceDate, adjustment.Requester.EmployeeCode,
            $"{adjustment.Requester.FirstName} {adjustment.Requester.LastName}", approver);
    }

    public async Task<AttendanceAdjustmentDto> RejectAsync(long adjustmentId, string approverEmployeeCode)
    {
        var approver = await _employeeRepository.GetByEmployeeCodeAsync(approverEmployeeCode)
            ?? throw new InvalidOperationException($"Employee code '{approverEmployeeCode}' not found.");

        var adjustment = await _adjustmentRepository.GetByIdAsync(adjustmentId)
            ?? throw new InvalidOperationException($"Adjustment {adjustmentId} not found.");

        if (adjustment.Status != ApprovalStatus.Pending)
        {
            throw new InvalidOperationException("Only pending adjustments can be rejected.");
        }

        adjustment.Status = ApprovalStatus.Rejected;
        adjustment.ApprovedBy = approver.Id;
        adjustment.ApprovedAt = DateTime.UtcNow;

        await _adjustmentRepository.SaveChangesAsync();

        return ToDto(adjustment, adjustment.Attendance.AttendanceDate, adjustment.Requester.EmployeeCode,
            $"{adjustment.Requester.FirstName} {adjustment.Requester.LastName}", approver);
    }

    private static AttendanceAdjustmentDto ToDto(
        AttendanceAdjustment adjustment, DateOnly attendanceDate, string employeeCode, string employeeName,
        Domain.Entities.HR.Employee? approver = null) => new()
    {
        Id = adjustment.Id,
        EmployeeCode = employeeCode,
        EmployeeName = employeeName,
        AttendanceDate = attendanceDate,
        Reason = adjustment.Reason,
        OldCheckInTime = adjustment.OldCheckInTime,
        NewCheckInTime = adjustment.NewCheckInTime,
        OldCheckOutTime = adjustment.OldCheckOutTime,
        NewCheckOutTime = adjustment.NewCheckOutTime,
        Status = adjustment.Status.ToString(),
        ApproverName = approver is null ? null : $"{approver.FirstName} {approver.LastName}",
        ApprovedAt = adjustment.ApprovedAt
    };
}
