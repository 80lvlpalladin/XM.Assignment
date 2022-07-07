using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Domain.Deserializers;
using XM.Assignment.Domain.Services;

namespace XM.Assignment.Domain.Extensions.DI
{
    public static class ServiceRegistrant
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddSingleton<IDeserializerProvider, DeserializerProvider>();
            services.AddSingleton<IPriceFetcherService, PriceFetcherService>();
            return services;
        }
    }
}
