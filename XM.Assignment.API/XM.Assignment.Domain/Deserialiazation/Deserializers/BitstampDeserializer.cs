using System.Text.Json;
using XM.Assignment.Domain.Deserializers;
using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Deserialiazation.Deserializers
{
    public class BitstampDeserializer : IDeserializer
    {
        public async Task<PriceLogEntry?> DeserializeJsonAsync(Stream jsonStream)
        {
            var bitstampResponseModel = await JsonSerializer.DeserializeAsync<BitstampResponseModel>(jsonStream);

            if (bitstampResponseModel is null)
                return null;

            return new PriceLogEntry(bitstampResponseModel.Last, bitstampResponseModel.TimeStamp);

        }

        private class BitstampResponseModel
        {
            public decimal Last { get; set; }
            public DateTime TimeStamp { get; set; }
        }
    }
}
