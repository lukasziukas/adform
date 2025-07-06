using Core.Common;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Dtos;

namespace Core.Services
{
    internal class OrdersAnalyticsService : IOrdersAnalyticsService
    {
        private readonly IAnalyticsRepository _analyticsRepository;

        public OrdersAnalyticsService(IAnalyticsRepository analyticsRepository)
        {
            _analyticsRepository = analyticsRepository;
        }

        public async Task<Result<IEnumerable<DistributionByCityDto>>> GetOrdersByCity(string? cityName = null, 
            string? orderBy = null, Order? order = null, 
            CancellationToken cancellationToken = default)
        {
            var result = await _analyticsRepository.GetOrdersByCityDistribution(cityName, orderBy, order, cancellationToken);

            if (result.IsSuccess)
            {
                return result.Data.ToList();
            }
            else
            {
                return $"Failed to retrieve orders by city distribution: {result.DBError}";
            }
        }
    }
}
