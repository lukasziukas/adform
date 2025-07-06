using Core.Common;
using Dtos;

namespace Core.Interfaces.Services
{
    public interface IOrdersAnalyticsService
    {
        Task<Result<IEnumerable<DistributionByCityDto>>> GetOrdersByCity(string? cityName = null, 
            string? orderBy = null, Order? order = null, 
            CancellationToken cancellationToken = default);
    }
}
