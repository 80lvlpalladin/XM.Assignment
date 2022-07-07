﻿using Microsoft.Extensions.Options;
using XM.Assignment.Domain.Abstractions;
using XM.Assignment.Domain.Models;
using XM.Assignment.Infrastructure.Configuration;

namespace XM.Assignment.Infrastructure.Providers;

public class ConfigSourcesProvider : ISourcesProvider
{
    private readonly IEnumerable<Source> _sources;

    public ConfigSourcesProvider(IOptions<AppSettings> appSettings)
    {
        _sources = appSettings.Value.Sources;
    }

    public IEnumerable<Source> GetAll() => _sources;

    public Source? GetByName(string name) => 
        _sources.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
}
