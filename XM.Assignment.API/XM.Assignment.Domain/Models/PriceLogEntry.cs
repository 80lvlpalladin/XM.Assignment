using XM.Assignment.Domain.Models.Enums;

namespace XM.Assignment.Domain.Models
{
    public record PriceLogEntry(decimal Price, DateTime TimeStamp, Currency Currency);
}
