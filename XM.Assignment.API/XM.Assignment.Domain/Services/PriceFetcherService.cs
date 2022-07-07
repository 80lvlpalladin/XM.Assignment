using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Domain.Deserializers;
using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Services
{
    internal class PriceFetcherService : IPriceFetcherService
    {
        private readonly HttpClient _httpClient;
        private readonly ISourcesProvider _sourcesProvider;
        private readonly IPriceLogDatastore _priceLogDatastore;
        private readonly IDeserializerProvider _deserializerProvider;

        public PriceFetcherService(
            IHttpClientFactory httpClientFactory, 
            ISourcesProvider sourcesProvider, 
            IPriceLogDatastore priceLogDatastore,
            IDeserializerProvider deserializerProvider)
        {
            _httpClient = httpClientFactory.CreateClient();
            _sourcesProvider = sourcesProvider;
            _priceLogDatastore = priceLogDatastore;
            _deserializerProvider = deserializerProvider;
        }

        public async Task<PriceLogEntry?> GetCurrentPriceAsync(string sourceName, string currency)
        {
            var source = _sourcesProvider.GetByName(sourceName);

            if (source is null)
                return null;

            var deserializer = _deserializerProvider.GetBySourceName(sourceName);


            //TODO this logic may be different for each source in the future
            var currentPriceJsonStream = await _httpClient.GetStreamAsync(new Uri(source.Uri, currency));

            var priceLogEntry = await deserializer.DeserializeJsonAsync(currentPriceJsonStream);

            if (priceLogEntry is null)
                return null;

            _priceLogDatastore.Save(sourceName, currency, priceLogEntry);

            return priceLogEntry;
        }
    }
}
