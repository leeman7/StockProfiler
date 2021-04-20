using System;
using System.Collections.Generic;
using System.Threading;

namespace StockProfiler
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Init block
            var container = new Container();
            container.Init();

            // Logic for parsing commands
            if (args.Length == 0)
            {
                DisplayPromptOptions();
                var command = GetOption();
                // Determine Request
                ProcessCommandOption(command, container);                

            }

            bool shutdown = false;
            while (shutdown != true)
            {
                DisplayPromptOptions();
                var command = GetOption();
                // Determine Request
                ProcessCommandOption(command, container);
                if (command == 0)
                    shutdown = true;
                // TODO: Any other logic here
            }
        }

        /// <summary>
        /// Process the option provided by the client.
        /// </summary>
        /// <param name="command"></param>
        private static void ProcessCommandOption(int command, Container container)
        {
            try
            {
                switch (command)
                {
                    case 0: // Exit Program
                        Shutdown(container);
                        Logger.Log(LogTarget.File, $"Closing down application");
                        break;
                    case 1: // Quotes
                        container.JSONHandler.ProcessQuoteRequest(container);
                        Logger.Log(LogTarget.File, $"Quote Selected");
                        break;
                    case 2: // Charts
                        container.JSONHandler.ProcessChartsRequest(container);
                        Logger.Log(LogTarget.File, $"Charts Selected");
                        break;
                    case 3: // Watchlist
                        container.JSONHandler.ProcessWatchlistRequest(container);
                        Logger.Log(LogTarget.File, $"Watchlist Selected");
                        break;
                    case 4: // Earnings
                        container.JSONHandler.ProcessEarningsRequest(container);
                        Logger.Log(LogTarget.File, $"Earnings Selected");
                        break;
                    case 5: // Trending Stocks
                        container.JSONHandler.ProcessTrendingRequest(container);
                        Logger.Log(LogTarget.File, $"Trending Stocks Selected");
                        break;
                    case 6: // Historical Data
                        container.JSONHandler.ProcessHistoricalDataRequest(container);
                        Logger.Log(LogTarget.File, $"Historical Data Selected");
                        break;
                    case 7: // Analysis
                        container.JSONHandler.ProcessStockAnalysisRequest(container);
                        Logger.Log(LogTarget.File, $"Analysis Selected");
                        break;
                    case 8: // Stock Summary
                        container.JSONHandler.ProcessStockSummaryRequest(container);
                        Logger.Log(LogTarget.File, $"Stock Summary Selected");
                        break;
                    case 9: // Stock Profile
                        container.JSONHandler.ProcessStockProfileRequest(container);
                        Logger.Log(LogTarget.File, $"Stock Profile Selected");
                        break;
                    default:
                        container.JSONHandler.ProcessQuoteRequest(container);
                        Logger.Log(LogTarget.File, $"Quote Selected");
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LogTarget.Exception, $"{ex}");
            }
        }

        private static void Shutdown(Container container)
        {
            container.Dispose();
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
            Console.WriteLine("\r\n========OPTIONS========");
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
    }
}
