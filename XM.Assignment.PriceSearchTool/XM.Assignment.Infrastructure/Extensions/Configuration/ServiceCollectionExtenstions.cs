using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XM.Assignment.Infrastructure.Configuration;

namespace XM.Assignment.Infrastructure.Extensions.Configuration;

public static partial class ServiceCollectionExtenstions
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services)
    {
        var confBuilder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false);
        services.Configure<AppSettings>(confBuilder.Build());
        return services;
    }
}
