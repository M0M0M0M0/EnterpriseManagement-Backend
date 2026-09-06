using EnterpriseManagement.Application.DTOs;

namespace EnterpriseManagement.Application.Interfaces;

public interface IAttendanceService
{
    Task<AttendanceRecordDto> PunchAsync(PunchRequest request);
    Task<IEnumerable<AttendanceRecordDto>> GetHistoryAsync(string employeeCode);
    Task<IEnumerable<AttendanceRecordDto>> GetByDepartmentAsync(string departmentCode, DateOnly startDate, DateOnly endDate);
}
