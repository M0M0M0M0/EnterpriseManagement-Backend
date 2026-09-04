using EnterpriseManagement.Application.DTOs;
using EnterpriseManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PositionsController : ControllerBase
{
    private readonly IPositionService _positionService;

    public PositionsController(IPositionService positionService)
    {
        _positionService = positionService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PositionDto>>> GetAll()
    {
        var positions = await _positionService.GetAllAsync();
        return Ok(positions);
    }

    [HttpGet("{positionCode}")]
    public async Task<ActionResult<PositionDto>> GetByCode(string positionCode)
    {
        var position = await _positionService.GetByCodeAsync(positionCode);
        return position is null ? NotFound() : Ok(position);
    }

    [HttpPost]
    public async Task<ActionResult<PositionDto>> Create(CreatePositionRequest request)
    {
        try
        {
            var created = await _positionService.CreateAsync(request);
            return CreatedAtAction(nameof(GetByCode), new { positionCode = created.PositionCode }, created);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
