using System.Text.Json;
using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Deserializers.Implementations
{
    public class BitstampDeserializer : IDeserializer
    {
        public async Task<PriceLogEntry?> DeserializeJsonAsync(Stream jsonStream)
        {
            var bitstampResponseModel = await JsonSerializer.DeserializeAsync<BitstampResponseModel>(jsonStream);

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
            public DateTime TimeStamp { get; set; }
        }
    }
}
