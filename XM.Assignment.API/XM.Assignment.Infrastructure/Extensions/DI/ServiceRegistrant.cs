using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Infrastructure.Configuration;
using XM.Assignment.Infrastructure.Datastore;
using XM.Assignment.Infrastructure.Providers;

namespace XM.Assignment.Infrastructure.Extensions.DI;

public static class ServiceRegistrant
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<ISourcesProvider, ConfigSourcesProvider>();
        services.AddSingleton<IPriceLogDatastore, PriceLogDatastore>();

        return services;
    }

    public static IServiceCollection AddConfiguration(this IServiceCollection services)
    {
        var confBuilder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false);
        services.Configure<AppSettings>(confBuilder.Build());
        return services;
    }
}
