using System.Text.Json.Serialization;
using XM.Assignment.Domain.Models.Enums;

namespace XM.Assignment.Domain.Models
{
    public record PriceLogEntry(decimal Price, DateTime TimeStamp);
}
