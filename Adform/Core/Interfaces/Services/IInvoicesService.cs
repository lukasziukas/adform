using Core.Common;
using Dtos;

namespace Core.Interfaces.Services
{
    public interface IInvoicesService
    {
        Task<Result<IEnumerable<InvoiceDetailsDto>>> GetInvoiceProducts(int invoiceId, 
            string? productName = null, string? productCategory = null,
            CancellationToken cancellationToken = default);
    }
}
