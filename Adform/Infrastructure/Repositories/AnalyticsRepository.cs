using Core.Common;
using Core.Interfaces.Repositories;
using Dtos;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    internal class AnalyticsRepository : RepositoryBase, IAnalyticsRepository
    {
        public AnalyticsRepository(RepositoryConfiguration repositoryConfiguration, ILogger<AnalyticsRepository> logger) : base(repositoryConfiguration.ConnectionString, logger)
        {
        }

        public async Task<DBResult<IEnumerable<DistributionByCityDto>>> GetOrdersByCityDistribution(string? cityName = null, string? orderBy = null, Order? order = null, CancellationToken cancellationToken = default)
        {
            const string sql = """
                SELECT
                    C.CITY AS CustomerCity,
                    COUNT(O.ORDER_ID) AS NumberOfOrders,
                    SUM(OD.ORDER_AMOUNT) AS TotalAmount
                FROM
                    ORDERS O
                    INNER JOIN CUSTOMERS C ON O.CUSTOMER_ID = C.CUSTOMER_ID
                    INNER JOIN (
                        SELECT
                            OD.ORDER_ID,
                            SUM(OD.QUANTITY * P.PRICE) ORDER_AMOUNT
                        FROM
                            ORDER_DETAILS OD
                            INNER JOIN PRODUCTS P ON OD.PRODUCT_ID = P.PRODUCT_ID
                        GROUP BY
                            OD.ORDER_ID
                    ) AS OD ON O.ORDER_ID = OD.ORDER_ID
                    WHERE
                    	(@cityName IS NULL OR C.CITY = @cityName)
                GROUP BY
                    C.CITY
                ORDER BY
                """;

            var orderSql = sql;
            var orderDirection = order == Order.Descending ? "desc" : "";

            switch (orderBy?.ToLower())
            {
                case "city":
                    orderSql += $" C.CITY {orderDirection};";
                    break;
                case "number_of_orders":
                    orderSql += $" COUNT(O.ORDER_ID) {orderDirection};";
                    break;
                case "total_amount":
                    orderSql += $" SUM(OD.ORDER_AMOUNT) {orderDirection};";
                    break;
                default:
                    orderSql += " C.CITY;"; // Default order by city if no valid orderBy is provided
                    break;
            }

            return await ExecuteQuery<DistributionByCityDto>(orderSql, new
            {
                cityName=cityName
            });
        }
    }
}
