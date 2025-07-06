using Core.Common;
using Dtos;

namespace Core.Interfaces.Repositories
{
    public interface IAnalyticsRepository
    {
        Task<DBResult<IEnumerable<DistributionByCityDto>>> GetOrdersByCityDistribution(string? cityName = null, 
            string? orderBy = null, Order? order = null, 
            CancellationToken cancellationToken = default);
    }
}
