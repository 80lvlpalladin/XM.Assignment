using System.Text.Json;
using XM.Assignment.Domain.Deserializers;
using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Deserialiazation.Deserializers
{
    public class DefaultDeserializer : IDeserializer
    {
        public string SourceName => "default";

        public async Task<PriceLogEntry?> DeserializeJsonAsync(Stream jsonStream)
        {
            return await JsonSerializer.DeserializeAsync<PriceLogEntry>(jsonStream);
        }
    }
}
