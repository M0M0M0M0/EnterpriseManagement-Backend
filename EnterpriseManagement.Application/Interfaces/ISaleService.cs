using EnterpriseManagement.Application.DTOs;

namespace EnterpriseManagement.Application.Interfaces;

public interface ISaleService
{
    Task<SaleDto> SubmitAsync(SubmitSaleRequest request);
    Task<IEnumerable<SaleDto>> GetPendingAsync();
    Task<IEnumerable<SaleDto>> GetHistoryAsync();
    Task<SaleDto> ApproveAsync(long saleId, string approverEmployeeCode);
    Task<SaleDto> RejectAsync(long saleId, string approverEmployeeCode);
}
