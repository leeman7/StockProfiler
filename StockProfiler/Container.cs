using System;
using System.Collections.Generic;
using System.Text;

namespace StockProfiler
{
    public class Container : IDisposable
    {
        public Mongo MongoClient { get; set; }
        public Redis RedisClient { get; set; }
        public Rapid RapidInstance { get; set; }
        public JsonHandler JSONHandler { get; set; }
        public Poller GenericPoll { get; set; }

        public Container()
        {
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Init()
        {
            // Open files for logging and output
            Logger.LogFileHelper();

            // Create and start Main application event Poller.
            GenericPoll = new Poller(Poller.EventHandlerType.Generic, 60000, 30000, this);

            // Redis client only needs instance for actions
            RedisClient = new Redis();

            // MongoDB client for storing data.
            MongoClient = new Mongo();
            MongoClient.Init();

            RapidInstance = new Rapid();
            JSONHandler = new JsonHandler();
        }
    }
}
