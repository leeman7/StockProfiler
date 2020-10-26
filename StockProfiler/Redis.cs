using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace StockProfiler
{
    public class Redis
    {
        private const string HOST = "localhost";
        private static Lazy<ConnectionMultiplexer> redisClient;
        public static ConnectionMultiplexer Connection => redisClient.Value;
        public static IDatabase RedisCache => Connection.GetDatabase();

        static Redis()
        {

            ConfigurationOptions Options = new ConfigurationOptions
            {
                EndPoints = { HOST }
            };

            redisClient = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(Options));
        }

        public void Subscribe()
        {
            ISubscriber subscriber = Connection.GetSubscriber();
            subscriber.SubscribeAsync("messages", (channel, message) => {
                Console.WriteLine(message);
            });
        }

        public bool Save(List<Quote> quotes)
        {
            bool isSuccess = false;

            foreach (var item in quotes)
            {
                // TODO: Temporarily use UTC as a unique key value 
                // TODO: Serialize the object and store it as a JSON formatted string
                // Thoughts, need to find a better way than storing as JSON string that is similar to the Quote object.
                isSuccess = RedisCache.StringSet(DateTime.UtcNow.ToString(), item.ToString());
            }
            return isSuccess;
        }
        private string Get(string host, string key)
        {
            return RedisCache.StringGet(key);
        }
    }
}
