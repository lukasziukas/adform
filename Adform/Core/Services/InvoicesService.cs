using Core.Common;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Dtos;

namespace Core.Services
{
    internal class InvoicesService : IInvoicesService
    {
        private readonly IInvoicesRepository _invoicesRepository;

        public InvoicesService(IInvoicesRepository invoicesRepository)
        {
            _invoicesRepository = invoicesRepository;
        }

        public async Task<Result<IEnumerable<InvoiceDetailsDto>>> GetInvoiceProducts(int invoiceId, 
            string? productName = null, string? productCategory = null,
            CancellationToken cancellationToken = default)
        {
            var invoicesResult = await _invoicesRepository.GetInvoiceDetails(invoiceId, productName, productCategory, cancellationToken);

            if(invoicesResult.IsSuccess)
            {
                return invoicesResult.Data.ToList();
            }
            else
            {
                return $"Failed to retrieve invoice details for invoice id: {invoiceId}, error: {invoicesResult.DBError}";
            }
        }
    }
}
