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
                new Source("Bitstamp", new Uri("http://someUri")),
                new Source("Bitfinex", new Uri("http://someUri"))
            };

        [Fact]
        public void Datastore_StoresTheExactAmountOfPricelogs_ThatWereSaved_ForExistingSource()
        {
            //Arrange
            var datastore = CreatePriceLogDatastore();

            //Act
            datastore.Save("Bitstamp", "btcusd", new PriceLogEntry(8.2m, DateTime.Now));
            datastore.Save("Bitstamp", "btcusd", new PriceLogEntry(8.3m, DateTime.Now.AddMinutes(1)));
            datastore.Save("Bitstamp", "btcusd", new PriceLogEntry(8.7m, DateTime.Now.AddMinutes(2)));

            //Assert
            Assert.Equal(3, datastore.GetAll("Bitstamp", "btcusd").Count());
        }

        [Fact]
        public void DataStore_GetAll_ReturnsEmptyArray_IfNoLogsWereSaved_ForExistingSource()
        {
            //Arrange
            var datastore = CreatePriceLogDatastore();

            //Act-Assert
            Assert.True(_testSources.All(s => datastore.GetAll(s.Name, "btcusd").Count() is 0));

        }

        [Fact]
        public void DataStore_GetAll_ReturnsNull_IfSourceDoesntExist()
        {
            //Arrange
            var datastore = CreatePriceLogDatastore();
            var nonExistingSource = "sdgwag";

            //Act-Assert
            Assert.Null(datastore.GetAll(nonExistingSource, "btcusd"));
        }

        private PriceLogDatastore CreatePriceLogDatastore()
        {
            var options = Options.Create(new AppSettings { Sources = _testSources });
            var sourcesProvider = new ConfigSourcesProvider(options);
            return new PriceLogDatastore(sourcesProvider);
        }
    }
}
