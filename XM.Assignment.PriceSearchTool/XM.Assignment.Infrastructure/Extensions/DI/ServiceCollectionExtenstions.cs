using Microsoft.Extensions.DependencyInjection;
using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Infrastructure.Datastore;
using XM.Assignment.Infrastructure.Providers;

namespace XM.Assignment.Infrastructure.Extensions.DI;

public static partial class ServiceCollectionExtenstions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IDomainConfigurationProvider, AppSettingsProvider>();
        services.AddSingleton<IPriceHistoryDatastore, PriceHistoryDatastore>();

        return services;
    }
}
