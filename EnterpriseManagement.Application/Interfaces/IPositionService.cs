using EnterpriseManagement.Application.DTOs;

namespace EnterpriseManagement.Application.Interfaces;

public interface IPositionService
{
    Task<IEnumerable<PositionDto>> GetAllAsync();
    Task<PositionDto?> GetByCodeAsync(string positionCode);
    Task<PositionDto> CreateAsync(CreatePositionRequest request);
    Task<PositionDto> SetStandardSalaryAsync(string positionCode, decimal standardSalary);
}
