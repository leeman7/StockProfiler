using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace StockProfiler
{
    public class Mongo
    {
        private const string HOST = "mongodb://localhost:27017";
        private const string DATABASE = "StockProfiler";
        private MongoClient mongoClient { get; set; }
        public MongoClient MongoClient { get; private set; }
        IMongoDatabase MongoDatabase { get; set; }        
        public bool IsConnected { get; set; }

        public delegate void CheckConnection(bool b);

        public Mongo()
        {
            MongoClient = mongoClient = new MongoClient(HOST);
        }        

        /// <summary>
        /// MongoDB Init that pings the Database
        /// </summary>
        public void Init()
        {
            try
            {
                //mongoClient.Settings.ConnectionMode = ConnectionMode.Automatic;
                //mongoClient.Settings.ConnectTimeout = new TimeSpan(0, 0, 0, 30);
#if false
                MongoDatabase = mongoClient.GetDatabase("DATABASE");
#endif
                MongoDatabase = mongoClient.GetDatabase("test");
                IsConnected = PingDatabase();

                var dbList = mongoClient.ListDatabases().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }            
        }

        /// <summary>
        /// Ping the Mongo Database to make sure its still up and we are connected.
        /// </summary>
        /// <returns>true if successful</returns>
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

        /// <summary>
        /// Insert multiple entries into the MongoDB. Mostly for the Watchlist.
        /// </summary>
        public void InsertMany()
        {
            try
            {
                var entity = new MongoQuote { Name = "MSFT" };
                List<MongoQuote> mongoQuotes = new List<MongoQuote>();
                var collection = MongoDatabase.GetCollection<MongoQuote>("watchlist");
                collection.InsertMany(mongoQuotes);
            }
            catch (Exception ex)
            {
                Logger.Log(LogTarget.Exception, $"{ex}");
                throw;
            }
        }

        /// <summary>
        /// Finds an entry in the MongoDB. Not sure if I need this if I intend to convert them to CSV files.
        /// </summary>
        public async void Find()
        {
            var collection = MongoDatabase.GetCollection<MongoQuote>("watchlist");
            var documents = await collection.Find(Builders<MongoQuote>.Filter.Empty).ToListAsync();
            //var quoteFilter = Builders<BsonDocument>.Filter.ElemMatch<BsonValue>("")
            //List<MongoQuote> query = collection.AsQueryable<MongoQuote>().Where<Entity>()
        }

        /// <summary>
        /// Used to update an entry within the MongoDB. 
        /// Not sure if needed since for most instances we take in what we get.
        /// </summary>
        /// <param name="field">Field to be updated</param>
        /// <param name="quote">Quote object for reference</param>
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
