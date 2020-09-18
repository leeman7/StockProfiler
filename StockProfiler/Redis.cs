using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace StockProfiler
{
    public class Redis
    {
        private const string HOST = "localhost";

        public Redis()
        {
            redisClient = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(HOST);
            });
        }

        private static Lazy<ConnectionMultiplexer> redisClient;

        public static ConnectionMultiplexer  Connection
        {
            get
            {
                return redisClient.Value;
            }
        }

        public void Save(List<Quote> quotes)
        {
            var cache = Connection.GetDatabase();

            foreach (var item in quotes)
            {
                // TODO: Temporarily use UTC as a unique key value 
                // TODO: Serialize the object and store it as a JSON formatted string
                // Thoughts, need to find a better way than storing as JSON string that is similar to the Quote object.
                cache.StringSet(DateTime.UtcNow.ToString(), item.ToString());
            }
        }
    }
}
