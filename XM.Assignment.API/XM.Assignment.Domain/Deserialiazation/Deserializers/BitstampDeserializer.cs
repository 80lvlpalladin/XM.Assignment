using System.Text.Json;
using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Deserialiazation.Deserializers;

public class BitstampDeserializer : DefaultDeserializer
{
    public override async Task<PriceLogEntry?> DeserializeJsonAsync(Stream jsonStream)
    {
        var bitstampResponseModel = await JsonSerializer.DeserializeAsync<BitstampResponseModel>(jsonStream, _jsonSerializerOptions);

        if (bitstampResponseModel is null)
            return null;

        return new PriceLogEntry
        {
            Price = bitstampResponseModel.Last,
            TimeStamp = bitstampResponseModel.TimeStamp
        };
    }

    private class BitstampResponseModel
    {
        public decimal Last { get; set; }
        public uint TimeStamp { get; set; }
    }
}
