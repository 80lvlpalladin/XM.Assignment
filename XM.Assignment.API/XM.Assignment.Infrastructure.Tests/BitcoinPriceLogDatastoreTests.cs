using Microsoft.Extensions.Options;
using XM.Assignment.Domain.Models;
using XM.Assignment.Domain.Models.Enums;
using XM.Assignment.Infrastructure.Configuration;
using XM.Assignment.Infrastructure.Datastore;
using XM.Assignment.Infrastructure.Providers;

namespace XM.Assignment.Infrastructure.Tests
{
    public class BitcoinPriceLogDatastoreTests
    {
        private IEnumerable<Source> _testSources = new[]
            {
            new Source{Name = "Bitstamp", Uri = new Uri("http://someUri")},
            new Source{Name = "Bitfinex", Uri = new Uri("http://someUri")}
        };

        [Fact]
        public void Datastore_StoresTheExactAmountOfPricelogs_ThatWereSaved_ForExistingSource()
        {
            //Arrange
            var datastore = CreatePriceLogDatastore();

            //Act
            datastore.Save("Bitstamp", "btcusd", new PriceLogEntry { Price = 8.1m, TimeStamp = 100 });
            datastore.Save("Bitstamp", "btcusd", new PriceLogEntry { Price = 8.3m, TimeStamp = 200 });
            datastore.Save("Bitstamp", "btcusd", new PriceLogEntry { Price = 8.4m, TimeStamp = 230 });

            //Assert
            Assert.Equal(3, datastore.GetForSourceAndCurrency("Bitstamp", "btcusd").Count());
        }

        [Fact]
        public void DataStore_GetAll_ReturnsEmptyArray_IfNoLogsWereSaved_ForExistingSource()
        {
            //Arrange
            var datastore = CreatePriceLogDatastore();

            //Act-Assert
            Assert.True(_testSources.All(s => datastore.GetForSourceAndCurrency(s.Name, "btcusd").Count() is 0));

        }

        [Fact]
        public void DataStore_GetAll_ReturnsNull_IfSourceDoesntExist()
        {
            //Arrange
            var datastore = CreatePriceLogDatastore();
            var nonExistingSource = "sdgwag";

            //Act-Assert
            Assert.Null(datastore.GetForSourceAndCurrency(nonExistingSource, "btcusd"));
        }

        private PriceLogDatastore CreatePriceLogDatastore()
        {
            var options = Options.Create(new AppSettings { Sources = _testSources });
            var sourcesProvider = new ConfigSourcesProvider(options);
            return new PriceLogDatastore(sourcesProvider);
        }
    }
}
