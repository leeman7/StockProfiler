using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockProfiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var rapidClient = new Rapid();
            JsonHandler jsonHandler = new JsonHandler();
            var response = rapidClient.RequestQuote();
            List<Quote> quotes = jsonHandler.ProcessQuoteResponse(response);

            // Display Watchlist
            Console.WriteLine("Watchlist Profile");
            foreach (var item in quotes)
            {
                Console.WriteLine($"{item.Symbol}\r\n   Name: {item.ShortName}\r\n   Open: {item.RegularMarketOpen}");
            }
            // Keep Program going
            Console.ReadLine();
        }

        public void Init()
        {
            Rapid rapidClient = new Rapid();            
            Redis redisClient = new Redis();
            Mongo mongoClient = new Mongo();
            mongoClient.Init();
            JsonHandler jsonHandler = new JsonHandler();
        }

        public void JunkTesting()
        {
            var redis = Redis.RedisCache;

            if (redis.StringSet("testKey", "testValue"))
            {
                var val = redis.StringGet("testKey");
                Console.WriteLine(val);
            }

            Mongo mongo = new Mongo();
            mongo.Init();
        }
    }
}
