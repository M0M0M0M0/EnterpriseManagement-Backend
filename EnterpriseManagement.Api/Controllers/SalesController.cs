using EnterpriseManagement.Application.DTOs;
using EnterpriseManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly ICurrentUserService _currentUserService;

    public SalesController(ISaleService saleService, ICurrentUserService currentUserService)
    {
        _saleService = saleService;
        _currentUserService = currentUserService;
    }

    [HttpPost]
    [Authorize(Roles = "EMPLOYEE")]
    public async Task<ActionResult<SaleDto>> Submit(SubmitSaleRequest request)
    {
        try
        {
            var result = await _saleService.SubmitAsync(request, _currentUserService.EmployeeCode!);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("mine")]
    [Authorize(Roles = "EMPLOYEE")]
    public async Task<ActionResult<IEnumerable<SaleDto>>> GetMine()
    {
        var sales = await _saleService.GetByEmployeeAsync(_currentUserService.EmployeeCode!);
        return Ok(sales);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "EMPLOYEE")]
    public async Task<ActionResult<SaleDto>> Update(long id, UpdateSaleRequest request)
    {
        try
        {
            var result = await _saleService.UpdateAsync(id, _currentUserService.EmployeeCode!, request);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("pending")]
    [Authorize(Roles = "MANAGER,ADMIN")]
    public async Task<ActionResult<IEnumerable<SaleDto>>> GetPending()
    {
        var pending = await _saleService.GetPendingAsync();
        return Ok(pending);
    }

    [HttpGet("history")]
    [Authorize(Roles = "MANAGER,ADMIN")]
    public async Task<ActionResult<IEnumerable<SaleDto>>> GetHistory()
    {
        var history = await _saleService.GetHistoryAsync();
        return Ok(history);
    }

    [HttpPut("{id}/approve")]
    [Authorize(Roles = "MANAGER,ADMIN")]
    public async Task<ActionResult<SaleDto>> Approve(long id)
    {
        try
        {
            var result = await _saleService.ApproveAsync(id, _currentUserService.EmployeeCode!);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}/reject")]
    [Authorize(Roles = "MANAGER,ADMIN")]
    public async Task<ActionResult<SaleDto>> Reject(long id)
    {
        try
        {
            var result = await _saleService.RejectAsync(id, _currentUserService.EmployeeCode!);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
