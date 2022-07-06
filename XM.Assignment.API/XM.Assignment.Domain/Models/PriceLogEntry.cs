using XM.Assignment.Domain.Models.Enums;

namespace XM.Assignment.Domain.Models
{
    public record BitcoinPriceLogEntry(decimal Price, DateTime TimeStamp, Currency Currency);
}
