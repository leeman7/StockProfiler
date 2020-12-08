using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StockProfiler
{
    public class JsonHandler
    {
        /// <summary>
        /// Parse Quote data from RAPID API.
        /// </summary>
        public List<Quote> ParseQuote(Root quoteEntries)
        {
            List<Quote> quotes = new List<Quote>();
            foreach (var item in quoteEntries.quoteResponse.result)
            {
                Quote quote = MapQuote(item);
                quotes.Add(quote);
            }
            return quotes;
        }
          
        /// <summary>
        /// Map JSON object to Quote object fields.
        /// </summary>
        /// <param name="item">JSON Quote object</param>
        /// <returns>Quote Object</returns>
        private Quote MapQuote(dynamic item)
        {
            Quote quote = new Quote();

            quote.Symbol = item.symbol;
            quote.ShortName = item.shortName;
            quote.Ask = item.ask;
            quote.Bid = item.bid;
            quote.DividendsPerShare = item.dividendsPerShare;
            quote.EarningsTimestamp = item.earningsTimestamp;
            quote.FiftyDayAverage = item.fiftyDayAverage;
            quote.PreMarketChange = item.preMarketChange ?? 0.0;
            quote.PreMarketPrice = item.preMarketPrice ?? 0.0;
            quote.RegularMarketPreviousClose = item.regularMarketPreviousClose;
            quote.RegularMarketChange = item.regularMarketChange;
            quote.RegularMarketChangePercent = item.regularMarketChangePercent;
            quote.RegularMarketOpen = item.regularMarketOpen;
            quote.RegularMarketPrice = item.regularMarketPrice;
            quote.RegularMarketTime = item.regularMarketTime;
            quote.TwoHundredDayAverage = item.twoHundredDayAverage;
            quote.QuoteType = item.quoteType;

            return quote;
        }

        /// <summary>
        /// Process JSON Quote response and return a list of Quote data.
        /// </summary>
        /// <param name="response">JSON Quote data</param>
        /// <returns>List of Quotes</returns>
        public List<Quote> ProcessQuoteResponse(string response)
        {
            List<Quote> quotes = new List<Quote>();
            try
            {
                Root entries = JsonConvert.DeserializeObject<Root>(response);
                quotes = ParseQuote(entries);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in ProcessQuoteResponse: {ex}");
            }

            return quotes;
        }
    }

    #region JSON Objects
    public class Root
    {
        public QuoteResponse quoteResponse { get; set; }
    }

    public class QuoteResponse
    {
        public List<dynamic> result { get; set; }
    }
    #endregion

    #region JSON Objects
    public class Quote
    {
        public Quote()
        {
            // default constructor 
        }

        public Quote(string symbol, double price, int time)
        {
            Symbol = symbol;
            RegularMarketPrice = price;
            RegularMarketTime = time;
        }

        public string Symbol { get; set; }
        public string QuoteType { get; set; }
        public string ShortName { get; set; }
        public double RegularMarketPrice { get; set; }
        public double RegularMarketPreviousClose { get; set; }
        public int RegularMarketTime { get; set; }
        public float RegularMarketChange { get; set; }
        public float RegularMarketChangePercent { get; set; }
        public double RegularMarketOpen { get; set; }
        public double PreMarketPrice { get; set; }
        public double PreMarketChange { get; set; }
        public double DividendsPerShare { get; set; }
        public double Ask { get; set; }
        public double Bid { get; set; }
        public int EarningsTimestamp { get; set; }
        public double FiftyDayAverage { get; set; }
        public double TwoHundredDayAverage { get; set; }
    }
    #endregion
}
