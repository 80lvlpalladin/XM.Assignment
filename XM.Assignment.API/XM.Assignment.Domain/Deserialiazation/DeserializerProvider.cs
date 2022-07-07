using System.Reflection;
using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Domain.Deserialiazation.Deserializers;

namespace XM.Assignment.Domain.Deserializers
{
    public class DeserializerProvider : IDeserializerProvider
    {
        private readonly ISourcesProvider _sourcesProvider;

        public DeserializerProvider(ISourcesProvider sourcesProvider)
        {
            _sourcesProvider = sourcesProvider;
        }

        //TODO make async
        public IDeserializer GetBySourceName(string sourceName)
        {
            if (_sourcesProvider.GetByName(sourceName) is null)
                return new DefaultDeserializer();

            var allTypes = Assembly.GetExecutingAssembly().GetTypes();
            var allIDeserializerTypes = allTypes.Where(type => type.GetInterfaces().Contains(typeof(IDeserializer)));
            var typeContainingSourcename = allIDeserializerTypes.FirstOrDefault(type => type.Name.ToLower().Contains(sourceName.ToLower()));

            if (typeContainingSourcename == null)
                return new DefaultDeserializer();

            return (IDeserializer) Activator.CreateInstance(typeContainingSourcename);
        }
    }
}
