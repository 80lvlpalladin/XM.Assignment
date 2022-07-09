using XM.Assignment.Domain.Models;
using XM.Assignment.Domain.Models.Enums;

namespace XM.Assignment.Domain.Abstractions;

public interface IPriceHistoryService
{
    public IEnumerable<PriceLogEntryDto> GetHistoryForCurrency(
        string currency,
        OrderBy? orderByOptions = null,
        FilteringOptions? filteringOptions = null);
}
