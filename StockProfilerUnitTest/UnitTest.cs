using System;
using Xunit;
using Moq;
using StockProfiler;

namespace StockProfilerUnitTest
{
    public class UnitTest : IDisposable
    {
        public Container Container { get; set; }

#pragma warning disable xUnit1013 // Public method should be marked as test
        public void Setup()
#pragma warning restore xUnit1013 // Public method should be marked as test
        {
            Container = new Container();
            Container.Init();
        }

        public void Dispose()
        {
        }

        [Fact]
        public void StartUpTest()
        {
            var container = new Container();
            container.Init();

            Assert.True(container.GenericPoll != null);
            Assert.True(container.JSONHandler != null);
            Assert.True(container.MongoClient != null);
            Assert.True(container.RapidInstance != null);
            Assert.True(container.RedisClient != null);
        }

        [Fact]
        public void MongoDBTest()
        {
            var mock = new Mock<Mongo>();
            mock.Object.Init();
            var mongoDb = mock.Object.MongoClient != null;            
            bool valid = mock.Object.PingDatabase();
            Assert.True(mongoDb);
            Assert.True(valid);
        }

        [Fact]
        public void RedisTest()
        {
            var mock = new Mock<Redis>();
            var validConnection = mock.Object.TestConnection();
            var validReconnect = mock.Object.Reconnect();
            Assert.True(validConnection);
            Assert.True(validReconnect);
        }

        [Fact]
        public void StockRequestTest()
        {
            Setup();
            var quotes = Container.JSONHandler.ProcessQuoteRequest(Container);
            Assert.NotNull(quotes);
        }

        [Fact]
        public void WatchlistRequestTest()
        {
            Setup();
            var quotes = Container.JSONHandler.ProcessWatchlistRequest(Container);
            Assert.NotNull(quotes);
        }
    }
}
