using System.Text.Json;
using XM.Assignment.Domain.Deserializers;
using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Deserialiazation.Deserializers
{
    public class BitfinexDeserializer : IDeserializer
    {
        public async Task<PriceLogEntry?> DeserializeJsonAsync(Stream jsonStream)
        {
            var bitfinexResponseModel = await JsonSerializer.DeserializeAsync<BitfinexResponseModel>(jsonStream);

            if (bitfinexResponseModel is null)
                return null;

            return new PriceLogEntry(bitfinexResponseModel.Last_Price, bitfinexResponseModel.TimeStamp);
        }

        private class BitfinexResponseModel
        {
            public decimal Last_Price { get; set; }
            public DateTime TimeStamp { get; set; }
        }
    }
}
