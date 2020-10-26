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
                Console.ReadLine();
            }

            // TODO: Run timers in background to make requests.
            // Create a timer with a ten second interval.
            System.Timers.Timer aTimer;
            aTimer = new System.Timers.Timer(10000);

            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            // Set the Interval to 2 seconds (2000 milliseconds).
            aTimer.Interval = 2000;
            aTimer.Enabled = true;

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
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs elapsed)
        {

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

        public static void JsonHandler()
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
