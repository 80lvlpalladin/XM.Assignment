using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Assignment.Domain.Deserialiazation.Deserializers;
using XM.Assignment.Domain.Deserializers;
using XM.Assignment.Domain.Models;
using XM.Assignment.Infrastructure.Configuration;
using XM.Assignment.Infrastructure.Providers;

namespace XM.Assignment.Domain.Tests;

public class DeserializerProviderTests
{
    private IEnumerable<Source> _testSources = new[]
        {
            new Source("Bitstamp", new Uri("http://someUri")),
            new Source("Bitfinex", new Uri("http://someUri"))
        };

    [Fact]
    public void DeserializerProvider_ReturnsImplementedDeserializer_IfSuchExists_ForTheSourceName()
    {
        //Arrange
        var deserializerProvider = CreateDeserializerProvider();

        //Act
        var bitfinexDeserializer = deserializerProvider.GetBySourceName("bitfinex");
        var bitstampDeserializer = deserializerProvider.GetBySourceName("bitstamp");


        //Assert
        Assert.IsType<BitfinexDeserializer>(bitfinexDeserializer);
        Assert.IsType<BitstampDeserializer>(bitstampDeserializer);

    }

    [Fact]
    public void DeserializerProvider_ReturnsDefaultDeserializer_IfNoSerializerForTheSourceNameExists()
    {
        //Arrange
        var deserializerProvider = CreateDeserializerProvider();

        //Act
        var defaultDeserializer = deserializerProvider.GetBySourceName("non-existing-source");


        //Assert
        Assert.IsType<DefaultDeserializer>(defaultDeserializer);

    }

    private IDeserializerProvider CreateDeserializerProvider()
    {
        var options = Options.Create(new AppSettings { Sources = _testSources });
        var sourcesProvider = new ConfigSourcesProvider(options);
        return new DeserializerProvider(sourcesProvider);
    }
}
