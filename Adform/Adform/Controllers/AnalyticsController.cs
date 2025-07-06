using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Adform.Controllers
{
    public class AnalyticsController : AdformControllerBase
    {
        private readonly IOrdersAnalyticsService _ordersAnalyticsService;

        public AnalyticsController(IOrdersAnalyticsService ordersAnalyticsService)
        {
            _ordersAnalyticsService = ordersAnalyticsService;
        }

        [HttpGet("OrdersByCity")]
        public async Task<IActionResult> GetOrdersByCity(
            [FromQuery] string? city = null,
            [FromQuery] string? orderBy = null,
            [FromQuery] string? orderDirection = null,
            CancellationToken cancellationToken = default)
        {
            //TODO: simplify order parsing logic
            var result = await _ordersAnalyticsService.GetOrdersByCity(city, orderBy, orderDirection == "desc" ? Core.Common.Order.Descending : Core.Common.Order.Ascending, cancellationToken);

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result);
        }
    }
}
