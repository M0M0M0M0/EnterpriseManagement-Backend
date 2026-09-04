using EnterpriseManagement.Application.DTOs;
using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Domain.Entities.HR;

namespace EnterpriseManagement.Application.Services;

public class PositionService : IPositionService
{
    private readonly IPositionRepository _positionRepository;

    public PositionService(IPositionRepository positionRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task<IEnumerable<PositionDto>> GetAllAsync()
    {
        var positions = await _positionRepository.GetAllAsync();
        return positions.Select(ToDto);
    }

    public async Task<PositionDto?> GetByCodeAsync(string positionCode)
    {
        var position = await _positionRepository.GetByCodeAsync(positionCode);
        return position is null ? null : ToDto(position);
    }

    public async Task<PositionDto> CreateAsync(CreatePositionRequest request)
    {
        var existing = await _positionRepository.GetByCodeAsync(request.PositionCode);
        if (existing is not null)
        {
            throw new InvalidOperationException($"Position code '{request.PositionCode}' already exists.");
        }

        var position = new Position
        {
            PositionCode = request.PositionCode,
            PositionName = request.PositionName,
            Description = request.Description,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _positionRepository.AddAsync(position);
        await _positionRepository.SaveChangesAsync();

        return ToDto(position);
    }

    private static PositionDto ToDto(Position position) => new()
    {
        PositionCode = position.PositionCode,
        PositionName = position.PositionName,
        Description = position.Description,
        IsActive = position.IsActive
    };
}
