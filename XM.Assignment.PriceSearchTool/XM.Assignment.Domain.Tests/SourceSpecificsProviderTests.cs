using XM.Assignment.Domain.Deserialiazation.Deserializers;
using XM.Assignment.Domain.UriProducers;
using XM.Assignment.Domain.Utiilities;
using Xunit;

namespace XM.Assignment.Domain.Tests;

public class SourceSpecificsProviderTests
{
    [Fact]
    public void SourceSpecificsProvider_ReturnsImplementedDeserializer_IfSuchExists_ForTheSourceName()
    {
        //Arrange
        var sourceSpesificsProvider = new SourceSpecificsProvider();

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
        var sourceSpesificsProvider = new SourceSpecificsProvider();

        //Act
        var defaultDeserializer = sourceSpesificsProvider.GetImplementationOf<IDeserializer>("non-existing-source");

        //Assert
        Assert.IsType<DefaultDeserializer>(defaultDeserializer);

    }

    [Fact]
    public void SourceSpecificsProvider_ReturnsDefaultUriProducer_IfNoUriProducerForTheSourceNameExists()
    {
        //Arrange
        var sourceSpesificsProvider = new SourceSpecificsProvider();

        //Act
        var defaultUriProducer = sourceSpesificsProvider.GetImplementationOf<IUriProducer>("non-existing-source");

        //Assert
        Assert.IsType<DefaultUriProducer>(defaultUriProducer);
    }
}
