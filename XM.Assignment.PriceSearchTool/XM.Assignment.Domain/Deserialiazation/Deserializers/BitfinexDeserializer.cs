using System.Text.Json;
using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Deserialiazation.Deserializers;

public class BitfinexDeserializer : DefaultDeserializer
{
    public override async Task<PriceLogEntry?> DeserializeJsonAsync(Stream jsonStream)
    {
        var bitfinexResponseModel = await JsonSerializer.DeserializeAsync<BitfinexResponseModel>(jsonStream, _jsonSerializerOptions);

        if (bitfinexResponseModel is null)
            return null;

        return new PriceLogEntry
        {
            Price = bitfinexResponseModel.Last_Price,
            TimeStamp = Convert.ToUInt32(bitfinexResponseModel.TimeStamp)
        };
    }

    private class BitfinexResponseModel
    {
        public decimal Last_Price { get; set; }
        public double TimeStamp { get; set; }
    }
}
