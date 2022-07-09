using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Domain.Models;
using XM.Assignment.Domain.Models.Enums;

namespace XM.Assignment.Domain.Services;

public class PriceHistoryService : IPriceHistoryService
{
    private readonly IPriceHistoryDatastore _priceLogDatastore;
    private readonly IDomainConfigurationProvider _domainConfigurationProvider;

    public PriceHistoryService(
        IPriceHistoryDatastore priceLogDatastore, 
        IDomainConfigurationProvider domainConfigurationProvider)
    {
        _priceLogDatastore = priceLogDatastore;
        _domainConfigurationProvider = domainConfigurationProvider;
    }

    public IEnumerable<PriceLogEntryDto> GetHistoryForCurrency(
        string currency, 
        OrderBy? orderByOptions = null,
        FilteringOptions? filteringOptions = null)
    {
        var result = new List<PriceLogEntryDto>();

        var sourcesToGet = GetFilteredSourcesList(filteringOptions);

        foreach (var source in sourcesToGet)
        {
            var resultsFromDatastore = 
                _priceLogDatastore.GetForSourceAndCurrency(source.Name, currency, GetFilterTimestamp(filteringOptions));

            if (resultsFromDatastore is not null)
                result.AddRange(resultsFromDatastore.Select(dsr =>
                new PriceLogEntryDto
                {
                    Price = dsr.Price,
                    TimeStamp = dsr.TimeStamp,
                    Source = source.Name
                }));
        }

        Order(result, orderByOptions);

        return result;
    }

    private void Order(List<PriceLogEntryDto> models, OrderBy? options)
    {
        if (options is null)
            return;

        Comparison<PriceLogEntryDto> comparison = options switch
        {
            OrderBy.Date => (m1, m2) => m2.TimeStamp.CompareTo(m1.TimeStamp),
            OrderBy.Price => (m1, m2) => m2.Price.CompareTo(m1.Price),
            _ => (m1, m2) => m2.TimeStamp.CompareTo(m1.TimeStamp),
        };

        models.Sort(comparison);
    }

    private uint? GetFilterTimestamp(FilteringOptions? options)
    {
        if (options is not null &&
            options.FilterBy is FilterBy.Date &&
            uint.TryParse(options.FilterValue, out uint parsedValue))
            return parsedValue;
        else return null;
    }

    private IEnumerable<Source> GetFilteredSourcesList(FilteringOptions? options)
    {
        if (options is not null && options.FilterBy is FilterBy.Source && !string.IsNullOrWhiteSpace(options?.FilterValue))
        {
            var sourceByName = _domainConfigurationProvider.GetSourceByName(options.FilterValue);

            return sourceByName is null ? Array.Empty<Source>() : new[] { sourceByName };
        }
        else
            return _domainConfigurationProvider.GetAllSources();
    }
}
