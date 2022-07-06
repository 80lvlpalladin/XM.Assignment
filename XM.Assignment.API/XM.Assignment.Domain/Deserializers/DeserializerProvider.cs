using System.Reflection;
using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Domain.Deserializers.Implementations;

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

            var deserializerType = 
                Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(type => 
                    type.IsAssignableFrom(typeof(IDeserializer)) && 
                    type.Name.ToLower().Contains(sourceName.ToLower()));

            if (deserializerType == null)
                return new DefaultDeserializer();


            return (IDeserializer) Activator.CreateInstance(deserializerType);
        }
    }
}
