using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Services
{
    internal class PriceFetcherService
    {
        private readonly HttpClient _httpClient;
        private readonly IEnumerable<Source> _sources;
        private readonly IPriceLogDatastore _priceLogDatastore;

        public PriceFetcherService(
            IHttpClientFactory httpClientFactory, 
            ISourcesProvider sourcesProvider, 
            IPriceLogDatastore priceLogDatastore)
        {
            _httpClient = httpClientFactory.CreateClient();
            _sources = sourcesProvider.GetAll();
            _priceLogDatastore = priceLogDatastore;
        }
    }
}
