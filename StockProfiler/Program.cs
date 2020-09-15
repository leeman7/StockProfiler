using System;
using System.Collections.Generic;

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
    }
}
