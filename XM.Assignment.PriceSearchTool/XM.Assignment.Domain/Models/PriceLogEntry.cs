using System.Text.Json.Serialization;
using XM.Assignment.Domain.Deserialiazation.Converters;

namespace XM.Assignment.Domain.Models;

public class PriceLogEntry
{
    /// <summary>
    /// The price at which the last order executed
    /// </summary>
    [JsonConverter(typeof(DecimalConverter))]
    public decimal Price { get; set; }

    /// <summary>
    /// The timestamp at which this information was valid
    /// </summary>
    public uint TimeStamp { get; set; }
};
