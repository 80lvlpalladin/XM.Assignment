using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Domain.Deserialiazation.Deserializers;
using XM.Assignment.Domain.Models;
using XM.Assignment.Domain.UriProducers;
using XM.Assignment.Domain.Utiilities;

namespace XM.Assignment.Domain.Services;

internal class PriceFetcherService : IPriceFetcherService
{
    private readonly HttpClient _httpClient;
    private readonly IDomainConfigurationProvider _domainConfigurationProvider;
    private readonly IPriceHistoryDatastore _priceHistoryDatastore;
    private readonly ISourceSpecificsProvider _sourceSpecificsProvider;

    public PriceFetcherService(
        IHttpClientFactory httpClientFactory,
        IDomainConfigurationProvider domainConfigurationProvider, 
        IPriceHistoryDatastore priceHistoryDatastore,
        ISourceSpecificsProvider sourceSpecificsProvider)
    {
        _httpClient = httpClientFactory.CreateClient();
        _domainConfigurationProvider = domainConfigurationProvider;
        _priceHistoryDatastore = priceHistoryDatastore;
        _sourceSpecificsProvider = sourceSpecificsProvider;
    }

    public async Task<PriceLogEntry?> GetCurrentPriceAsync(string sourceName, string currency)
    {
        var source = _domainConfigurationProvider.GetSourceByName(sourceName);

        if (source is null)
            return null;

        var deserializer = _sourceSpecificsProvider.GetImplementationOf<IDeserializer>(sourceName);
        var uriProducer = _sourceSpecificsProvider.GetImplementationOf<IUriProducer>(sourceName);

        var uri = uriProducer.Produce(source.Uri, currency);
        var currentPriceJsonStream = await _httpClient.GetStreamAsync(uri);

        var priceLogEntry = await deserializer.DeserializeJsonAsync(currentPriceJsonStream);

        if (priceLogEntry is null)
            return null;

        _priceHistoryDatastore.Save(sourceName, currency, priceLogEntry);

        return priceLogEntry;
    }
}
