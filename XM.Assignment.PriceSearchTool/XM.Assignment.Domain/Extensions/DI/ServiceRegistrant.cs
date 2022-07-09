using Microsoft.Extensions.DependencyInjection;
using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Domain.Models;
using XM.Assignment.Domain.Services;
using XM.Assignment.Domain.Utiilities;

namespace XM.Assignment.Domain.Extensions.DI;

public static class ServiceRegistrant
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<ISourceSpecificsProvider, SourceSpecificsProvider>();
        services.AddSingleton<IPriceFetcherService, PriceFetcherService>();
        services.AddSingleton<IPriceHistoryService, PriceHistoryService>();
        services.AddSingleton<IEqualityComparer<PriceLogEntry>, PriceLogEntryEqualityComparer>();
        return services;
    }
}
