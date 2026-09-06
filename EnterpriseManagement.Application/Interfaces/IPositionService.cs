using EnterpriseManagement.Application.DTOs;

namespace EnterpriseManagement.Application.Interfaces;

public interface IPositionService
{
    Task<IEnumerable<PositionDto>> GetAllAsync();
    Task<PositionDto?> GetByCodeAsync(string positionCode);
    Task<PositionDto> CreateAsync(CreatePositionRequest request);
    Task<PositionDto> UpdateAsync(string positionCode, UpdatePositionRequest request);
    Task<PositionDto> SetActiveAsync(string positionCode, bool isActive);
    Task<PositionDto> SetStandardSalaryAsync(string positionCode, decimal standardSalary);
}
