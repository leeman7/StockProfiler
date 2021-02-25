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
        /// Parse the rest of the Watchlist JSON object from the request.
        /// </summary>
        /// <param name="watchlist">Watchlist Object</param>
        /// <returns>Populated Watchlist Object</returns>
        public Watchlist ParseWatchlist(Watchlist watchlist)
        {
            watchlist.Finance.Result[0].Symbols = new List<WatchlistSymbol>();
            foreach (var item in watchlist.Finance.Result[0].SymbolsList)
            {
                WatchlistSymbol watchlistSymbol = MapWatchlist(item.Value, item.Name);
                watchlist.Finance.Result[0].Symbols.Add(watchlistSymbol);
            }
            return watchlist;
        }

        /// <summary>
        /// Map Watchlist symbol data.
        /// </summary>
        /// <param name="item">Symbol object</param>
        /// <param name="symbol">Symbol Ticker</param>
        /// <returns>Watchlist Symbol object</returns>
        private WatchlistSymbol MapWatchlist(dynamic item, string symbol)
        {
            WatchlistSymbol watchlistSymbol = new WatchlistSymbol();
            
            watchlistSymbol.Symbol = symbol;
            watchlistSymbol.OneDayPercentChange = item.oneDayPercentChange;
            watchlistSymbol.OneMonthPercentChange = item.oneMonthPercentChange;
            watchlistSymbol.OneYearPercentChange = item.oneYearPercentChange;
            
            if (item.lifetimePercentChange == "Infinity")
                watchlistSymbol.LifetimePercentChange = 0.0;
            else
                watchlistSymbol.LifetimePercentChange = item.lifetimePercentChange ?? 0.0;

            watchlistSymbol.OriginTimestamp = item.originTimestamp;
            watchlistSymbol.UpdatedAt = item.updatedAt;

            return watchlistSymbol;
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

        /// <summary>
        /// Process JSON Quote response and return a list of Quote data.
        /// </summary>
        /// <param name="response">JSON Quote data</param>
        /// <returns>List of Quotes</returns>
        public Watchlist ProcessWatchlistResponse(string response)
        {
            Watchlist watchlist = new Watchlist();
            try
            {
                var entries = JsonConvert.DeserializeObject<Watchlist>(response);
                watchlist = ParseWatchlist(entries);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in ProcessWatchlistResponse: {ex}");
            }

            return watchlist;
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

    public class Watchlist
    {
        [JsonProperty("finance")]
        public Finance Finance { get; set; }
    }

    public partial class Finance
    {
        [JsonProperty("result")]
        public List<Result> Result { get; set; }

        [JsonProperty("error")]
        public dynamic Error { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("portfolio")]
        public WatchlistSymbol Portfolio { get; set; }

        [JsonProperty("symbols")]
        public dynamic SymbolsList { get; set; }

        public List<WatchlistSymbol> Symbols { get; set; }
    }

    public partial class WatchlistSymbol
    {
        public string Symbol { get; set; }

        [JsonProperty("oneDayPercentChange")]
        public double OneDayPercentChange { get; set; }

        [JsonProperty("oneMonthPercentChange")]
        public double OneMonthPercentChange { get; set; }

        [JsonProperty("oneYearPercentChange")]
        public double OneYearPercentChange { get; set; }

        [JsonProperty("lifetimePercentChange")]
        public double LifetimePercentChange { get; set; }

        [JsonProperty("originTimestamp")]
        public long OriginTimestamp { get; set; }

        [JsonProperty("updatedAt")]
        public long UpdatedAt { get; set; }
    }

    /*
    {
        "finance": {
            "result": [
                {
                "portfolio": {
                    "oneDayPercentChange": -0.8460327235542864,
                    "oneMonthPercentChange": -3.97857,
                    "oneYearPercentChange": 8.13591,
                    "lifetimePercentChange": 24.0281,
                    "originTimestamp": 0,
                    "updatedAt": 1611719505
                },
                "symbols": {
                    "GSPC": {
                        "oneDayPercentChange": -0.149,
                        "oneMonthPercentChange": 3.0589999999999997,
                        "oneYearPercentChange": 18.682000000000002,
                        "lifetimePercentChange": 4039.376,
                        "originTimestamp": 0,
                        "updatedAt": 0
                        }
                    }
                }
            ],
        "error": null
        }
      }
      */
    #endregion
}
