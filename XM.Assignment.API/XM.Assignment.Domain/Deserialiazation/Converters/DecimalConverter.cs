using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Assignment.Domain.Deserialiazation.Converters
{
    public class DecimalConverter : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(((decimal)value).ToString("0.00"));
        }
    }
}
