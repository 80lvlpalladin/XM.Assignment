using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Domain.Models;
using XM.Assignment.Domain.Models.Enums;

namespace XM.Assignment.Domain.Services;

public class PriceHistoryService : IPriceHistoryService
{
    private readonly IPriceLogDatastore _priceLogDatastore;
    private readonly IEnumerable<Source> _sources;

    public PriceHistoryService(IPriceLogDatastore priceLogDatastore, ISourcesProvider sourcesProvider)
    {
        _priceLogDatastore = priceLogDatastore;
        _sources = sourcesProvider.GetAll();
    }

    public IEnumerable<PriceHistoryResponseModel> GetAllHistoryForCurrency(string currency, OrderingOptions? orderingOptions = null)
    {
        var result = new List<PriceHistoryResponseModel>();
        foreach(var source in _sources)
        {
            var resultsFromDatastore = _priceLogDatastore.GetForSourceAndCurrency(source.Name, currency);
            if (resultsFromDatastore is not null)
                result.AddRange(resultsFromDatastore.Select(dsr =>
                new PriceHistoryResponseModel
                {
                    Price = dsr.Price,
                    TimeStamp = dsr.TimeStamp,
                    Source = source.Name
                }));
        }

        if (orderingOptions is not null) 
            Order(result, orderingOptions.Value);

        return result;
    }

    private void Order(List<PriceHistoryResponseModel> models, OrderingOptions options)
    {
        Comparison<PriceHistoryResponseModel> comparison = options switch
        {
            OrderingOptions.Date => (m1, m2) => m2.TimeStamp.CompareTo(m1.TimeStamp),
            OrderingOptions.Price => (m1, m2) => m2.Price.CompareTo(m1.Price),
            _ => (m1, m2) => m2.TimeStamp.CompareTo(m1.TimeStamp),
        };

        models.Sort(comparison);
    }
}
