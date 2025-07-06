using Core.Interfaces.Services;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class CoreInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IInvoicesService, InvoicesService>()
                .AddScoped<IOrdersAnalyticsService, OrdersAnalyticsService>();

            return services;
        }

    }
}
