using EnterpriseManagement.Application.DTOs;
using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Domain.Entities.HR;
using EnterpriseManagement.Domain.Enums;

namespace EnterpriseManagement.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return employees.Select(ToDto);
    }

    public async Task<EmployeeDto?> GetByCodeAsync(string employeeCode)
    {
        var employee = await _employeeRepository.GetByEmployeeCodeAsync(employeeCode);
        return employee is null ? null : ToDto(employee);
    }

    public async Task<EmployeeDto> CreateAsync(CreateEmployeeRequest request)
    {
        var employee = new Employee
        {
            EmployeeCode = await GenerateUniqueEmployeeCodeAsync(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            DepartmentId = request.DepartmentId,
            PositionId = request.PositionId,
            ManagerId = request.ManagerId,
            HireDate = request.HireDate,
            EmploymentStatus = EmploymentStatus.Probation,
            CreatedAt = DateTime.UtcNow
        };

        await _employeeRepository.AddAsync(employee);
        await _employeeRepository.SaveChangesAsync();

        // employee.Id được EF Core tự gán sau SaveChangesAsync() — chỉ dùng nội bộ để refetch kèm navigation, không lộ ra ngoài API.
        var created = await _employeeRepository.GetByIdAsync(employee.Id);
        return ToDto(created!);
    }

    private const string CodeAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private const int CodeLength = 8;

    private async Task<string> GenerateUniqueEmployeeCodeAsync()
    {
        string code;
        do
        {
            code = string.Create(CodeLength, Random.Shared, (span, random) =>
            {
                for (var i = 0; i < span.Length; i++)
                {
                    span[i] = CodeAlphabet[random.Next(CodeAlphabet.Length)];
                }
            });
        }
        while (await _employeeRepository.GetByEmployeeCodeAsync(code) is not null);

        return code;
    }

    private static EmployeeDto ToDto(Employee employee) => new()
    {
        EmployeeCode = employee.EmployeeCode,
        FullName = $"{employee.FirstName} {employee.LastName}",
        DepartmentName = employee.Department.DepartmentName,
        PositionName = employee.Position.PositionName,
        EmploymentStatus = employee.EmploymentStatus.ToString(),
        HireDate = employee.HireDate
    };
}
