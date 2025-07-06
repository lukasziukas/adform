using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Adform.Controllers
{
    public class InvoicesController : AdformControllerBase
    {
        private readonly IInvoicesService _invoicesService;

        public InvoicesController(IInvoicesService invoicesService)
        {
            _invoicesService = invoicesService;
        }

        [HttpGet("{invoiceId:int}")]
        public async Task<IActionResult> GetInvoiceProducts([FromRoute] int invoiceId,
            [FromQuery] string? productName = null, [FromQuery] string? productCategory = null,
            CancellationToken cancellationToken = default)
        {
            var result = await _invoicesService.GetInvoiceProducts(invoiceId, productName, productCategory, cancellationToken);

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result);
        }
    }
}
