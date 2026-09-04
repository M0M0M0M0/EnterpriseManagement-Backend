using EnterpriseManagement.Application.Common;
using EnterpriseManagement.Application.DTOs;
using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Domain.Entities.Sales;
using EnterpriseManagement.Domain.Enums;

namespace EnterpriseManagement.Application.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public SaleService(ISaleRepository saleRepository, ICustomerRepository customerRepository, IEmployeeRepository employeeRepository)
    {
        _saleRepository = saleRepository;
        _customerRepository = customerRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<SaleDto> SubmitAsync(SubmitSaleRequest request)
    {
        var employee = await _employeeRepository.GetByEmployeeCodeAsync(request.EmployeeCode)
            ?? throw new InvalidOperationException($"Employee code '{request.EmployeeCode}' not found.");

        var customer = await _customerRepository.GetByCodeAsync(request.CustomerCode)
            ?? throw new InvalidOperationException($"Customer code '{request.CustomerCode}' not found.");

        var sale = new Sale
        {
            SaleCode = await GenerateUniqueSaleCodeAsync(),
            CustomerId = customer.Id,
            EmployeeId = employee.Id,
            Amount = request.Amount,
            OrderDate = DateTime.UtcNow,
            Status = SaleStatus.Pending,
            Note = request.Note,
            CreatedAt = DateTime.UtcNow
        };

        await _saleRepository.AddAsync(sale);
        await _saleRepository.SaveChangesAsync();

        var created = await _saleRepository.GetByCodeAsync(sale.SaleCode);
        return ToDto(created!);
    }

    public async Task<IEnumerable<SaleDto>> GetPendingAsync()
    {
        var sales = await _saleRepository.GetPendingAsync();
        return sales.Select(ToDto);
    }

    public async Task<IEnumerable<SaleDto>> GetHistoryAsync()
    {
        var sales = await _saleRepository.GetAllAsync();
        return sales.Select(ToDto);
    }

    public async Task<SaleDto> ApproveAsync(long saleId, string approverEmployeeCode)
    {
        var sale = await ChangeStatusAsync(saleId, approverEmployeeCode, SaleStatus.Confirmed, "approved");
        return ToDto(sale);
    }

    public async Task<SaleDto> RejectAsync(long saleId, string approverEmployeeCode)
    {
        var sale = await ChangeStatusAsync(saleId, approverEmployeeCode, SaleStatus.Cancelled, "rejected");
        return ToDto(sale);
    }

    private async Task<Sale> ChangeStatusAsync(long saleId, string approverEmployeeCode, SaleStatus newStatus, string action)
    {
        var approver = await _employeeRepository.GetByEmployeeCodeAsync(approverEmployeeCode)
            ?? throw new InvalidOperationException($"Employee code '{approverEmployeeCode}' not found.");

        var sale = await _saleRepository.GetByIdAsync(saleId)
            ?? throw new InvalidOperationException($"Sale {saleId} not found.");

        if (sale.Status != SaleStatus.Pending)
        {
            throw new InvalidOperationException($"Only pending sales can be {action}.");
        }

        sale.Status = newStatus;
        sale.ApprovedBy = approver.Id;
        sale.Approver = approver;
        sale.ApprovedAt = DateTime.UtcNow;
        sale.UpdatedAt = DateTime.UtcNow;

        await _saleRepository.SaveChangesAsync();

        return sale;
    }

    private async Task<string> GenerateUniqueSaleCodeAsync()
    {
        string code;
        do
        {
            code = RandomCodeGenerator.Generate(10);
        }
        while (await _saleRepository.GetByCodeAsync(code) is not null);

        return code;
    }

    private static SaleDto ToDto(Sale sale) => new()
    {
        Id = sale.Id,
        SaleCode = sale.SaleCode,
        CustomerCode = sale.Customer.CustomerCode,
        CustomerName = sale.Customer.CustomerName,
        EmployeeCode = sale.Employee.EmployeeCode,
        EmployeeName = $"{sale.Employee.FirstName} {sale.Employee.LastName}",
        Amount = sale.Amount,
        OrderDate = sale.OrderDate,
        Status = sale.Status.ToString(),
        Note = sale.Note,
        ApproverName = sale.Approver is null ? null : $"{sale.Approver.FirstName} {sale.Approver.LastName}",
        ApprovedAt = sale.ApprovedAt
    };
}
