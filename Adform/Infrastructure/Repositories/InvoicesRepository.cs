using Core.Common;
using Core.Interfaces.Repositories;
using Dtos;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    internal class InvoicesRepository : RepositoryBase, IInvoicesRepository
    {
        public InvoicesRepository(RepositoryConfiguration repositoryConfiguration, 
            ILogger<InvoicesRepository> logger) : base(repositoryConfiguration.ConnectionString, logger)
        {
        }

        public async Task<DBResult<IEnumerable<InvoiceDetailsDto>>> GetInvoiceDetails(int invoiceId, 
            string? productName = null, string? productCategory = null, 
            CancellationToken cancellationToken = default)
        {
            const string sql = """
                    SELECT
                        P.CATEGORY AS ProductCategory,
                        P.NAME AS ProductName,
                        OD.Quantity,
                        P.PRICE AS Amount,
                        (OD.QUANTITY * P.PRICE) AS TotalAmount
                    FROM
                        ORDER_DETAILS OD
                        INNER JOIN PRODUCTS P ON OD.PRODUCT_ID = P.PRODUCT_ID
                    WHERE
                        OD.ORDER_ID = @invoiceId AND
                        (@productName IS NULL OR P.NAME LIKE @productName)
                        AND (@productCategory IS NULL OR P.CATEGORY LIKE @productCategory);
                    """;

            return await ExecuteQuery<InvoiceDetailsDto>(sql, new
            {
                invoiceId,
                productName= !string.IsNullOrEmpty(productName) ? $"%{productName}%" : null,
                productCategory= !string.IsNullOrEmpty(productCategory) ? $"%{productCategory}%" : null
            });
        }
    }
}
