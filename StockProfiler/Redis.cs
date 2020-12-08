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

        /// <summary>
        /// Subscribe Event
        /// </summary>
        public void Subscribe()
        {
            ISubscriber subscriber = Connection.GetSubscriber();
            subscriber.SubscribeAsync("messages", (channel, message) => {
                Console.WriteLine(message);
            });
        }

        /// <summary>
        /// Save most current event to Redis cache for Delta processing.
        /// </summary>
        /// <param name="quotes"></param>
        /// <returns></returns>
        public bool Save(List<Quote> quotes)
        {
            bool isSuccess = false;

            foreach (var item in quotes)
            {
                // TODO: Temporarily use UTC as a unique key value 
                // TODO: Serialize the object and store it as a JSON formatted string
                // Thoughts, need to find a better way than storing as JSON string that is similar to the Quote object.
                isSuccess = RedisCache.StringSet(DateTime.UtcNow.ToString(), item.ToString());
                Console.WriteLine(item.ToString());
            }
            return isSuccess;
        }

        /// <summary>
        /// Get back previous events.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string Get(string host, string key)
        {
            return RedisCache.StringGet(key);
        }
    }
}
