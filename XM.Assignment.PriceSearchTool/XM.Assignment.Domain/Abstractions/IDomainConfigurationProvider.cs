using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Abstractions;

public interface IDomainConfigurationProvider
{
    public IEnumerable<Source> GetAllSources();
    Source? GetSourceByName(string name);
    public int? GetDatastoreExpirationTime();
}
