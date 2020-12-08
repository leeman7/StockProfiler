using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using MongoDB.Driver;

namespace StockProfiler
{
    class Program
    {
        public static Mongo MongoClient { get; set; }
        public static Redis RedisClient { get; set; }
        
        static void Main(string[] args)
        {
            // Init block
            Init();

            // TODO: Logic for parsing commands
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter an argument.");
                Console.WriteLine("1 - Quotes");
                Console.WriteLine("2 - Charts");
                Console.WriteLine("3 - Watchlist");
                Console.WriteLine("4 - Earnings");
                Console.WriteLine("5 - Trending Stocks");
                Console.WriteLine("6 - Historical Data");
                Console.WriteLine("7 - Analysis");
                Console.WriteLine("8 - Stock Summary");
                Console.WriteLine("9 - Stock Profile");
                var test = Console.ReadLine();

                if (!int.TryParse(test, out int command))
                {
                    Console.WriteLine("Please enter a valid command: ");
                    test = Console.ReadLine();
                }

                switch(command)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    default:
                        break;
                       
                }
            }

            JsonHandler();

            // Keep Program going
            Console.ReadLine();
        }

        public static void Init()
        {
            // Redis client only needs instance for actions
            RedisClient = new Redis();            

            // MongoDB client for storing data.
            MongoClient = new Mongo();
            MongoClient.Init();

            // Open files for logging and output
            //FileHandler();

            var Poller = new Poller();
            Poller.AlternatePoller();
        }

        private static void FileHandler()
        {
            try
            {
                string path = @"C:\Users\Leema\source\repos\StockProfiler\StockProfiler\Logs\stocks" + DateTime.Today.ToShortDateString() + ".txt";
                if (!File.Exists(path))
                {
                    File.Create(path);
                }
                else
                {
                    var wh = new AutoResetEvent(false);
                    var fsw = new FileSystemWatcher(".");
                    fsw.Filter = "file-to-read";
                    fsw.EnableRaisingEvents = true;
                    fsw.Changed += (s, e) => wh.Set();

                    var fs = new FileStream("file-to-read", FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                    using (var sr = new StreamWriter(fs))
                    {
                        var s = "";
                        while (true)
                        {                            
                            if (s != null)
                                sr.WriteLine(s);
                            else
                                wh.WaitOne(1000);
                        }
                    }

                    wh.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
            }
        }

        public static List<Quote> JsonHandler()
        {
            var rapid = new Rapid();
            JsonHandler jsonHandler = new JsonHandler();
            var response = rapid.RequestQuote();
            List<Quote> quotes = jsonHandler.ProcessQuoteResponse(response);

            // Display Watchlist
            Console.WriteLine("Watchlist Profile");
            foreach (var item in quotes)
            {
                Console.WriteLine($"{item.Symbol}\r\n   Name: {item.ShortName}\r\n   Open: {item.RegularMarketOpen}");
            }

            return quotes;
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
