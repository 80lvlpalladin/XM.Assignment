using Microsoft.Extensions.Options;
using XM.Assignment.Domain.Models;
using XM.Assignment.Domain.Models.Enums;
using XM.Assignment.Infrastructure.Configuration;
using XM.Assignment.Infrastructure.Datastore;

namespace XM.Assignment.Infrastructure.Tests
{
    public class BitcoinPriceLogDatastoreTests
    {      
        [Fact]
        public void Datastore_StoresTheExactAmountOfPricelogs_ThatWereSaved_ForExistingSource()
        {
            //Arrange
            var sources = new[]
            {
                new Source("Bitstamp", new Uri("http://someUri")),
                new Source("Bitfinex", new Uri("http://someUri"))
            };
            var options = Options.Create(new AppSettings { Sources = sources });
            var datastore = new PriceLogDatastore(options);

            //Act
            datastore.Save("Bitstamp", new BitcoinPriceLogEntry(8.2m, DateTime.Now, Currency.USD));
            datastore.Save("Bitstamp", new BitcoinPriceLogEntry(8.3m, DateTime.Now.AddMinutes(1), Currency.USD));
            datastore.Save("Bitstamp", new BitcoinPriceLogEntry(8.7m, DateTime.Now.AddMinutes(2), Currency.USD));

            //Assert
            Assert.Equal(3, datastore.GetAll("Bitstamp").Count());
        }

        [Fact]
        public void DataStore_GetAll_ReturnsEmptyArray_IfNoLogsWereSaved_ForExistingSource()
        {
            //Arrange
            var sources = new[]
            {
                new Source("Bitstamp", new Uri("http://someUri")),
                new Source("Bitfinex", new Uri("http://someUri"))
            };
            var options = Options.Create(new AppSettings { Sources = sources });
            var datastore = new PriceLogDatastore(options);

            //Act-Assert
            Assert.True(sources.All(s => datastore.GetAll(s.Name).Count() is 0));

        }

        [Fact]
        public void DataStore_GetAll_ReturnsNull_IfSourceDoesntExist()
        {
            //Arrange
            var sources = new[]
            {
                new Source("Bitstamp", new Uri("http://someUri")),
                new Source("Bitfinex", new Uri("http://someUri"))
            };
            var options = Options.Create(new AppSettings { Sources = sources });
            var datastore = new PriceLogDatastore(options);
            var nonExistingSource = "sdgwag";

            //Act-Assert
            Assert.Null(datastore.GetAll(nonExistingSource));
        }
    }
}
