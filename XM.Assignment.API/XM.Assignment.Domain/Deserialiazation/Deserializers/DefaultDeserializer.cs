using System.Text.Json;
using System.Text.Json.Serialization;
using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Deserialiazation.Deserializers;

public class DefaultDeserializer : IDeserializer
{
    protected JsonSerializerOptions _jsonSerializerOptions => new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    public virtual async Task<PriceLogEntry?> DeserializeJsonAsync(Stream jsonStream)
    {
        return await JsonSerializer.DeserializeAsync<PriceLogEntry>(jsonStream, _jsonSerializerOptions);
    }
}
