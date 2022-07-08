using Microsoft.Extensions.Options;
using XM.Assignment.Domain.Deserialiazation.Deserializers;
using XM.Assignment.Domain.Models;
using XM.Assignment.Domain.UriProducers;
using XM.Assignment.Domain.Utiilities;
using XM.Assignment.Infrastructure.Configuration;
using XM.Assignment.Infrastructure.Providers;

namespace XM.Assignment.Domain.Tests;

public class SourceSpecificsProviderTests
{
    private IEnumerable<Source> _testSources = new[]
        {
            new Source{Name = "Bitstamp", Uri = new Uri("http://someUri")},
            new Source{Name = "Bitfinex", Uri = new Uri("http://someUri")}
        };

    [Fact]
    public void SourceSpecificsProvider_ReturnsImplementedDeserializer_IfSuchExists_ForTheSourceName()
    {
        //Arrange
        var sourceSpesificsProvider = CreateSourceSpecificsProvider();

        //Act
        var bitfinexDeserializer = sourceSpesificsProvider.GetImplementationOf<IDeserializer>("bitfinex");
        var bitstampDeserializer = sourceSpesificsProvider.GetImplementationOf<IDeserializer>("bitstamp");


        //Assert
        Assert.IsType<BitfinexDeserializer>(bitfinexDeserializer);
        Assert.IsType<BitstampDeserializer>(bitstampDeserializer);

    }

    [Fact]
    public void SourceSpecificsProvider_ReturnsDefaultDeserializer_IfNoSerializerForTheSourceNameExists()
    {
        //Arrange
        var sourceSpesificsProvider = CreateSourceSpecificsProvider();

        //Act
        var defaultDeserializer = sourceSpesificsProvider.GetImplementationOf<IDeserializer>("non-existing-source");

        //Assert
        Assert.IsType<DefaultDeserializer>(defaultDeserializer);

    }

    [Fact]
    public void SourceSpecificsProvider_ReturnsDefaultUriProducer_IfNoUriProducerForTheSourceNameExists()
    {
        //Arrange
        var sourceSpesificsProvider = CreateSourceSpecificsProvider();

        //Act
        var defaultUriProducer = sourceSpesificsProvider.GetImplementationOf<IUriProducer>("non-existing-source");

        //Assert
        Assert.IsType<DefaultUriProducer>(defaultUriProducer);
    }

    private ISourceSpecificsProvider CreateSourceSpecificsProvider()
    {
        var options = Options.Create(new AppSettings { Sources = _testSources });
        var sourcesProvider = new ConfigSourcesProvider(options);
        return new SourceSpecificsProvider(sourcesProvider);
    }
}
