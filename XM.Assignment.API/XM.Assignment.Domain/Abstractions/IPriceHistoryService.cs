using XM.Assignment.Domain.Models;
using XM.Assignment.Domain.Models.Enums;

namespace XM.Assignment.Domain.Abstractions;

public interface IPriceHistoryService
{
    public IEnumerable<PriceHistoryResponseModel> GetAllHistoryForCurrency(string currency, OrderingOptions? orderingOptions = null);
}
