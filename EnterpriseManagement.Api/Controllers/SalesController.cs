using EnterpriseManagement.Application.DTOs;
using EnterpriseManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;

    public SalesController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpPost]
    public async Task<ActionResult<SaleDto>> Submit(SubmitSaleRequest request)
    {
        try
        {
            var result = await _saleService.SubmitAsync(request);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("pending")]
    public async Task<ActionResult<IEnumerable<SaleDto>>> GetPending()
    {
        var pending = await _saleService.GetPendingAsync();
        return Ok(pending);
    }

    [HttpGet("history")]
    public async Task<ActionResult<IEnumerable<SaleDto>>> GetHistory()
    {
        var history = await _saleService.GetHistoryAsync();
        return Ok(history);
    }

    [HttpPut("{id}/approve")]
    public async Task<ActionResult<SaleDto>> Approve(long id, ApprovalRequest request)
    {
        try
        {
            var result = await _saleService.ApproveAsync(id, request.ApproverEmployeeCode);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}/reject")]
    public async Task<ActionResult<SaleDto>> Reject(long id, ApprovalRequest request)
    {
        try
        {
            var result = await _saleService.RejectAsync(id, request.ApproverEmployeeCode);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
