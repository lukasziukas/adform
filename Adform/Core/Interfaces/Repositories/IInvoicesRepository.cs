using Core.Common;
using Dtos;

namespace Core.Interfaces.Repositories
{
    public interface IInvoicesRepository
    {
        Task<DBResult<IEnumerable<InvoiceDetailsDto>>> GetInvoiceDetails(int invoiceId, 
            string? productName = null, string? productCategory = null, 
            CancellationToken cancellationToken = default);
    }
}
