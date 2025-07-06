using Core.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            var repoConfig = new RepositoryConfiguration
            {
                ConnectionString = connectionString
            };

            services.AddSingleton<RepositoryConfiguration>(repoConfig);

            services.AddScoped<IInvoicesRepository, InvoicesRepository>()
                .AddScoped<IAnalyticsRepository, AnalyticsRepository>();

            return services;
        }

    }
}
