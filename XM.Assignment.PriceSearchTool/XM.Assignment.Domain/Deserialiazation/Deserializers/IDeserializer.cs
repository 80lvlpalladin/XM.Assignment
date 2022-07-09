using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Deserialiazation.Deserializers;

public interface IDeserializer
{
    public Task<PriceLogEntry?> DeserializeJsonAsync(Stream jsonStream);
}
