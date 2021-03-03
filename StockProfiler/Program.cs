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
        public static Rapid RapidInstance { get; set; }
        public static JsonHandler JSONHandler { get; set; }
        public static Poller GenericPoll { get; set; }

        static void Main(string[] args)
        {
            // Init block
            Init();

            // TODO: Logic for parsing commands
            if (args.Length == 0)
            {
                DisplayPromptOptions();
                var command = GetOption();
                ProcessCommandOption(command);
                //TODO: Determine Request logic here

            }

            // Keep Program going
            Console.ReadLine();
        }

        /// <summary>
        /// Process the option provided by the client.
        /// </summary>
        /// <param name="command"></param>
        private static void ProcessCommandOption(int command)
        {
            try
            {
                switch (command)
                {
                    case 0: // Exit Program
                        Shutdown();
                        Logger.Log(LogTarget.File, $"Closing down application");
                        break;
                    case 1: // Quotes
                        ProcessQuoteRequest();
                        Logger.Log(LogTarget.File, $"Quote Selected");
                        break;
                    case 2: // Charts
                        ProcessChartsRequest();
                        Logger.Log(LogTarget.File, $"Charts Selected");
                        break;
                    case 3: // Watchlist
                        ProcessWatchlistRequest();
                        Logger.Log(LogTarget.File, $"Watchlist Selected");
                        break;
                    case 4: // Earnings
                        ProcessEarningsRequest();
                        Logger.Log(LogTarget.File, $"Earnings Selected");
                        break;
                    case 5: // Trending Stocks
                        ProcessTrendingRequest();
                        Logger.Log(LogTarget.File, $"Trending Stocks Selected");
                        break;
                    case 6: // Historical Data
                        ProcessHistoricalDataRequest();
                        Logger.Log(LogTarget.File, $"Historical Data Selected");
                        break;
                    case 7: // Analysis
                        ProcessStockAnalysisRequest();
                        Logger.Log(LogTarget.File, $"Analysis Selected");
                        break;
                    case 8: // Stock Summary
                        ProcessStockSummaryRequest();
                        Logger.Log(LogTarget.File, $"Stock Summary Selected");
                        break;
                    case 9: // Stock Profile
                        ProcessStockProfileRequest();
                        Logger.Log(LogTarget.File, $"Stock Profile Selected");
                        break;
                    default:
                        ProcessQuoteRequest();
                        Logger.Log(LogTarget.File, $"Quote Selected");
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LogTarget.Exception, $"{ex}");
            }
        }

        private static void Shutdown()
        {
            return 1;
        }

        /// <summary>
        /// Gets the options provided by the client.
        /// </summary>
        /// <returns></returns>
        private static int GetOption()
        {
            var option = Console.ReadLine();
            int command;

            while (!int.TryParse(option, out command))
            {
                Console.WriteLine("Please enter a valid command: ");
                option = Console.ReadLine();
            }

            return command;
        }

        /// <summary>
        /// Options display. To be displayed to the client upon launching the applications.
        /// </summary>
        private static void DisplayPromptOptions()
        {
            Console.WriteLine("========OPTIONS========");
            Console.WriteLine("0 - Exit Program");
            Console.WriteLine("1 - Quotes");
            Console.WriteLine("2 - Charts");
            Console.WriteLine("3 - Watchlist");
            Console.WriteLine("4 - Earnings");
            Console.WriteLine("5 - Trending Stocks");
            Console.WriteLine("6 - Historical Data");
            Console.WriteLine("7 - Analysis");
            Console.WriteLine("8 - Stock Summary");
            Console.WriteLine("9 - Stock Profile");
            Console.WriteLine("\r\n");
        }

        public static void Init()
        {
            // Open files for logging and output
            Logger.LogFileHelper();

            // Create and start Main application event Poller.
            GenericPoll = new Poller(Poller.EventHandlerType.Generic, 60000, 30000);

            // Redis client only needs instance for actions
            RedisClient = new Redis();            

            // MongoDB client for storing data.
            MongoClient = new Mongo();
            MongoClient.Init();

            RapidInstance = new Rapid();
            JSONHandler = new JsonHandler();
        }

        /// <summary>
        /// Handles JSON strings and logs output to command window.
        /// </summary>
        /// <returns>List of Quotes Objects</returns>
        public static List<Quote> ProcessQuoteRequest()
        {
            var response = RapidInstance.RequestQuote();
            List<Quote> quotes = JSONHandler.ProcessQuoteResponse(response);
            
            Console.WriteLine("Stock Statistics");
            foreach (var item in quotes)
            {
                Console.WriteLine($"{item.Symbol}\r\n   Name: {item.ShortName}\r\n   Open: {item.RegularMarketOpen}");
                Logger.Log(LogTarget.File, $"{item.Symbol}\r\n   Name: {item.ShortName}\r\n   Open: {item.RegularMarketOpen}");
            }
            return quotes;
        }

        /// <summary>
        /// Handles JSON strings and logs output to command window.
        /// </summary>
        /// <returns>List of Quotes Objects</returns>
        public static dynamic ProcessWatchlistRequest()
        {
            var response = RapidInstance.RequestWatchlist();
            var watchlist = JSONHandler.ProcessWatchlistResponse(response);

            Console.WriteLine("Watchlist Profile");
            Console.WriteLine($"One Day: {watchlist.Finance.Result[0].Portfolio.OneDayPercentChange}\r\n   " +
                              $"One Month: {watchlist.Finance.Result[0].Portfolio.OneMonthPercentChange}\r\n   " +
                              $"One Year: {watchlist.Finance.Result[0].Portfolio.OneYearPercentChange}\r\n  " +
                              $"Lifetime Percent: {watchlist.Finance.Result[0].Portfolio.LifetimePercentChange}\r\n  " +
                              $"Updated: {watchlist.Finance.Result[0].Portfolio.UpdatedAt}\r\n  " +
                              $"Time: {watchlist.Finance.Result[0].Portfolio.OriginTimestamp}\r\n");
            foreach (var item in watchlist.Finance.Result[0].Symbols)
            {
                Console.WriteLine($"Symbol: {item.Symbol}\r\n  " +
                                  $"One Day: {item.OneDayPercentChange}\r\n   " +
                                  $"One Month: {item.OneMonthPercentChange}\r\n   " +
                                  $"One Year: {item.OneYearPercentChange}\r\n  " +
                                  $"Lifetime Percent: {item.LifetimePercentChange}\r\n  " +
                                  $"Updated: {item.UpdatedAt}\r\n  " +
                                  $"Time: {item.OriginTimestamp}\r\n");
            }

            return watchlist;
        }

        /// <summary>
        /// Handles JSON strings and logs output to command window for Charts request.
        /// </summary>
        /// <returns></returns>
        public static dynamic ProcessChartsRequest()
        {
            var response = RapidInstance.RequestCharts();
            var watchlist = JSONHandler.ProcessChartsResponse(response);

            Console.WriteLine("Watchlist Profile");
            Console.WriteLine($"");

            return watchlist;
        }

        /// <summary>
        /// Handles JSON strings and logs output to command window for Earnings request.
        /// </summary>
        /// <returns></returns>
        public static dynamic ProcessEarningsRequest()
        {
            var response = RapidInstance.RequestEarnings();
            var watchlist = JSONHandler.ProcessEarningsResponse(response);

            Console.WriteLine("Watchlist Profile");
            Console.WriteLine($"");

            return watchlist;
        }

        /// <summary>
        /// Handles JSON strings and logs output to command window for Trending Request.
        /// </summary>
        /// <returns></returns>
        public static dynamic ProcessTrendingRequest()
        {
            var response = RapidInstance.RequestTrendingStocks();
            var watchlist = JSONHandler.ProcessTrendingResponse(response);

            Console.WriteLine("Watchlist Profile");
            Console.WriteLine($"");

            return watchlist;
        }

        /// <summary>
        /// Handles JSON strings and logs output to command window for Stock Profile.
        /// </summary>
        private static dynamic ProcessStockProfileRequest()
        {
            var response = RapidInstance.RequestStockProfile();
            var watchlist = JSONHandler.ProcessStockProfileResponse(response);

            Console.WriteLine("Watchlist Profile");
            Console.WriteLine($"");

            return watchlist;
        }

        /// <summary>
        /// Handles JSON strings and logs output to command window for Stock Summary.
        /// </summary>
        /// <returns></returns>
        private static dynamic ProcessStockSummaryRequest()
        {
            var response = RapidInstance.RequestStockSummary();
            var watchlist = JSONHandler.ProcessStockSummaryResponse(response);

            Console.WriteLine("Watchlist Profile");
            Console.WriteLine($"");

            return watchlist;
        }

        /// <summary>
        /// Handles JSON strings and logs output to command window for Stock Analysis.
        /// </summary>
        /// <returns></returns>
        private static dynamic ProcessStockAnalysisRequest()
        {
            var response = RapidInstance.RequestStockAnalysis();
            var watchlist = JSONHandler.ProcessStockAnalysisResponse(response);

            Console.WriteLine("Watchlist Profile");
            Console.WriteLine($"");

            return watchlist;
        }

        /// <summary>
        /// Handles JSON strings and logs output to command window for Historical Data.
        /// </summary>
        /// <returns></returns>
        private static dynamic ProcessHistoricalDataRequest()
        {
            var response = RapidInstance.RequestHistoricalData();
            var watchlist = JSONHandler.ProcessHistoricalDataResponse(response);

            Console.WriteLine("Watchlist Profile");
            Console.WriteLine($"");

            return watchlist;
        }
    }
}
