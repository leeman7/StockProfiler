using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.WireProtocol.Messages;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

namespace StockProfiler
{
    public class Mongo
    {
        private MongoClient mongoClient { get; set; }
        public MongoClient MongoClient => mongoClient;
        IMongoDatabase MongoDatabase { get; set; }
        private const string HOST = "mongodb://localhost:27017";
        public bool IsConnected { get; set; }

        public Mongo()
        {
            mongoClient = new MongoClient(HOST);
        }        

        public void Init()
        {
            try
            {
                
                //mongoClient.Settings.ConnectionMode = ConnectionMode.Automatic;
                //mongoClient.Settings.ConnectTimeout = new TimeSpan(0, 0, 0, 30);
                
                MongoDatabase = mongoClient.GetDatabase("test");
                IsConnected = PingDatabase();

                var dbList = mongoClient.ListDatabases().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }            
        }

        public bool PingDatabase()
        {
            bool successfulConnection;
            try
            {
                successfulConnection = MongoDatabase.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                successfulConnection = MongoDatabase.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
                // Reconnect logic
                // Reconnect();
            }
            return successfulConnection;
        }

        public void InsertMany()
        {
            var entity = new MongoQuote { Name = "MSFT" };
            List<MongoQuote> mongoQuotes = new List<MongoQuote>();
            var collection = MongoDatabase.GetCollection<MongoQuote>("watchlist");
            collection.InsertMany(mongoQuotes);
        }

        public async void Find()
        {
            var collection = MongoDatabase.GetCollection<MongoQuote>("watchlist");
            var documents = await collection.Find(Builders<MongoQuote>.Filter.Empty).ToListAsync();
            //var quoteFilter = Builders<BsonDocument>.Filter.ElemMatch<BsonValue>("")
            //List<MongoQuote> query = collection.AsQueryable<MongoQuote>().Where<Entity>()
        }

        public async void Update(string field, Quote quote)
        {
            var collection = MongoDatabase.GetCollection<BsonDocument>("watchlist");
            var retult = await collection.FindOneAndUpdateAsync(
                Builders<BsonDocument>.Filter.Eq("Symbol", "MSFT"),
                Builders<BsonDocument>.Update.Set("RegularMarketPrice", 209.05));
            await collection.Find(new BsonDocument()).ForEachAsync(x => Console.WriteLine(x));
        }
    }

    #region Mongo Objects
    public class MongoQuote
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
    #endregion
}
