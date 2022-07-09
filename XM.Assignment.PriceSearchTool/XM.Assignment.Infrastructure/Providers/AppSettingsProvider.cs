using Microsoft.Extensions.Options;
using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Domain.Models;
using XM.Assignment.Infrastructure.Configuration;

namespace XM.Assignment.Infrastructure.Providers;

public class AppSettingsProvider : IDomainConfigurationProvider
{
    private readonly IEnumerable<Source> _sources;
    private readonly int? _datastoreExpirationTimeHours;

    public AppSettingsProvider(IOptions<AppSettings> appSettings)
    {
        _sources = appSettings.Value.Sources;
        _datastoreExpirationTimeHours = appSettings.Value.DatastoreExpirationTimeHours;
    }

    public IEnumerable<Source> GetAllSources() => _sources;

    public Source? GetSourceByName(string name) => 
        _sources.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

    public int? GetDatastoreExpirationTime() => _datastoreExpirationTimeHours;
}
