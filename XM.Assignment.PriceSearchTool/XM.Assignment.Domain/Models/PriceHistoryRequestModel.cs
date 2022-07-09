using XM.Assignment.Domain.Models.Enums;

namespace XM.Assignment.Domain.Models;

public class PriceHistoryRequestModel
{
    public FilteringOptions? Filtering { get; set; }

    public OrderBy? OrderBy { get; set; }
}
