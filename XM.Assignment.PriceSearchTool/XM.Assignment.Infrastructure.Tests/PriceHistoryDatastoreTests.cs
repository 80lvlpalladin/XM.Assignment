using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using XM.Assignment.Domain.Models;
using XM.Assignment.Domain.Utiilities;
using XM.Assignment.Infrastructure.Configuration;
using XM.Assignment.Infrastructure.Datastore;
using XM.Assignment.Infrastructure.Providers;
using Xunit;

namespace XM.Assignment.Infrastructure.Tests;

public class PriceHistoryDatastoreTests
{
    [Fact]
    public void Datastore_Save_DoesntSaveDuplicateEntries()
    {
        //Arrange
        var datastore = CreatePriceLogDatastore();
        var currency = "someCurrency";
        var source = "someSource";
        var expectedResult = 1;

        //Act
        foreach (var entry in Enumerable.Repeat(new PriceLogEntry { Price = 42.42m, TimeStamp = 42 }, 5))
        {
            datastore.Save(source, currency, entry);
        }

        //Assert
        var actualResult = datastore.GetForSourceAndCurrency(source, currency).Count();
        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void Datastore_HandlesParallelSaves_Gracefully()
    {
        //Arrange
        var datastore = CreatePriceLogDatastore();
        var expectedResult = new List<PriceLogEntry>();
        var currency = "someCurrency";
        var source = "someSource";
        
        //Act
        Parallel.ForEach(Enumerable.Range(1, 10), i =>
        {
            var pricelogentry = new PriceLogEntry { Price = 8.1m + i, TimeStamp = (uint) i };
            datastore.Save(source, currency, pricelogentry);
            expectedResult.Add(pricelogentry);
        });

        //Assert
        var actualResult = datastore.GetForSourceAndCurrency(source, currency);
        Assert.True(expectedResult.OrderBy(p => p.TimeStamp)
            .SequenceEqual(actualResult.OrderBy(p => p.TimeStamp), new PriceLogEntryEqualityComparer()));
    }

    [Fact]
    public void Datastore_StoresTheExactAmountOfPricelogs_ThatWereSaved()
    {
        //Arrange
        var datastore = CreatePriceLogDatastore();
        var currency = "someCurrency";
        var source = "someSource";

        //Act
        datastore.Save(source, currency, new PriceLogEntry { Price = 8.1m, TimeStamp = 100 });
        datastore.Save(source, currency, new PriceLogEntry { Price = 8.3m, TimeStamp = 200 });
        datastore.Save(source, currency, new PriceLogEntry { Price = 8.4m, TimeStamp = 230 });

        //Assert
        Assert.Equal(3, datastore.GetForSourceAndCurrency(source, currency).Count());
    }

    [Fact]
    public void DataStore_GetAll_ReturnsNull_IfSourceDoesntExistInTheDatastore()
    {
        //Arrange
        var datastore = CreatePriceLogDatastore();
        var nonExistingSource = "sdgwag";

        //Act-Assert
        Assert.Null(datastore.GetForSourceAndCurrency(nonExistingSource, "btcusd"));
    }

    private PriceHistoryDatastore CreatePriceLogDatastore()
    {
        var appsettingsOptions = Options.Create(new AppSettings());
        var memoryCacheOptions = Options.Create(new MemoryCacheOptions());
        var equalityComparer = new PriceLogEntryEqualityComparer();
        return new PriceHistoryDatastore(new MemoryCache(memoryCacheOptions), equalityComparer, new AppSettingsProvider(appsettingsOptions));
    }
}
