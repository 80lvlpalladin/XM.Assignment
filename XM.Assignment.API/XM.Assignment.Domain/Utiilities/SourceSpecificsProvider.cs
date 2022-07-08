using System.Reflection;
using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Utiilities;

public class SourceSpecificsProvider : ISourceSpecificsProvider
{
    private readonly IEnumerable<Source> _sources;

    public SourceSpecificsProvider(ISourcesProvider sourcesProvider)
    {
        _sources = sourcesProvider.GetAll();
    }

    public T GetImplementationOf<T>(string sourceName) where T : class
    { 
        if (!typeof(T).IsInterface)
            throw new ArgumentException("T must be an interface");

        var allTypes = Assembly.GetExecutingAssembly().GetTypes();
        var allTypesImplementingT = allTypes.Where(type => type.GetInterfaces().Contains(typeof(T)));
        var defaultImplementation = allTypesImplementingT
            .First(type => type.Name.ToLower().Contains($"default{typeof(T).Name.Substring(1).ToLower()}"));
        var implementationForSource =
            allTypesImplementingT.FirstOrDefault(type => type.Name.ToLower().Contains(sourceName.ToLower()));

        return implementationForSource is null ? 
            (T) Activator.CreateInstance(defaultImplementation) :
            (T) Activator.CreateInstance(implementationForSource);
    }
}
