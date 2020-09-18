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
        IMongoDatabase MongoDatabase { get; set; }
        private const string HOST = "mongodb://localhost:27017";

        public Mongo()
        {
            mongoClient = new MongoClient();
        }

        public MongoClient MongoClient => mongoClient;


        public void Init()
        {
            mongoClient = new MongoClient(HOST);
            MongoDatabase = mongoClient.GetDatabase("");
            var dbList = mongoClient.ListDatabases().ToList();

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
