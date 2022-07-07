using XM.Assignment.Domain.Deserialiazation.Deserializers;

namespace XM.Assignment.Domain.Deserializers;

public interface IDeserializerProvider
{
    public IDeserializer GetBySourceName(string sourceName);
}
