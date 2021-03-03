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

        /// <summary>
        /// Earnings Response handling.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public Earnings ProcessEarningsResponse(string response)
        {
            Earnings earnings = new Earnings();
            try
            {
                var entries = JsonConvert.DeserializeObject<Earnings>(response);
                earnings = ParseEarnings(entries);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in ProcessEarningsResponse: {ex}");
            }

            return earnings;
        }

        /// <summary>
        /// Earnings response parsing.
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        private Earnings ParseEarnings(Earnings entries)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Trending response handling.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public Trending ProcessTrendingResponse(string response)
        {
            Trending trending = new Trending();
            try
            {
                var entries = JsonConvert.DeserializeObject<Trending>(response);
                trending = ParseTrending(entries);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in ProcessTrendingResponse: {ex}");
            }

            return trending;
        }

        /// <summary>
        /// Trending response parsing.
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        public Trending ParseTrending(Trending entries)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stock Profile response handling.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        internal object ProcessStockProfileResponse(string response)
        {
            Trending trending = new Trending();
            try
            {
                var entries = JsonConvert.DeserializeObject<StockProfile>(response);
                trending = ParseStockProfile(entries);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in ProcessStockProfileResponse: {ex}");
            }

            return trending;
        }

        /// <summary>
        /// Stock Profile response parsing.
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        private Trending ParseStockProfile(StockProfile entries)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stock Summary response handling.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public object ProcessStockSummaryResponse(string response)
        {
            Trending trending = new Trending();
            try
            {
                var entries = JsonConvert.DeserializeObject<StockSummary>(response);
                trending = ParseStockSummary(entries);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in ProcessStockSummaryResponse: {ex}");
            }

            return trending;
        }

        /// <summary>
        /// Stock Summar response handling.
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        private Trending ParseStockSummary(StockSummary entries)
        {
            throw new NotImplementedException();
        }

        internal object ProcessChartsResponse(string response)
        {
            Charts trending = new Charts();
            try
            {
                var entries = JsonConvert.DeserializeObject<Charts>(response);
                //trending = ParseStockSummary(entries);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in ProcessStockSummaryResponse: {ex}");
            }

            return trending;
        }

        /// <summary>
        /// Stock Analysis response handling.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        internal object ProcessStockAnalysisResponse(string response)
        {
            StockAnalysis trending = new StockAnalysis();
            try
            {
                var entries = JsonConvert.DeserializeObject<StockAnalysis>(response);
                //trending = ParseStockAnalysis(entries);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in ProcessStockAnalysisResponse: {ex}");
            }

            return trending;
        }

        /// <summary>
        /// Stock Analysis response parsing.
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        private Trending ParseStockAnalysis(StockAnalysis entries)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Historical Data response handling.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public object ProcessHistoricalDataResponse(string response)
        {
            HistoricalData trending = new HistoricalData();
            try
            {
                var entries = JsonConvert.DeserializeObject<HistoricalData>(response);
                //trending = ParseHistoricalData(entries);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in ProcessHistoricalDataResponse: {ex}");
            }

            return trending;
        }

        /// <summary>
        /// Historical Data response parsing.
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        private Trending ParseHistoricalData(HistoricalData entries)
        {
            throw new NotImplementedException();
        }
    }

    #region JSON Objects

    #region Quote JSON Object
    public class Root
    {
        public QuoteResponse quoteResponse { get; set; }
    }

    public class QuoteResponse
    {
        public List<dynamic> result { get; set; }
    }
    
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

    #region Charts JSON Object
    public partial class Charts
    {
        [JsonProperty("chart")]
        public Chart Chart { get; set; }
    }

    public partial class Chart
    {
        [JsonProperty("result")]
        public List<ChartsResult> Result { get; set; }

        [JsonProperty("error")]
        public dynamic Error { get; set; }
    }

    public partial class ChartsResult
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("timestamp")]
        public List<long> Timestamp { get; set; }

        [JsonProperty("comparisons")]
        public List<Comparison> Comparisons { get; set; }

        [JsonProperty("indicators")]
        public Indicators Indicators { get; set; }
    }

    public partial class Comparison
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("previousClose")]
        public double PreviousClose { get; set; }

        [JsonProperty("gmtoffset")]
        public long Gmtoffset { get; set; }

        [JsonProperty("high")]
        public List<double?> High { get; set; }

        [JsonProperty("low")]
        public List<double?> Low { get; set; }

        [JsonProperty("chartPreviousClose")]
        public double ChartPreviousClose { get; set; }

        [JsonProperty("close")]
        public List<double?> Close { get; set; }

        [JsonProperty("open")]
        public List<double?> Open { get; set; }
    }

    public partial class Indicators
    {
        [JsonProperty("quote")]
        public List<ChartsQuote> Quote { get; set; }
    }

    public partial class ChartsQuote
    {
        [JsonProperty("high")]
        public List<double?> High { get; set; }

        [JsonProperty("low")]
        public List<double?> Low { get; set; }

        [JsonProperty("close")]
        public List<double?> Close { get; set; }

        [JsonProperty("volume")]
        public List<long?> Volume { get; set; }

        [JsonProperty("open")]
        public List<double?> Open { get; set; }
    }

    public partial class Meta
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("exchangeName")]
        public string ExchangeName { get; set; }

        [JsonProperty("instrumentType")]
        public string InstrumentType { get; set; }

        [JsonProperty("firstTradeDate")]
        public long FirstTradeDate { get; set; }

        [JsonProperty("regularMarketTime")]
        public long RegularMarketTime { get; set; }

        [JsonProperty("gmtoffset")]
        public long Gmtoffset { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("exchangeTimezoneName")]
        public string ExchangeTimezoneName { get; set; }

        [JsonProperty("regularMarketPrice")]
        public double RegularMarketPrice { get; set; }

        [JsonProperty("chartPreviousClose")]
        public double ChartPreviousClose { get; set; }

        [JsonProperty("previousClose")]
        public double PreviousClose { get; set; }

        [JsonProperty("scale")]
        public long Scale { get; set; }

        [JsonProperty("priceHint")]
        public long PriceHint { get; set; }

        [JsonProperty("currentTradingPeriod")]
        public CurrentTradingPeriod CurrentTradingPeriod { get; set; }

        [JsonProperty("tradingPeriods")]
        public List<List<Post>> TradingPeriods { get; set; }

        [JsonProperty("dataGranularity")]
        public string DataGranularity { get; set; }

        [JsonProperty("range")]
        public string Range { get; set; }

        [JsonProperty("validRanges")]
        public List<string> ValidRanges { get; set; }
    }

    public partial class CurrentTradingPeriod
    {
        [JsonProperty("pre")]
        public Post Pre { get; set; }

        [JsonProperty("regular")]
        public Post Regular { get; set; }

        [JsonProperty("post")]
        public Post Post { get; set; }
    }

    public partial class Post
    {
        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("end")]
        public long End { get; set; }

        [JsonProperty("gmtoffset")]
        public long Gmtoffset { get; set; }
    }

    /* Sample Charts Response
     {
      "chart": {
        "result": [
          {
            "meta": {
              "currency": "RUB",
              "symbol": "HYDR.ME",
              "exchangeName": "MCX",
              "instrumentType": "EQUITY",
              "firstTradeDate": 1267597800,
              "regularMarketTime": 1614718198,
              "gmtoffset": 10800,
              "timezone": "MSK",
              "exchangeTimezoneName": "Europe/Moscow",
              "regularMarketPrice": 0.806,
              "chartPreviousClose": 0.7894,
              "previousClose": 0.7894,
              "scale": 4,
              "priceHint": 4,
              "currentTradingPeriod": {
                "pre": {
                  "timezone": "MSK",
                  "start": 1614753000,
                  "end": 1614753000,
                  "gmtoffset": 10800
                },
                "regular": {
                  "timezone": "MSK",
                  "start": 1614753000,
                  "end": 1614787200,
                  "gmtoffset": 10800
                },
                "post": {
                  "timezone": "MSK",
                  "start": 1614787200,
                  "end": 1614787200,
                  "gmtoffset": 10800
                }
              },
              "tradingPeriods": [
                [
                  {
                    "timezone": "MSK",
                    "start": 1614666600,
                    "end": 1614700800,
                    "gmtoffset": 10800
                  }
                ]
              ],
              "dataGranularity": "5m",
              "range": "1d",
              "validRanges": [
                "5y",
                "5d",
                "3mo",
                "10y",
                "max",
                "1d",
                "6mo",
                "1mo",
                "1y",
                "2y",
                "ytd"
              ]
            },
            "timestamp": [
              1614668100,
              1614668400,
              1614668700,
              1614669000,
              1614669300,
              1614669600,
              1614669900,
              1614670200,
              1614670500,
              1614670800,
              1614671100,
              1614671400,
              1614671700,
              1614672000,
              1614672300,
              1614672600,
              1614672900,
              1614673200,
              1614673500,
              1614673800,
              1614674100,
              1614674400,
              1614674700,
              1614675000,
              1614675300,
              1614675600,
              1614675900,
              1614676200,
              1614676500,
              1614676800,
              1614677100,
              1614677400,
              1614677700,
              1614678000,
              1614678300,
              1614678600,
              1614678900,
              1614679200,
              1614679500,
              1614679800,
              1614680100,
              1614680400,
              1614680700,
              1614681000,
              1614681300,
              1614681600,
              1614681900,
              1614682200,
              1614682500,
              1614682800,
              1614683100,
              1614683400,
              1614683700,
              1614684000,
              1614684300,
              1614684600,
              1614684900,
              1614685200,
              1614685500,
              1614685800,
              1614686100,
              1614686400,
              1614686700,
              1614687000,
              1614687300,
              1614687600,
              1614687900,
              1614688200,
              1614688500,
              1614688800,
              1614689100,
              1614689400,
              1614689700,
              1614690000,
              1614690300,
              1614690600,
              1614690900,
              1614691200,
              1614691500,
              1614691800,
              1614692100,
              1614692400,
              1614692700,
              1614693000,
              1614693300,
              1614693600,
              1614693900,
              1614694200,
              1614694500,
              1614694800,
              1614695100,
              1614695400,
              1614695700,
              1614696000,
              1614696300,
              1614696600,
              1614696900,
              1614697200,
              1614697500,
              1614697800,
              1614698100,
              1614698400,
              1614698700,
              1614699000,
              1614699300,
              1614699600,
              1614699900,
              1614700200,
              1614700500
            ],
            "comparisons": [
              {
                "symbol": "^FCHI",
                "previousClose": 5792.79,
                "gmtoffset": 3600,
                "high": [
                  5782.27,
                  5782.69,
                  5795.88,
                  5799.28,
                  5802.76,
                  5795.71,
                  5795.86,
                  5807.14,
                  5806.62,
                  5804.95,
                  5801.92,
                  5796.05,
                  5787.17,
                  5787.55,
                  5786.44,
                  5786.01,
                  5783.69,
                  5789.04,
                  5789.96,
                  5794.55,
                  5798.69,
                  5801.27,
                  5800.65,
                  5801.34,
                  5801.92,
                  5802.64,
                  5803.63,
                  5805.25,
                  5807.23,
                  5808.1,
                  5806.77,
                  5807.49,
                  5810.28,
                  5811.62,
                  5812.97,
                  5812.9,
                  5811.76,
                  5814.7,
                  5815.36,
                  5815.64,
                  5818.97,
                  5823.19,
                  5823.61,
                  5820.6,
                  5822.05,
                  5823.08,
                  5824.36,
                  5824.65,
                  5825.29,
                  5826.88,
                  5826.26,
                  5827.47,
                  5830.64,
                  5832.02,
                  5831.13,
                  5830.25,
                  5831.72,
                  5833.77,
                  5834.38,
                  5830.62,
                  5831.17,
                  5829.97,
                  5827.79,
                  5825.76,
                  5825.35,
                  5825.16,
                  5824.87,
                  5822.1,
                  5821.41,
                  5820.73,
                  5823.45,
                  5828.27,
                  5830.17,
                  5830.86,
                  5825.64,
                  5826.01,
                  5827.2,
                  5827.94,
                  5832.12,
                  5833.39,
                  5833.93,
                  5832.62,
                  5827.1,
                  5834.97,
                  5834.55,
                  5834.64,
                  5835.74,
                  5835.16,
                  5835.47,
                  5828.8,
                  5824.52,
                  5825.66,
                  5821.56,
                  5820.92,
                  5820.8,
                  5818.89
                ],
                "low": [
                  5773.52,
                  5776.75,
                  5783.39,
                  5794.73,
                  5796.43,
                  5787.21,
                  5786.39,
                  5795.69,
                  5803.47,
                  5801.65,
                  5796.17,
                  5782.83,
                  5782.82,
                  5784.4,
                  5779.06,
                  5781.51,
                  5780.83,
                  5783.7,
                  5788.22,
                  5789.86,
                  5793.7,
                  5799.06,
                  5796.76,
                  5797.53,
                  5797.53,
                  5797.14,
                  5798.29,
                  5802.48,
                  5803.65,
                  5805.51,
                  5803.78,
                  5805.79,
                  5807.57,
                  5807.65,
                  5808.75,
                  5811.01,
                  5810,
                  5811.6,
                  5813.48,
                  5813.94,
                  5814.15,
                  5819.18,
                  5817.84,
                  5818.27,
                  5819.93,
                  5820.16,
                  5821.6,
                  5820.61,
                  5819.7,
                  5825.24,
                  5825.19,
                  5823.04,
                  5827.03,
                  5830.1,
                  5828.21,
                  5826.13,
                  5829.92,
                  5831.54,
                  5830.69,
                  5826.82,
                  5828.37,
                  5827.5,
                  5823.98,
                  5824.01,
                  5823.36,
                  5822.82,
                  5820.41,
                  5820.38,
                  5819.78,
                  5818.92,
                  5819.16,
                  5823.51,
                  5826.59,
                  5822.25,
                  5822.37,
                  5822.6,
                  5825.49,
                  5826.54,
                  5828.2,
                  5830.46,
                  5826.09,
                  5821.52,
                  5820.48,
                  5825.26,
                  5832.02,
                  5830.5,
                  5826.15,
                  5830.79,
                  5830.41,
                  5817.47,
                  5815.74,
                  5818.62,
                  5818.28,
                  5815.37,
                  5817.55,
                  5816.71
                ],
                "chartPreviousClose": 5792.79,
                "close": [
                  5782.27,
                  5782.69,
                  5795.88,
                  5798.77,
                  5796.97,
                  5787.21,
                  5795.86,
                  5804.42,
                  5803.9,
                  5801.68,
                  5796.32,
                  5783.87,
                  5786.61,
                  5787.17,
                  5779.08,
                  5782.94,
                  5783.14,
                  5789.04,
                  5789.44,
                  5794.14,
                  5797.3,
                  5801.27,
                  5798.82,
                  5797.53,
                  5801.22,
                  5797.86,
                  5802.95,
                  5803.55,
                  5807.03,
                  5806.3,
                  5806.49,
                  5807.49,
                  5809.36,
                  5810.21,
                  5811.24,
                  5812.44,
                  5811.73,
                  5813,
                  5815.34,
                  5814.25,
                  5818.97,
                  5822.4,
                  5817.84,
                  5820.02,
                  5821.36,
                  5821.33,
                  5823.39,
                  5820.68,
                  5825.29,
                  5825.77,
                  5826.08,
                  5826.97,
                  5830.64,
                  5831.48,
                  5828.21,
                  5830.25,
                  5831.46,
                  5833.56,
                  5831,
                  5830.62,
                  5828.37,
                  5827.5,
                  5824.47,
                  5825.11,
                  5823.4,
                  5824.86,
                  5821.1,
                  5821.65,
                  5820.36,
                  5818.93,
                  5823.45,
                  5828.27,
                  5830.17,
                  5823.77,
                  5823.37,
                  5826.01,
                  5827.2,
                  5826.87,
                  5832.12,
                  5832.63,
                  5827.66,
                  5821.52,
                  5825.33,
                  5833.69,
                  5832.02,
                  5833.8,
                  5830.34,
                  5833.75,
                  5831.32,
                  5817.47,
                  5824.52,
                  5818.62,
                  5821.31,
                  5820.07,
                  5818.26,
                  5816.71
                ],
                "open": [
                  5774.62,
                  5782.45,
                  5783.39,
                  5797.09,
                  5798.4,
                  5795.61,
                  5787.26,
                  5796.17,
                  5804.26,
                  5803.68,
                  5801.28,
                  5796.05,
                  5784.05,
                  5787.19,
                  5786.44,
                  5782.69,
                  5781.87,
                  5783.7,
                  5789.38,
                  5789.86,
                  5793.7,
                  5799.06,
                  5800.65,
                  5799,
                  5797.53,
                  5801.18,
                  5798.29,
                  5802.69,
                  5804.37,
                  5806.4,
                  5806.38,
                  5806.32,
                  5807.57,
                  5809.27,
                  5809.8,
                  5811.28,
                  5811.27,
                  5811.6,
                  5813.48,
                  5815.19,
                  5814.15,
                  5819.18,
                  5822.74,
                  5818.27,
                  5820.38,
                  5821.21,
                  5821.6,
                  5824.44,
                  5819.7,
                  5825.24,
                  5825.35,
                  5826.61,
                  5827.03,
                  5830.39,
                  5831.13,
                  5826.77,
                  5830.3,
                  5831.54,
                  5833.28,
                  5830.32,
                  5831.17,
                  5828.33,
                  5827.26,
                  5824.53,
                  5825.19,
                  5823.41,
                  5824.63,
                  5820.9,
                  5821.26,
                  5820.2,
                  5819.16,
                  5823.67,
                  5828.28,
                  5829.77,
                  5823.29,
                  5822.6,
                  5826.12,
                  5827.3,
                  5828.2,
                  5831.38,
                  5832.62,
                  5828.49,
                  5821.36,
                  5825.26,
                  5832.5,
                  5832.12,
                  5834.97,
                  5830.79,
                  5832.4,
                  5828.8,
                  5815.74,
                  5822.87,
                  5818.28,
                  5820.92,
                  5820.8,
                  5818.57
                ]
              },
              {
                "symbol": "^GDAXI",
                "previousClose": 14012.82,
                "gmtoffset": 3600,
                "high": [
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  13981.49,
                  13984.58,
                  14013.75,
                  14030.51,
                  14028.43,
                  14006.96,
                  14019.21,
                  14070.04,
                  14062.49,
                  14046.2,
                  14042.14,
                  14018.72,
                  14005.53,
                  14000.09,
                  13992.61,
                  13983.97,
                  13993.57,
                  14001.15,
                  14011.93,
                  14024.31,
                  14035.2,
                  14033.26,
                  14026.53,
                  14035.45,
                  14041.53,
                  14042.18,
                  14037.16,
                  14035.01,
                  14043.07,
                  14046.15,
                  14045.2,
                  14050.53,
                  14058.78,
                  14056.92,
                  14062.36,
                  14060.11,
                  14057.23,
                  14062.49,
                  14062.33,
                  14063.07,
                  14077.69,
                  14091.94,
                  14094.35,
                  14087.01,
                  14089.4,
                  14091.84,
                  14091.33,
                  14091.95,
                  14092.5,
                  14094.69,
                  14090.05,
                  14089.33,
                  14092.03,
                  14092.5,
                  14091.48,
                  14089.75,
                  14091.8,
                  14089.57,
                  14088.21,
                  14080.48,
                  14083.28,
                  14079.8,
                  14074.05,
                  14074.1,
                  14076.84,
                  14079.08,
                  14078.58,
                  14073.09,
                  14076.76,
                  14075.58,
                  14075.18,
                  14087.9,
                  14093.72,
                  14094.87,
                  14084.18,
                  14092.29,
                  14098.78,
                  14100.15,
                  14101.79,
                  14097.66,
                  14101.29,
                  14091.43,
                  14080.73,
                  14098.95,
                  14095.72,
                  14097.21,
                  14100.37,
                  14099.87,
                  14098.21,
                  14079.94,
                  14073.56,
                  14083.37,
                  14065.53,
                  14067.6,
                  14066.93,
                  14063.52
                ],
                "low": [
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  13962.07,
                  13967.62,
                  13983.43,
                  14014.6,
                  14001.99,
                  13983.94,
                  13980.63,
                  14015.63,
                  14038.67,
                  14038.42,
                  14017.82,
                  13985.56,
                  13988.11,
                  13988.37,
                  13961.62,
                  13971.39,
                  13971.61,
                  13991.65,
                  14000.27,
                  14011.71,
                  14019.86,
                  14026.2,
                  14008.94,
                  14024.01,
                  14024.58,
                  14030.18,
                  14027.43,
                  14026.31,
                  14032.09,
                  14041.26,
                  14035.36,
                  14044.13,
                  14050.51,
                  14050.11,
                  14049.35,
                  14046.74,
                  14051.82,
                  14057.28,
                  14056.65,
                  14057.86,
                  14057.94,
                  14077.63,
                  14074.54,
                  14075.21,
                  14084.13,
                  14077.75,
                  14082.77,
                  14083.67,
                  14082.83,
                  14087.7,
                  14086.07,
                  14075.84,
                  14083.34,
                  14089.65,
                  14083.56,
                  14081.6,
                  14085.99,
                  14085.2,
                  14079.97,
                  14072.7,
                  14076.25,
                  14071.37,
                  14066.6,
                  14067.16,
                  14071.26,
                  14073.1,
                  14069.41,
                  14068.25,
                  14066.88,
                  14063.79,
                  14068.15,
                  14074.5,
                  14084.38,
                  14073.8,
                  14076,
                  14080.52,
                  14091.79,
                  14093.57,
                  14091.43,
                  14081.35,
                  14078.99,
                  14054.94,
                  14054.77,
                  14074.94,
                  14086.31,
                  14084.61,
                  14075.15,
                  14088.01,
                  14079.92,
                  14053.99,
                  14053.63,
                  14060.78,
                  14059.21,
                  14050.58,
                  14060.23,
                  14056.21
                ],
                "chartPreviousClose": 14012.82,
                "close": [
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  13981.38,
                  13983.51,
                  14013.75,
                  14023.44,
                  14002.88,
                  13986.1,
                  14018.83,
                  14048.53,
                  14042.21,
                  14039.78,
                  14018.72,
                  13989.75,
                  14000.09,
                  13992.61,
                  13966.81,
                  13973.91,
                  13993.57,
                  14001.15,
                  14011.66,
                  14019.73,
                  14029.46,
                  14026.76,
                  14023.99,
                  14025.01,
                  14034.26,
                  14032.1,
                  14028.21,
                  14033.22,
                  14042.24,
                  14043.01,
                  14044.55,
                  14050.34,
                  14056.78,
                  14051.58,
                  14056.95,
                  14053.55,
                  14057.23,
                  14058.99,
                  14062.31,
                  14057.86,
                  14077.69,
                  14088.22,
                  14075.27,
                  14084.13,
                  14089.02,
                  14082.4,
                  14091.23,
                  14083.74,
                  14091.91,
                  14088,
                  14087.66,
                  14083.34,
                  14091.95,
                  14090.01,
                  14083.61,
                  14087.28,
                  14088.65,
                  14085.65,
                  14080.02,
                  14080.18,
                  14076.25,
                  14072.13,
                  14067.16,
                  14072.5,
                  14073.78,
                  14077.78,
                  14070.52,
                  14071.29,
                  14072.33,
                  14068.34,
                  14075.18,
                  14087.4,
                  14091.77,
                  14076.6,
                  14080.86,
                  14092.2,
                  14097.94,
                  14094.92,
                  14095.49,
                  14096.62,
                  14087.08,
                  14055.81,
                  14077.71,
                  14091.44,
                  14086.49,
                  14095.8,
                  14088.62,
                  14090.18,
                  14079.92,
                  14054.12,
                  14069.98,
                  14061.83,
                  14061.3,
                  14067.03,
                  14061.96,
                  14056.28
                ],
                "open": [
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  13962.07,
                  13980.86,
                  13983.43,
                  14014.69,
                  14023.44,
                  14002.88,
                  13985.88,
                  14019.09,
                  14049.17,
                  14042.07,
                  14039.67,
                  14018.72,
                  13989.75,
                  14000.09,
                  13992.61,
                  13973.97,
                  13973.87,
                  13993.68,
                  14002.11,
                  14011.71,
                  14019.91,
                  14029.54,
                  14026.21,
                  14024.08,
                  14025.01,
                  14034.31,
                  14032.23,
                  14028.21,
                  14033.22,
                  14042.24,
                  14043.01,
                  14044.29,
                  14050.51,
                  14056.55,
                  14051.32,
                  14056.88,
                  14053.14,
                  14057.49,
                  14058.99,
                  14062.43,
                  14057.98,
                  14077.74,
                  14088.29,
                  14075.21,
                  14084.13,
                  14089.02,
                  14082.77,
                  14091.42,
                  14083.53,
                  14091.91,
                  14088,
                  14087.78,
                  14083.34,
                  14091.95,
                  14090.01,
                  14082.26,
                  14087.22,
                  14088.65,
                  14085.69,
                  14080.02,
                  14080.18,
                  14076.25,
                  14072.13,
                  14067.16,
                  14072.5,
                  14073.65,
                  14077.57,
                  14070.52,
                  14071.29,
                  14071.62,
                  14068.34,
                  14075.18,
                  14087.4,
                  14091.77,
                  14076.55,
                  14080.69,
                  14092.2,
                  14097.94,
                  14095.11,
                  14095.45,
                  14096.68,
                  14086.88,
                  14055,
                  14077.79,
                  14091.44,
                  14087.08,
                  14096.05,
                  14088.62,
                  14088.91,
                  14079.76,
                  14053.63,
                  14069.97,
                  14061.83,
                  14061.43,
                  14066.93,
                  14062.27
                ]
              }
            ],
            "indicators": {
              "quote": [
                {
                  "high": [
                    0.7875999808311462,
                    0.7889000177383423,
                    0.789900004863739,
                    0.7897999882698059,
                    0.7883999943733215,
                    0.7878000140190125,
                    0.7876999974250793,
                    0.7876999974250793,
                    0.7872999906539917,
                    0.7876999974250793,
                    0.7883999943733215,
                    0.7883999943733215,
                    0.7883999943733215,
                    0.7883999943733215,
                    0.7883999943733215,
                    0.7888000011444092,
                    0.7919999957084656,
                    0.7918999791145325,
                    0.7907000184059143,
                    0.7914999723434448,
                    0.7918000221252441,
                    0.7918999791145325,
                    0.7915999889373779,
                    0.7914000153541565,
                    0.791100025177002,
                    0.7903000116348267,
                    0.791100025177002,
                    0.7904999852180481,
                    0.7912999987602234,
                    0.7904999852180481,
                    0.7908999919891357,
                    0.7908999919891357,
                    0.7910000085830688,
                    0.7910000085830688,
                    0.792900025844574,
                    0.7925999760627747,
                    0.792900025844574,
                    0.7924000024795532,
                    0.792900025844574,
                    0.7929999828338623,
                    0.7932000160217285,
                    0.7932999730110168,
                    0.7929999828338623,
                    0.7922999858856201,
                    0.7918999791145325,
                    0.7924000024795532,
                    0.7924000024795532,
                    0.7925000190734863,
                    0.7925000190734863,
                    0.7926999926567078,
                    0.7990000247955322,
                    0.798799991607666,
                    0.7985000014305115,
                    0.8022000193595886,
                    0.8022000193595886,
                    0.8004999756813049,
                    0.8009999990463257,
                    0.8012999892234802,
                    0.8014000058174133,
                    0.8033999800682068,
                    0.8033000230789185,
                    0.8033999800682068,
                    0.8027999997138977,
                    0.802299976348877,
                    0.8014000058174133,
                    0.8014000058174133,
                    0.8009999990463257,
                    0.8001000285148621,
                    0.7993000149726868,
                    0.8012999892234802,
                    0.8016999959945679,
                    0.8015000224113464,
                    0.8014000058174133,
                    0.8015999794006348,
                    0.8011999726295471,
                    0.8012999892234802,
                    0.8033999800682068,
                    0.8046000003814697,
                    0.8039000034332275,
                    0.8033999800682068,
                    0.8029999732971191,
                    0.8029999732971191,
                    0.8033000230789185,
                    0.8041999936103821,
                    0.8047999739646912,
                    0.8050000071525574,
                    0.8068000078201294,
                    0.8068000078201294,
                    0.8065999746322632,
                    0.8068000078201294,
                    0.8068000078201294,
                    0.8066999912261963,
                    0.8062999844551086,
                    0.8062999844551086,
                    0.8062999844551086,
                    0.8059999942779541,
                    0.8066999912261963,
                    0.8073999881744385,
                    0.8066999912261963,
                    0.8065999746322632,
                    0.8062999844551086,
                    0.8061000108718872,
                    0.8062000274658203,
                    0.8062999844551086,
                    0.8064000010490417,
                    null,
                    0.8059999942779541,
                    null,
                    null
                  ],
                  "low": [
                    0.7875999808311462,
                    0.7854999899864197,
                    0.7885000109672546,
                    0.7879999876022339,
                    0.7875000238418579,
                    0.7871000170707703,
                    0.7857999801635742,
                    0.7871000170707703,
                    0.7870000004768372,
                    0.7870000004768372,
                    0.7847999930381775,
                    0.7875000238418579,
                    0.7876999974250793,
                    0.7875999808311462,
                    0.7878000140190125,
                    0.7882000207901001,
                    0.7886999845504761,
                    0.789900004863739,
                    0.789900004863739,
                    0.7900000214576721,
                    0.7907000184059143,
                    0.7908999919891357,
                    0.7907000184059143,
                    0.7906000018119812,
                    0.7900000214576721,
                    0.789900004863739,
                    0.789900004863739,
                    0.789900004863739,
                    0.7901999950408936,
                    0.789900004863739,
                    0.789900004863739,
                    0.7900999784469604,
                    0.7901999950408936,
                    0.7904999852180481,
                    0.789900004863739,
                    0.7914999723434448,
                    0.7921000123023987,
                    0.7921000123023987,
                    0.7919999957084656,
                    0.7922000288963318,
                    0.7919999957084656,
                    0.7919999957084656,
                    0.7919999957084656,
                    0.7915999889373779,
                    0.7901999950408936,
                    0.7914000153541565,
                    0.7912999987602234,
                    0.7914999723434448,
                    0.7914000153541565,
                    0.791700005531311,
                    0.7919999957084656,
                    0.7940000295639038,
                    0.7958999872207642,
                    0.7983999848365784,
                    0.7994999885559082,
                    0.7991999983787537,
                    0.7975000143051147,
                    0.8001999855041504,
                    0.8001000285148621,
                    0.8011000156402588,
                    0.8011999726295471,
                    0.8011999726295471,
                    0.8015000224113464,
                    0.8001999855041504,
                    0.8001999855041504,
                    0.8001000285148621,
                    0.7991999983787537,
                    0.7976999878883362,
                    0.7971000075340271,
                    0.7990000247955322,
                    0.8004999756813049,
                    0.8008999824523926,
                    0.8008000254631042,
                    0.8003000020980835,
                    0.800000011920929,
                    0.800599992275238,
                    0.8003000020980835,
                    0.8033000230789185,
                    0.8029000163078308,
                    0.8021000027656555,
                    0.8019999861717224,
                    0.802299976348877,
                    0.8029000163078308,
                    0.8029999732971191,
                    0.8036999702453613,
                    0.8041999936103821,
                    0.8048999905586243,
                    0.8058000206947327,
                    0.8043000102043152,
                    0.805400013923645,
                    0.8059999942779541,
                    0.8051000237464905,
                    0.8032000064849854,
                    0.8047999739646912,
                    0.804099977016449,
                    0.8047000169754028,
                    0.805400013923645,
                    0.8059999942779541,
                    0.8058000206947327,
                    0.8059999942779541,
                    0.8058000206947327,
                    0.8057000041007996,
                    0.805400013923645,
                    0.8051999807357788,
                    0.8047000169754028,
                    null,
                    0.8059999942779541,
                    null,
                    null
                  ],
                  "close": [
                    0.7875999808311462,
                    0.7889000177383423,
                    0.7889000177383423,
                    0.7879999876022339,
                    0.7876999974250793,
                    0.7871000170707703,
                    0.7876999974250793,
                    0.7872999906539917,
                    0.7871000170707703,
                    0.7875000238418579,
                    0.7883999943733215,
                    0.7878999710083008,
                    0.7883999943733215,
                    0.7883999943733215,
                    0.7882000207901001,
                    0.7888000011444092,
                    0.7919999957084656,
                    0.7910000085830688,
                    0.7900000214576721,
                    0.7907999753952026,
                    0.7915999889373779,
                    0.7910000085830688,
                    0.7912999987602234,
                    0.7908999919891357,
                    0.7900000214576721,
                    0.7900000214576721,
                    0.7904999852180481,
                    0.7903000116348267,
                    0.7901999950408936,
                    0.7901999950408936,
                    0.7907999753952026,
                    0.7908999919891357,
                    0.7910000085830688,
                    0.7907999753952026,
                    0.7925000190734863,
                    0.7919999957084656,
                    0.7924000024795532,
                    0.7924000024795532,
                    0.7926999926567078,
                    0.792900025844574,
                    0.7932000160217285,
                    0.7919999957084656,
                    0.7921000123023987,
                    0.7918000221252441,
                    0.7918000221252441,
                    0.7922000288963318,
                    0.7922999858856201,
                    0.7922000288963318,
                    0.7919999957084656,
                    0.7919999957084656,
                    0.7985000014305115,
                    0.7964000105857849,
                    0.7982000112533569,
                    0.8021000027656555,
                    0.7998999953269958,
                    0.7991999983787537,
                    0.8009999990463257,
                    0.8011999726295471,
                    0.8011999726295471,
                    0.8016999959945679,
                    0.8026000261306763,
                    0.8022000193595886,
                    0.8019999861717224,
                    0.8004999756813049,
                    0.8012999892234802,
                    0.8011000156402588,
                    0.8004999756813049,
                    0.7989000082015991,
                    0.7993000149726868,
                    0.8009999990463257,
                    0.8009999990463257,
                    0.8014000058174133,
                    0.8012999892234802,
                    0.8003000020980835,
                    0.8008000254631042,
                    0.8007000088691711,
                    0.8033000230789185,
                    0.8039000034332275,
                    0.8030999898910522,
                    0.8027999997138977,
                    0.8023999929428101,
                    0.8026999831199646,
                    0.8033000230789185,
                    0.8036999702453613,
                    0.8043000102043152,
                    0.8048999905586243,
                    0.805899977684021,
                    0.8064000010490417,
                    0.8057000041007996,
                    0.8065999746322632,
                    0.8059999942779541,
                    0.8051000237464905,
                    0.8062999844551086,
                    0.8050000071525574,
                    0.8048999905586243,
                    0.8058000206947327,
                    0.8062000274658203,
                    0.8059999942779541,
                    0.8065000176429749,
                    0.8062999844551086,
                    0.8058000206947327,
                    0.8057000041007996,
                    0.8055999875068665,
                    0.8057000041007996,
                    0.8050000071525574,
                    null,
                    0.8059999942779541,
                    null,
                    null
                  ],
                  "volume": [
                    0,
                    4871000,
                    4317000,
                    7206000,
                    2419000,
                    1569000,
                    2909000,
                    751000,
                    2818000,
                    1212000,
                    18423000,
                    4674000,
                    3159000,
                    2035000,
                    0,
                    62544000,
                    22623000,
                    6886000,
                    2086000,
                    4621000,
                    2334000,
                    1863000,
                    987000,
                    2840000,
                    1037000,
                    3152000,
                    2618000,
                    1823000,
                    2235000,
                    583000,
                    1016000,
                    793000,
                    961000,
                    1169000,
                    25089000,
                    4823000,
                    2118000,
                    424000,
                    1607000,
                    1172000,
                    5422000,
                    7629000,
                    3033000,
                    2495000,
                    11175000,
                    3140000,
                    2222000,
                    529000,
                    1810000,
                    1943000,
                    86834000,
                    31015000,
                    28216000,
                    48080000,
                    18647000,
                    9401000,
                    35428000,
                    17466000,
                    15762000,
                    25913000,
                    20310000,
                    15377000,
                    7413000,
                    13755000,
                    3268000,
                    8494000,
                    10034000,
                    7367000,
                    7276000,
                    17623000,
                    1649000,
                    1268000,
                    1463000,
                    2081000,
                    3291000,
                    1802000,
                    16110000,
                    9962000,
                    4472000,
                    11665000,
                    1561000,
                    1625000,
                    2226000,
                    6450000,
                    4432000,
                    6271000,
                    16441000,
                    3650000,
                    11517000,
                    2209000,
                    2276000,
                    6236000,
                    8219000,
                    6424000,
                    5975000,
                    1741000,
                    5606000,
                    7328000,
                    3303000,
                    1809000,
                    4505000,
                    2857000,
                    4697000,
                    3433000,
                    7587000,
                    null,
                    6679000,
                    null,
                    null
                  ],
                  "open": [
                    0.7875999808311462,
                    0.7875000238418579,
                    0.7886000275611877,
                    0.7893999814987183,
                    0.7879999876022339,
                    0.7878000140190125,
                    0.7871000170707703,
                    0.7875000238418579,
                    0.7871999740600586,
                    0.7871999740600586,
                    0.7875000238418579,
                    0.7882999777793884,
                    0.7878999710083008,
                    0.7882999777793884,
                    0.7883999943733215,
                    0.7883999943733215,
                    0.7886999845504761,
                    0.7914999723434448,
                    0.7903000116348267,
                    0.7904000282287598,
                    0.7911999821662903,
                    0.7915999889373779,
                    0.7912999987602234,
                    0.7912999987602234,
                    0.791100025177002,
                    0.7900000214576721,
                    0.7900000214576721,
                    0.7904999852180481,
                    0.7907000184059143,
                    0.7904999852180481,
                    0.7903000116348267,
                    0.7904999852180481,
                    0.7908999919891357,
                    0.7908999919891357,
                    0.7907999753952026,
                    0.7921000123023987,
                    0.7921000123023987,
                    0.7924000024795532,
                    0.7922999858856201,
                    0.7922000288963318,
                    0.7929999828338623,
                    0.7932999730110168,
                    0.792900025844574,
                    0.7922999858856201,
                    0.7918000221252441,
                    0.7919999957084656,
                    0.7922999858856201,
                    0.7922999858856201,
                    0.7922000288963318,
                    0.7919999957084656,
                    0.7919999957084656,
                    0.7985000014305115,
                    0.7958999872207642,
                    0.7983999848365784,
                    0.8015999794006348,
                    0.7997999787330627,
                    0.7998999953269958,
                    0.8009999990463257,
                    0.8009999990463257,
                    0.8012999892234802,
                    0.801800012588501,
                    0.8029999732971191,
                    0.8023999929428101,
                    0.8019999861717224,
                    0.8004999756813049,
                    0.8007000088691711,
                    0.8004999756813049,
                    0.8001000285148621,
                    0.7989000082015991,
                    0.7990000247955322,
                    0.8009999990463257,
                    0.8011999726295471,
                    0.8009999990463257,
                    0.8014000058174133,
                    0.8007000088691711,
                    0.8008999824523926,
                    0.8008999824523926,
                    0.8033000230789185,
                    0.8039000034332275,
                    0.8030999898910522,
                    0.8029000163078308,
                    0.8023999929428101,
                    0.8029000163078308,
                    0.8033000230789185,
                    0.8036999702453613,
                    0.8044999837875366,
                    0.8048999905586243,
                    0.8059999942779541,
                    0.8064000010490417,
                    0.8055999875068665,
                    0.8065000176429749,
                    0.8062000274658203,
                    0.805400013923645,
                    0.8062000274658203,
                    0.8051999807357788,
                    0.8051999807357788,
                    0.8055999875068665,
                    0.8065999746322632,
                    0.8059999942779541,
                    0.8059999942779541,
                    0.8061000108718872,
                    0.8058000206947327,
                    0.8057000041007996,
                    0.8057000041007996,
                    0.8057000041007996,
                    null,
                    0.8059999942779541,
                    null,
                    null
                  ]
                }
              ]
            }
          }
        ],
        "error": null
      }
    }
     */
    #endregion

    #region Watchlist JSON Object
    /// <summary>
    /// Watchlist Object
    /// </summary>
    public class Watchlist
    {
        [JsonProperty("finance")]
        public WatchlistResponse Finance { get; set; }
    }

    /// <summary>
    /// Watchlist Response results
    /// </summary>
    public partial class WatchlistResponse
    {
        [JsonProperty("result")]
        public List<WatchlistResult> Result { get; set; }

        [JsonProperty("error")]
        public dynamic WatchlistError { get; set; }
    }

    /// <summary>
    /// Watchlist Resulsts list items
    /// </summary>
    public partial class WatchlistResult
    {
        [JsonProperty("portfolio")]
        public WatchlistSymbol Portfolio { get; set; }

        [JsonProperty("symbols")]
        public dynamic SymbolsList { get; set; }

        public List<WatchlistSymbol> Symbols { get; set; }
    }

    /// <summary>
    /// Watchlist symbol object
    /// </summary>
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
    /* Sample Watchlist Response
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

    #region Earnings JSON Object
    /// <summary>
    /// Earnings Object
    /// </summary>
    public partial class Earnings
    {
        [JsonProperty("finance")]
        public EarningsResponse Finance { get; set; }
    }

    /// <summary>
    /// Earnings Response result
    /// </summary>
    public partial class EarningsResponse
    {
        [JsonProperty("result")]
        public List<EarningsResult> Result { get; set; }

        [JsonProperty("error")]
        public dynamic Error { get; set; }
    }

    /// <summary>
    /// Earnings Result object list
    /// </summary>
    public partial class EarningsResult
    {
        [JsonProperty("ticker")]
        public string Ticker { get; set; }

        [JsonProperty("companyShortName")]
        public string CompanyShortName { get; set; }

        [JsonProperty("startDateTime")]
        public long StartDateTime { get; set; }

        [JsonProperty("startDateTimeType")]
        public StartDateTimeType StartDateTimeType { get; set; }

        [JsonProperty("surprisePercent")]
        public double SurprisePercent { get; set; }

        [JsonProperty("rank")]
        public long Rank { get; set; }
    }

    public enum StartDateTimeType { Tns };

    // Sample Earnings Response
    /*
     {
      "finance": {
        "result": [
          {
            "ticker": "GME",
            "companyShortName": "GameStop Corp.",
            "startDateTime": 1585180800000,
            "startDateTimeType": "TNS",
            "surprisePercent": 61.37,
            "rank": 11770649
          },
          {
            "ticker": "SNDL",
            "companyShortName": "Sundial Growers Inc.",
            "startDateTime": 1585526400000,
            "startDateTimeType": "TNS",
            "surprisePercent": -635.14,
            "rank": 999643
          },
          {
            "ticker": "BB",
            "companyShortName": "BlackBerry Limited",
            "startDateTime": 1585612800000,
            "startDateTimeType": "TNS",
            "surprisePercent": 119.51,
            "rank": 790212
          },
          {
            "ticker": "T",
            "companyShortName": "AT&T Inc.",
            "startDateTime": 1587513600000,
            "startDateTimeType": "TNS",
            "surprisePercent": -0.94,
            "rank": 494005
          },
          {
            "ticker": "OCGN",
            "companyShortName": "Ocugen, Inc.",
            "startDateTime": 1585267200000,
            "startDateTimeType": "TNS",
            "surprisePercent": 3050,
            "rank": 467824
          },
          {
            "ticker": "JNJ",
            "companyShortName": "Johnson & Johnson",
            "startDateTime": 1586822400000,
            "startDateTimeType": "TNS",
            "surprisePercent": 15.29,
            "rank": 341147
          },
          {
            "ticker": "CYDY",
            "companyShortName": "CytoDyn Inc.",
            "startDateTime": 1586390400000,
            "startDateTimeType": "TNS",
            "surprisePercent": -166.67,
            "rank": 332051
          },
          {
            "ticker": "APHA",
            "companyShortName": "Aphria Inc.",
            "startDateTime": 1586822400000,
            "startDateTimeType": "TNS",
            "surprisePercent": 148.78,
            "rank": 312749
          },
          {
            "ticker": "BAC",
            "companyShortName": "Bank of America Corporation",
            "startDateTime": 1586908800000,
            "startDateTimeType": "TNS",
            "surprisePercent": -13.23,
            "rank": 275799
          },
          {
            "ticker": "TSM",
            "companyShortName": "Taiwan Semiconductor Manufacturing Company Limited",
            "startDateTime": 1586995200000,
            "startDateTimeType": "TNS",
            "surprisePercent": 2.32,
            "rank": 267987
          }
        ],
        "error": null
        }
     }
     */
    #endregion

    #region Trending JSON Object
    /// <summary>
    /// Trending object
    /// </summary>
    public partial class Trending
    {
        [JsonProperty("finance")]
        public TrendingResponse Finance { get; set; }
    }

    /// <summary>
    /// Trending Response result
    /// </summary>
    public partial class TrendingResponse
    {
        [JsonProperty("result")]
        public List<TrendingResult> Result { get; set; }

        [JsonProperty("error")]
        public dynamic TrendingError { get; set; }
    }

    /// <summary>
    /// Trending Portfolio result
    /// </summary>
    public partial class TrendingResult
    {
        [JsonProperty("portfolio")]
        public TrendingPortfolio Portfolio { get; set; }

        [JsonProperty("symbols")]
        public TrendingSymbols Symbols { get; set; }
    }

    /// <summary>
    /// Trending Portfolio
    /// </summary>
    public partial class TrendingPortfolio
    {
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

    public partial class TrendingSymbols
    {
        [JsonProperty("MSFT")]
        public Stock Msft { get; set; }

        [JsonProperty("AAPL")]
        public Stock Aapl { get; set; }

        [JsonProperty("TSLA")]
        public Stock Tsla { get; set; }

        [JsonProperty("CSCO")]
        public Stock Csco { get; set; }

        [JsonProperty("T")]
        public Stock T { get; set; }

        [JsonProperty("MS")]
        public Stock Ms { get; set; }

        [JsonProperty("JNJ")]
        public Stock Jnj { get; set; }

        [JsonProperty("PG")]
        public Stock Pg { get; set; }

        [JsonProperty("SCHW")]
        public Stock Schw { get; set; }

        [JsonProperty("FB")]
        public Stock Fb { get; set; }
    }

    /// <summary>
    /// Stock object (reusable)
    /// </summary>
    public partial class Stock
    {
        [JsonProperty("oneDayPercentChange")]
        public double OneDayPercentChange { get; set; }

        [JsonProperty("oneMonthPercentChange")]
        public double OneMonthPercentChange { get; set; }

        [JsonProperty("oneYearPercentChange")]
        public double OneYearPercentChange { get; set; }

        [JsonProperty("lifetimePercentChange")]
        public string LifetimePercentChange { get; set; }

        [JsonProperty("originTimestamp")]
        public long OriginTimestamp { get; set; }

        [JsonProperty("updatedAt")]
        public long UpdatedAt { get; set; }
    }
    #endregion

    #region Historical Data JSON Object
    /// <summary>
    /// HistoricalData Object
    /// </summary>
    public partial class HistoricalData
    {
        [JsonProperty("prices")]
        public List<Price> Prices { get; set; }

        [JsonProperty("isPending")]
        public bool IsPending { get; set; }

        [JsonProperty("firstTradeDate")]
        public long FirstTradeDate { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("timeZone")]
        public TimeZone TimeZone { get; set; }

        [JsonProperty("eventsData")]
        public List<dynamic> EventsData { get; set; }
    }

    /// <summary>
    /// Price Attribute
    /// </summary>
    public partial class Price
    {
        [JsonProperty("date")]
        public long Date { get; set; }

        [JsonProperty("open")]
        public double Open { get; set; }

        [JsonProperty("high")]
        public double High { get; set; }

        [JsonProperty("low")]
        public double Low { get; set; }

        [JsonProperty("close")]
        public double Close { get; set; }

        [JsonProperty("volume")]
        public long Volume { get; set; }

        [JsonProperty("adjclose")]
        public double Adjclose { get; set; }
    }

    /// <summary>
    /// TimeZone Attribute
    /// </summary>
    public partial class TimeZone
    {
        [JsonProperty("gmtOffset")]
        public long GmtOffset { get; set; }
    }

    /*
     {
        "prices": [
        {
            "date": 1614718801,
            "open": 6.170000076293945,
            "high": 6.28000020980835,
            "low": 5.940000057220459,
            "close": 6.03000020980835,
            "volume": 11547574,
            "adjclose": 6.03000020980835
        },
        {
            "date": 1614609000,
            "open": 6.920000076293945,
            "high": 6.989999771118164,
            "low": 6.170000076293945,
            "close": 6.190000057220459,
            "volume": 15460700,
            "adjclose": 6.190000057220459
        },
        {
            "date": 1614349800,
            "open": 7.03000020980835,
            "high": 7.079999923706055,
            "low": 6.71999979019165,
            "close": 6.739999771118164,
            "volume": 6395100,
            "adjclose": 6.739999771118164
        },
        {
            "date": 1614263400,
            "open": 7.440000057220459,
            "high": 7.559999942779541,
            "low": 6.949999809265137,
            "close": 6.949999809265137,
            "volume": 5631200,
            "adjclose": 6.949999809265137
        }
        ],
        "isPending": false,
        "firstTradeDate": 733674600,
        "id": "",
        "timeZone": {
        "gmtOffset": -18000
        },
        "eventsData": []
     }
     */
    #endregion

    #region Stock Analysis JSON Object
    /// <summary>
    /// StockAnalysis Object
    /// </summary>
    public partial class StockAnalysis
    {
        [JsonProperty("recommendationTrend")]
        public RecommendationTrend RecommendationTrend { get; set; }

        [JsonProperty("financialsTemplate")]
        public FinancialsTemplate FinancialsTemplate { get; set; }

        [JsonProperty("price")]
        public Price Price { get; set; }

        [JsonProperty("earningsHistory")]
        public EarningsHistory EarningsHistory { get; set; }

        [JsonProperty("indexTrend")]
        public IndexTrend IndexTrend { get; set; }

        [JsonProperty("financialData")]
        public FinancialData FinancialData { get; set; }

        [JsonProperty("earningsTrend")]
        public EarningsTrend EarningsTrend { get; set; }

        [JsonProperty("quoteType")]
        public QuoteType QuoteType { get; set; }

        [JsonProperty("sectorTrend")]
        public Trend SectorTrend { get; set; }

        [JsonProperty("summaryDetail")]
        public SummaryDetail SummaryDetail { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("upgradeDowngradeHistory")]
        public UpgradeDowngradeHistory UpgradeDowngradeHistory { get; set; }

        [JsonProperty("pageViews")]
        public PageViews PageViews { get; set; }

        [JsonProperty("industryTrend")]
        public Trend IndustryTrend { get; set; }
    }

    public partial class EarningsHistory
    {
        [JsonProperty("history")]
        public List<EarningsHistoryHistory> History { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }
    }

    public partial class EarningsHistoryHistory
    {
        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("epsActual")]
        public CurrentPrice EpsActual { get; set; }

        [JsonProperty("epsEstimate")]
        public CurrentPrice EpsEstimate { get; set; }

        [JsonProperty("epsDifference")]
        public CurrentPrice EpsDifference { get; set; }

        [JsonProperty("surprisePercent")]
        public CurrentPrice SurprisePercent { get; set; }

        [JsonProperty("quarter")]
        public CurrentPrice Quarter { get; set; }

        [JsonProperty("period")]
        public string Period { get; set; }
    }

    public partial class CurrentPrice
    {
        [JsonProperty("raw", NullValueHandling = NullValueHandling.Ignore)]
        public double? Raw { get; set; }

        [JsonProperty("fmt", NullValueHandling = NullValueHandling.Ignore)]
        public string Fmt { get; set; }
    }

    public partial class EarningsTrend
    {
        [JsonProperty("trend")]
        public List<EarningsTrendTrend> Trend { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }
    }

    public partial class EarningsTrendTrend
    {
        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("endDate")]
        public DateTimeOffset? EndDate { get; set; }

        [JsonProperty("growth")]
        public CurrentPrice Growth { get; set; }

        [JsonProperty("earningsEstimate")]
        public EarningsEstimate EarningsEstimate { get; set; }

        [JsonProperty("revenueEstimate")]
        public RevenueEstimate RevenueEstimate { get; set; }

        [JsonProperty("epsTrend")]
        public EpsTrend EpsTrend { get; set; }

        [JsonProperty("epsRevisions")]
        public EpsRevisions EpsRevisions { get; set; }
    }

    public partial class EarningsEstimate
    {
        [JsonProperty("avg")]
        public CurrentPrice Avg { get; set; }

        [JsonProperty("low")]
        public CurrentPrice Low { get; set; }

        [JsonProperty("high")]
        public CurrentPrice High { get; set; }

        [JsonProperty("yearAgoEps")]
        public CurrentPrice YearAgoEps { get; set; }

        [JsonProperty("numberOfAnalysts")]
        public Ebitda NumberOfAnalysts { get; set; }

        [JsonProperty("growth")]
        public CurrentPrice Growth { get; set; }
    }

    public partial class Ebitda
    {
        [JsonProperty("raw", NullValueHandling = NullValueHandling.Ignore)]
        public long? Raw { get; set; }

        [JsonProperty("fmt")]
        public string Fmt { get; set; }

        [JsonProperty("longFmt", NullValueHandling = NullValueHandling.Ignore)]
        public string LongFmt { get; set; }
    }

    public partial class EpsRevisions
    {
        [JsonProperty("upLast7days")]
        public Ebitda UpLast7Days { get; set; }

        [JsonProperty("upLast30days")]
        public Ebitda UpLast30Days { get; set; }

        [JsonProperty("downLast30days")]
        public Ebitda DownLast30Days { get; set; }

        [JsonProperty("downLast90days")]
        public PeRatio DownLast90Days { get; set; }
    }

    public partial class PeRatio
    {
    }

    public partial class EpsTrend
    {
        [JsonProperty("current")]
        public CurrentPrice Current { get; set; }

        [JsonProperty("7daysAgo")]
        public CurrentPrice The7DaysAgo { get; set; }

        [JsonProperty("30daysAgo")]
        public CurrentPrice The30DaysAgo { get; set; }

        [JsonProperty("60daysAgo")]
        public CurrentPrice The60DaysAgo { get; set; }

        [JsonProperty("90daysAgo")]
        public CurrentPrice The90DaysAgo { get; set; }
    }

    public partial class RevenueEstimate
    {
        [JsonProperty("avg")]
        public Ebitda Avg { get; set; }

        [JsonProperty("low")]
        public Ebitda Low { get; set; }

        [JsonProperty("high")]
        public Ebitda High { get; set; }

        [JsonProperty("numberOfAnalysts")]
        public Ebitda NumberOfAnalysts { get; set; }

        [JsonProperty("yearAgoRevenue")]
        public Ebitda YearAgoRevenue { get; set; }

        [JsonProperty("growth")]
        public CurrentPrice Growth { get; set; }
    }

    public partial class FinancialData
    {
        [JsonProperty("ebitdaMargins")]
        public CurrentPrice EbitdaMargins { get; set; }

        [JsonProperty("profitMargins")]
        public CurrentPrice ProfitMargins { get; set; }

        [JsonProperty("grossMargins")]
        public CurrentPrice GrossMargins { get; set; }

        [JsonProperty("operatingCashflow")]
        public Ebitda OperatingCashflow { get; set; }

        [JsonProperty("revenueGrowth")]
        public CurrentPrice RevenueGrowth { get; set; }

        [JsonProperty("operatingMargins")]
        public CurrentPrice OperatingMargins { get; set; }

        [JsonProperty("ebitda")]
        public Ebitda Ebitda { get; set; }

        [JsonProperty("targetLowPrice")]
        public CurrentPrice TargetLowPrice { get; set; }

        [JsonProperty("recommendationKey")]
        public string RecommendationKey { get; set; }

        [JsonProperty("grossProfits")]
        public Ebitda GrossProfits { get; set; }

        [JsonProperty("freeCashflow")]
        public Ebitda FreeCashflow { get; set; }

        [JsonProperty("targetMedianPrice")]
        public CurrentPrice TargetMedianPrice { get; set; }

        [JsonProperty("currentPrice")]
        public CurrentPrice CurrentPrice { get; set; }

        [JsonProperty("earningsGrowth")]
        public CurrentPrice EarningsGrowth { get; set; }

        [JsonProperty("currentRatio")]
        public CurrentPrice CurrentRatio { get; set; }

        [JsonProperty("returnOnAssets")]
        public CurrentPrice ReturnOnAssets { get; set; }

        [JsonProperty("numberOfAnalystOpinions")]
        public Ebitda NumberOfAnalystOpinions { get; set; }

        [JsonProperty("targetMeanPrice")]
        public CurrentPrice TargetMeanPrice { get; set; }

        [JsonProperty("debtToEquity")]
        public CurrentPrice DebtToEquity { get; set; }

        [JsonProperty("returnOnEquity")]
        public CurrentPrice ReturnOnEquity { get; set; }

        [JsonProperty("targetHighPrice")]
        public CurrentPrice TargetHighPrice { get; set; }

        [JsonProperty("totalCash")]
        public Ebitda TotalCash { get; set; }

        [JsonProperty("totalDebt")]
        public Ebitda TotalDebt { get; set; }

        [JsonProperty("totalRevenue")]
        public Ebitda TotalRevenue { get; set; }

        [JsonProperty("totalCashPerShare")]
        public CurrentPrice TotalCashPerShare { get; set; }

        [JsonProperty("financialCurrency")]
        public string FinancialCurrency { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("revenuePerShare")]
        public CurrentPrice RevenuePerShare { get; set; }

        [JsonProperty("quickRatio")]
        public CurrentPrice QuickRatio { get; set; }

        [JsonProperty("recommendationMean")]
        public CurrentPrice RecommendationMean { get; set; }
    }

    public partial class IndexTrend
    {
        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("peRatio")]
        public CurrentPrice PeRatio { get; set; }

        [JsonProperty("pegRatio")]
        public CurrentPrice PegRatio { get; set; }

        [JsonProperty("estimates")]
        public List<Estimate> Estimates { get; set; }
    }

    public partial class Estimate
    {
        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("growth")]
        public CurrentPrice Growth { get; set; }
    }

    public partial class Trend
    {
        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("symbol")]
        public dynamic Symbol { get; set; }

        [JsonProperty("peRatio")]
        public PeRatio PeRatio { get; set; }

        [JsonProperty("pegRatio")]
        public PeRatio PegRatio { get; set; }

        [JsonProperty("estimates")]
        public List<dynamic> Estimates { get; set; }
    }

    public partial class PageViews
    {
        [JsonProperty("shortTermTrend")]
        public string ShortTermTrend { get; set; }

        [JsonProperty("midTermTrend")]
        public string MidTermTrend { get; set; }

        [JsonProperty("longTermTrend")]
        public string LongTermTrend { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }
    }

    public partial class Price
    {
        [JsonProperty("quoteSourceName")]
        public string QuoteSourceName { get; set; }

        [JsonProperty("regularMarketOpen")]
        public CurrentPrice RegularMarketOpen { get; set; }

        [JsonProperty("averageDailyVolume3Month")]
        public Ebitda AverageDailyVolume3Month { get; set; }

        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("regularMarketTime")]
        public long RegularMarketTime { get; set; }

        [JsonProperty("volume24Hr")]
        public PeRatio Volume24Hr { get; set; }

        [JsonProperty("regularMarketDayHigh")]
        public CurrentPrice RegularMarketDayHigh { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("averageDailyVolume10Day")]
        public Ebitda AverageDailyVolume10Day { get; set; }

        [JsonProperty("longName")]
        public string LongName { get; set; }

        [JsonProperty("regularMarketChange")]
        public CurrentPrice RegularMarketChange { get; set; }

        [JsonProperty("currencySymbol")]
        public string CurrencySymbol { get; set; }

        [JsonProperty("regularMarketPreviousClose")]
        public CurrentPrice RegularMarketPreviousClose { get; set; }

        [JsonProperty("postMarketTime")]
        public long PostMarketTime { get; set; }

        [JsonProperty("preMarketPrice")]
        public CurrentPrice PreMarketPrice { get; set; }

        [JsonProperty("preMarketTime")]
        public long PreMarketTime { get; set; }

        [JsonProperty("exchangeDataDelayedBy")]
        public long ExchangeDataDelayedBy { get; set; }

        [JsonProperty("toCurrency")]
        public dynamic ToCurrency { get; set; }

        [JsonProperty("postMarketChange")]
        public CurrentPrice PostMarketChange { get; set; }

        [JsonProperty("postMarketPrice")]
        public CurrentPrice PostMarketPrice { get; set; }

        [JsonProperty("exchangeName")]
        public string ExchangeName { get; set; }

        [JsonProperty("preMarketChange")]
        public CurrentPrice PreMarketChange { get; set; }

        [JsonProperty("circulatingSupply")]
        public PeRatio CirculatingSupply { get; set; }

        [JsonProperty("regularMarketDayLow")]
        public CurrentPrice RegularMarketDayLow { get; set; }

        [JsonProperty("priceHint")]
        public Ebitda PriceHint { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("regularMarketPrice")]
        public CurrentPrice RegularMarketPrice { get; set; }

        [JsonProperty("regularMarketVolume")]
        public Ebitda RegularMarketVolume { get; set; }

        [JsonProperty("lastMarket")]
        public dynamic LastMarket { get; set; }

        [JsonProperty("regularMarketSource")]
        public string RegularMarketSource { get; set; }

        [JsonProperty("openInterest")]
        public PeRatio OpenInterest { get; set; }

        [JsonProperty("marketState")]
        public string MarketState { get; set; }

        [JsonProperty("underlyingSymbol")]
        public dynamic UnderlyingSymbol { get; set; }

        [JsonProperty("marketCap")]
        public Ebitda MarketCap { get; set; }

        [JsonProperty("quoteType")]
        public string QuoteType { get; set; }

        [JsonProperty("preMarketChangePercent")]
        public CurrentPrice PreMarketChangePercent { get; set; }

        [JsonProperty("volumeAllCurrencies")]
        public PeRatio VolumeAllCurrencies { get; set; }

        [JsonProperty("postMarketSource")]
        public string PostMarketSource { get; set; }

        [JsonProperty("strikePrice")]
        public PeRatio StrikePrice { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("postMarketChangePercent")]
        public CurrentPrice PostMarketChangePercent { get; set; }

        [JsonProperty("preMarketSource")]
        public string PreMarketSource { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("fromCurrency")]
        public dynamic FromCurrency { get; set; }

        [JsonProperty("regularMarketChangePercent")]
        public CurrentPrice RegularMarketChangePercent { get; set; }
    }

    public partial class QuoteType
    {
        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("longName")]
        public string LongName { get; set; }

        [JsonProperty("exchangeTimezoneName")]
        public string ExchangeTimezoneName { get; set; }

        [JsonProperty("exchangeTimezoneShortName")]
        public string ExchangeTimezoneShortName { get; set; }

        [JsonProperty("isEsgPopulated")]
        public bool IsEsgPopulated { get; set; }

        [JsonProperty("gmtOffSetMilliseconds")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long GmtOffSetMilliseconds { get; set; }

        [JsonProperty("quoteType")]
        public string QuoteTypeQuoteType { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("messageBoardId")]
        public string MessageBoardId { get; set; }

        [JsonProperty("market")]
        public string Market { get; set; }
    }

    public partial class RecommendationTrend
    {
        [JsonProperty("trend")]
        public List<RecommendationTrendTrend> Trend { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }
    }

    public partial class RecommendationTrendTrend
    {
        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("strongBuy")]
        public long StrongBuy { get; set; }

        [JsonProperty("buy")]
        public long Buy { get; set; }

        [JsonProperty("hold")]
        public long Hold { get; set; }

        [JsonProperty("sell")]
        public long Sell { get; set; }

        [JsonProperty("strongSell")]
        public long StrongSell { get; set; }
    }

    public partial class SummaryDetail
    {
        [JsonProperty("previousClose")]
        public CurrentPrice PreviousClose { get; set; }

        [JsonProperty("regularMarketOpen")]
        public CurrentPrice RegularMarketOpen { get; set; }

        [JsonProperty("twoHundredDayAverage")]
        public CurrentPrice TwoHundredDayAverage { get; set; }

        [JsonProperty("trailingAnnualDividendYield")]
        public PeRatio TrailingAnnualDividendYield { get; set; }

        [JsonProperty("payoutRatio")]
        public CurrentPrice PayoutRatio { get; set; }

        [JsonProperty("volume24Hr")]
        public PeRatio Volume24Hr { get; set; }

        [JsonProperty("regularMarketDayHigh")]
        public CurrentPrice RegularMarketDayHigh { get; set; }

        [JsonProperty("navPrice")]
        public PeRatio NavPrice { get; set; }

        [JsonProperty("averageDailyVolume10Day")]
        public Ebitda AverageDailyVolume10Day { get; set; }

        [JsonProperty("totalAssets")]
        public PeRatio TotalAssets { get; set; }

        [JsonProperty("regularMarketPreviousClose")]
        public CurrentPrice RegularMarketPreviousClose { get; set; }

        [JsonProperty("fiftyDayAverage")]
        public CurrentPrice FiftyDayAverage { get; set; }

        [JsonProperty("trailingAnnualDividendRate")]
        public PeRatio TrailingAnnualDividendRate { get; set; }

        [JsonProperty("open")]
        public CurrentPrice Open { get; set; }

        [JsonProperty("toCurrency")]
        public dynamic ToCurrency { get; set; }

        [JsonProperty("averageVolume10days")]
        public Ebitda AverageVolume10Days { get; set; }

        [JsonProperty("expireDate")]
        public PeRatio ExpireDate { get; set; }

        [JsonProperty("yield")]
        public PeRatio Yield { get; set; }

        [JsonProperty("algorithm")]
        public dynamic Algorithm { get; set; }

        [JsonProperty("dividendRate")]
        public PeRatio DividendRate { get; set; }

        [JsonProperty("exDividendDate")]
        public PeRatio ExDividendDate { get; set; }

        [JsonProperty("beta")]
        public CurrentPrice Beta { get; set; }

        [JsonProperty("circulatingSupply")]
        public PeRatio CirculatingSupply { get; set; }

        [JsonProperty("startDate")]
        public PeRatio StartDate { get; set; }

        [JsonProperty("regularMarketDayLow")]
        public CurrentPrice RegularMarketDayLow { get; set; }

        [JsonProperty("priceHint")]
        public Ebitda PriceHint { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("regularMarketVolume")]
        public Ebitda RegularMarketVolume { get; set; }

        [JsonProperty("lastMarket")]
        public dynamic LastMarket { get; set; }

        [JsonProperty("maxSupply")]
        public PeRatio MaxSupply { get; set; }

        [JsonProperty("openInterest")]
        public PeRatio OpenInterest { get; set; }

        [JsonProperty("marketCap")]
        public Ebitda MarketCap { get; set; }

        [JsonProperty("volumeAllCurrencies")]
        public PeRatio VolumeAllCurrencies { get; set; }

        [JsonProperty("strikePrice")]
        public PeRatio StrikePrice { get; set; }

        [JsonProperty("averageVolume")]
        public Ebitda AverageVolume { get; set; }

        [JsonProperty("priceToSalesTrailing12Months")]
        public CurrentPrice PriceToSalesTrailing12Months { get; set; }

        [JsonProperty("dayLow")]
        public CurrentPrice DayLow { get; set; }

        [JsonProperty("ask")]
        public CurrentPrice Ask { get; set; }

        [JsonProperty("ytdReturn")]
        public PeRatio YtdReturn { get; set; }

        [JsonProperty("askSize")]
        public Ebitda AskSize { get; set; }

        [JsonProperty("volume")]
        public Ebitda Volume { get; set; }

        [JsonProperty("fiftyTwoWeekHigh")]
        public CurrentPrice FiftyTwoWeekHigh { get; set; }

        [JsonProperty("forwardPE")]
        public CurrentPrice ForwardPe { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("fromCurrency")]
        public dynamic FromCurrency { get; set; }

        [JsonProperty("fiveYearAvgDividendYield")]
        public PeRatio FiveYearAvgDividendYield { get; set; }

        [JsonProperty("fiftyTwoWeekLow")]
        public CurrentPrice FiftyTwoWeekLow { get; set; }

        [JsonProperty("bid")]
        public CurrentPrice Bid { get; set; }

        [JsonProperty("tradeable")]
        public bool Tradeable { get; set; }

        [JsonProperty("dividendYield")]
        public PeRatio DividendYield { get; set; }

        [JsonProperty("bidSize")]
        public Ebitda BidSize { get; set; }

        [JsonProperty("dayHigh")]
        public CurrentPrice DayHigh { get; set; }
    }

    public partial class UpgradeDowngradeHistory
    {
        [JsonProperty("history")]
        public List<UpgradeDowngradeHistoryHistory> History { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }
    }

    public partial class UpgradeDowngradeHistoryHistory
    {
        [JsonProperty("epochGradeDate")]
        public long EpochGradeDate { get; set; }

        [JsonProperty("firm")]
        public string Firm { get; set; }

        [JsonProperty("toGrade")]
        public string ToGrade { get; set; }

        [JsonProperty("fromGrade")]
        [JsonConverter(typeof(FromGradeConverter))]
        public dynamic FromGrade { get; set; }

        [JsonProperty("action")]
        [JsonConverter(typeof(ActionConverter))]
        public dynamic Action { get; set; }
    }

    public enum Action { Down, Init, Main, Up };

    public enum FromGrade { Buy, Empty, Neutral, Outperform, Overweight, Underperform };

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class ActionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Action) || t == typeof(Action?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "down":
                    return Action.Down;
                case "init":
                    return Action.Init;
                case "main":
                    return Action.Main;
                case "up":
                    return Action.Up;
            }
            throw new Exception("Cannot unmarshal type Action");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Action)untypedValue;
            switch (value)
            {
                case Action.Down:
                    serializer.Serialize(writer, "down");
                    return;
                case Action.Init:
                    serializer.Serialize(writer, "init");
                    return;
                case Action.Main:
                    serializer.Serialize(writer, "main");
                    return;
                case Action.Up:
                    serializer.Serialize(writer, "up");
                    return;
            }
            throw new Exception("Cannot marshal type Action");
        }

        public static readonly ActionConverter Singleton = new ActionConverter();
    }

    internal class FromGradeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(FromGrade) || t == typeof(FromGrade?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "":
                    return FromGrade.Empty;
                case "Buy":
                    return FromGrade.Buy;
                case "Neutral":
                    return FromGrade.Neutral;
                case "Outperform":
                    return FromGrade.Outperform;
                case "Overweight":
                    return FromGrade.Overweight;
                case "Underperform":
                    return FromGrade.Underperform;
            }
            throw new Exception("Cannot unmarshal type FromGrade");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (FromGrade)untypedValue;
            switch (value)
            {
                case FromGrade.Empty:
                    serializer.Serialize(writer, "");
                    return;
                case FromGrade.Buy:
                    serializer.Serialize(writer, "Buy");
                    return;
                case FromGrade.Neutral:
                    serializer.Serialize(writer, "Neutral");
                    return;
                case FromGrade.Outperform:
                    serializer.Serialize(writer, "Outperform");
                    return;
                case FromGrade.Overweight:
                    serializer.Serialize(writer, "Overweight");
                    return;
                case FromGrade.Underperform:
                    serializer.Serialize(writer, "Underperform");
                    return;
            }
            throw new Exception("Cannot marshal type FromGrade");
        }

        public static readonly FromGradeConverter Singleton = new FromGradeConverter();
    }
    /*
     {
      "recommendationTrend": {
        "trend": [
          {
            "period": "0m",
            "strongBuy": 2,
            "buy": 3,
            "hold": 0,
            "sell": 0,
            "strongSell": 0
          },
          {
            "period": "-1m",
            "strongBuy": 2,
            "buy": 6,
            "hold": 4,
            "sell": 0,
            "strongSell": 0
          },
          {
            "period": "-2m",
            "strongBuy": 2,
            "buy": 6,
            "hold": 5,
            "sell": 0,
            "strongSell": 0
          },
          {
            "period": "-3m",
            "strongBuy": 2,
            "buy": 3,
            "hold": 0,
            "sell": 0,
            "strongSell": 0
          }
        ],
        "maxAge": 86400
      },
      "financialsTemplate": {
        "code": "N",
        "maxAge": 1
      },
      "price": {
        "quoteSourceName": "Nasdaq Real Time Price",
        "regularMarketOpen": {
          "raw": 6.17,
          "fmt": "6.17"
        },
        "averageDailyVolume3Month": {
          "raw": 7947921,
          "fmt": "7.95M",
          "longFmt": "7,947,921"
        },
        "exchange": "NMS",
        "regularMarketTime": 1614718801,
        "volume24Hr": {},
        "regularMarketDayHigh": {
          "raw": 6.28,
          "fmt": "6.28"
        },
        "shortName": "Amarin Corporation plc",
        "averageDailyVolume10Day": {
          "raw": 7208950,
          "fmt": "7.21M",
          "longFmt": "7,208,950"
        },
        "longName": "Amarin Corporation plc",
        "regularMarketChange": {
          "raw": -0.15999985,
          "fmt": "-0.16"
        },
        "currencySymbol": "$",
        "regularMarketPreviousClose": {
          "raw": 6.19,
          "fmt": "6.19"
        },
        "postMarketTime": 1614723710,
        "preMarketPrice": {
          "raw": 6.15,
          "fmt": "6.15"
        },
        "preMarketTime": 1614695396,
        "exchangeDataDelayedBy": 0,
        "toCurrency": null,
        "postMarketChange": {
          "raw": 0.029999733,
          "fmt": "0.03"
        },
        "postMarketPrice": {
          "raw": 6.06,
          "fmt": "6.06"
        },
        "exchangeName": "NasdaqGS",
        "preMarketChange": {
          "raw": -0.04,
          "fmt": "-0.04"
        },
        "circulatingSupply": {},
        "regularMarketDayLow": {
          "raw": 5.94,
          "fmt": "5.94"
        },
        "priceHint": {
          "raw": 2,
          "fmt": "2",
          "longFmt": "2"
        },
        "currency": "USD",
        "regularMarketPrice": {
          "raw": 6.03,
          "fmt": "6.03"
        },
        "regularMarketVolume": {
          "raw": 11547574,
          "fmt": "11.55M",
          "longFmt": "11,547,574.00"
        },
        "lastMarket": null,
        "regularMarketSource": "FREE_REALTIME",
        "openInterest": {},
        "marketState": "POST",
        "underlyingSymbol": null,
        "marketCap": {
          "raw": 2373619200,
          "fmt": "2.37B",
          "longFmt": "2,373,619,200.00"
        },
        "quoteType": "EQUITY",
        "preMarketChangePercent": {
          "raw": -0.0064620296,
          "fmt": "-0.65%"
        },
        "volumeAllCurrencies": {},
        "postMarketSource": "FREE_REALTIME",
        "strikePrice": {},
        "symbol": "AMRN",
        "postMarketChangePercent": {
          "raw": 0.00497508,
          "fmt": "0.50%"
        },
        "preMarketSource": "FREE_REALTIME",
        "maxAge": 1,
        "fromCurrency": null,
        "regularMarketChangePercent": {
          "raw": -0.025848117,
          "fmt": "-2.58%"
        }
      },
      "earningsHistory": {
        "history": [
          {
            "maxAge": 1,
            "epsActual": {
              "raw": -0.06,
              "fmt": "-0.06"
            },
            "epsEstimate": {
              "raw": -0.07,
              "fmt": "-0.07"
            },
            "epsDifference": {
              "raw": 0.01,
              "fmt": "0.01"
            },
            "surprisePercent": {
              "raw": 0.143,
              "fmt": "14.30%"
            },
            "quarter": {
              "raw": 1585612800,
              "fmt": "2020-03-31"
            },
            "period": "-4q"
          },
          {
            "maxAge": 1,
            "epsActual": {
              "raw": 0.01,
              "fmt": "0.01"
            },
            "epsEstimate": {
              "raw": -0.07,
              "fmt": "-0.07"
            },
            "epsDifference": {
              "raw": 0.08,
              "fmt": "0.08"
            },
            "surprisePercent": {
              "raw": 1.143,
              "fmt": "114.30%"
            },
            "quarter": {
              "raw": 1593475200,
              "fmt": "2020-06-30"
            },
            "period": "-3q"
          },
          {
            "maxAge": 1,
            "epsActual": {
              "raw": -0.02,
              "fmt": "-0.02"
            },
            "epsEstimate": {
              "raw": 0.01,
              "fmt": "0.01"
            },
            "epsDifference": {
              "raw": -0.03,
              "fmt": "-0.03"
            },
            "surprisePercent": {
              "raw": -3,
              "fmt": "-300.00%"
            },
            "quarter": {
              "raw": 1601424000,
              "fmt": "2020-09-30"
            },
            "period": "-2q"
          },
          {
            "maxAge": 1,
            "epsActual": {
              "raw": 0.01,
              "fmt": "0.01"
            },
            "epsEstimate": {
              "raw": -0.01,
              "fmt": "-0.01"
            },
            "epsDifference": {
              "raw": 0.02,
              "fmt": "0.02"
            },
            "surprisePercent": {
              "raw": 2,
              "fmt": "200.00%"
            },
            "quarter": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "period": "-1q"
          }
        ],
        "maxAge": 86400
      },
      "indexTrend": {
        "maxAge": 1,
        "symbol": "SP5",
        "peRatio": {
          "raw": 14.552,
          "fmt": "14.55"
        },
        "pegRatio": {
          "raw": 1.25381,
          "fmt": "1.25"
        },
        "estimates": [
          {
            "period": "0q",
            "growth": {
              "raw": 0.34,
              "fmt": "0.34"
            }
          },
          {
            "period": "+1q",
            "growth": {
              "raw": 0.856,
              "fmt": "0.86"
            }
          },
          {
            "period": "0y",
            "growth": {
              "raw": 0.216,
              "fmt": "0.22"
            }
          },
          {
            "period": "+1y",
            "growth": {
              "raw": 0.141,
              "fmt": "0.14"
            }
          },
          {
            "period": "+5y",
            "growth": {
              "raw": 0.0888303,
              "fmt": "0.09"
            }
          },
          {
            "period": "-5y",
            "growth": {}
          }
        ]
      },
      "financialData": {
        "ebitdaMargins": {
          "raw": -0.02869,
          "fmt": "-2.87%"
        },
        "profitMargins": {
          "raw": -0.02931,
          "fmt": "-2.93%"
        },
        "grossMargins": {
          "raw": 0.78594,
          "fmt": "78.59%"
        },
        "operatingCashflow": {
          "raw": -21746000,
          "fmt": "-21.75M",
          "longFmt": "-21,746,000"
        },
        "revenueGrowth": {
          "raw": 0.167,
          "fmt": "16.70%"
        },
        "operatingMargins": {
          "raw": -0.03201,
          "fmt": "-3.20%"
        },
        "ebitda": {
          "raw": -17617000,
          "fmt": "-17.62M",
          "longFmt": "-17,617,000"
        },
        "targetLowPrice": {
          "raw": 6,
          "fmt": "6.00"
        },
        "recommendationKey": "buy",
        "grossProfits": {
          "raw": 482616000,
          "fmt": "482.62M",
          "longFmt": "482,616,000"
        },
        "freeCashflow": {
          "raw": -19711500,
          "fmt": "-19.71M",
          "longFmt": "-19,711,500"
        },
        "targetMedianPrice": {
          "raw": 10,
          "fmt": "10.00"
        },
        "currentPrice": {
          "raw": 6.03,
          "fmt": "6.03"
        },
        "earningsGrowth": {
          "raw": -0.424,
          "fmt": "-42.40%"
        },
        "currentRatio": {
          "raw": 2.86,
          "fmt": "2.86"
        },
        "returnOnAssets": {
          "raw": -0.01329,
          "fmt": "-1.33%"
        },
        "numberOfAnalystOpinions": {
          "raw": 11,
          "fmt": "11",
          "longFmt": "11"
        },
        "targetMeanPrice": {
          "raw": 10.64,
          "fmt": "10.64"
        },
        "debtToEquity": {
          "raw": 1.694,
          "fmt": "1.69"
        },
        "returnOnEquity": {
          "raw": -0.02913,
          "fmt": "-2.91%"
        },
        "targetHighPrice": {
          "raw": 19,
          "fmt": "19.00"
        },
        "totalCash": {
          "raw": 500932992,
          "fmt": "500.93M",
          "longFmt": "500,932,992"
        },
        "totalDebt": {
          "raw": 10628000,
          "fmt": "10.63M",
          "longFmt": "10,628,000"
        },
        "totalRevenue": {
          "raw": 614060032,
          "fmt": "614.06M",
          "longFmt": "614,060,032"
        },
        "totalCashPerShare": {
          "raw": 1.273,
          "fmt": "1.27"
        },
        "financialCurrency": "USD",
        "maxAge": 86400,
        "revenuePerShare": {
          "raw": 1.609,
          "fmt": "1.61"
        },
        "quickRatio": {
          "raw": 2.132,
          "fmt": "2.13"
        },
        "recommendationMean": {
          "raw": 2.2,
          "fmt": "2.20"
        }
      },
      "earningsTrend": {
        "trend": [
          {
            "maxAge": 1,
            "period": "0q",
            "endDate": "2021-03-31",
            "growth": {
              "raw": 0.16700001,
              "fmt": "16.70%"
            },
            "earningsEstimate": {
              "avg": {
                "raw": -0.05,
                "fmt": "-0.05"
              },
              "low": {
                "raw": -0.12,
                "fmt": "-0.12"
              },
              "high": {
                "raw": -0.01,
                "fmt": "-0.01"
              },
              "yearAgoEps": {
                "raw": -0.06,
                "fmt": "-0.06"
              },
              "numberOfAnalysts": {
                "raw": 8,
                "fmt": "8",
                "longFmt": "8"
              },
              "growth": {
                "raw": 0.16700001,
                "fmt": "16.70%"
              }
            },
            "revenueEstimate": {
              "avg": {
                "raw": 150950000,
                "fmt": "150.95M",
                "longFmt": "150,950,000"
              },
              "low": {
                "raw": 140000000,
                "fmt": "140M",
                "longFmt": "140,000,000"
              },
              "high": {
                "raw": 161250000,
                "fmt": "161.25M",
                "longFmt": "161,250,000"
              },
              "numberOfAnalysts": {
                "raw": 7,
                "fmt": "7",
                "longFmt": "7"
              },
              "yearAgoRevenue": {
                "raw": 137430000,
                "fmt": "137.43M",
                "longFmt": "137,430,000"
              },
              "growth": {
                "raw": 0.098000005,
                "fmt": "9.80%"
              }
            },
            "epsTrend": {
              "current": {
                "raw": -0.05,
                "fmt": "-0.05"
              },
              "7daysAgo": {
                "raw": -0.06,
                "fmt": "-0.06"
              },
              "30daysAgo": {
                "raw": -0.06,
                "fmt": "-0.06"
              },
              "60daysAgo": {
                "raw": -0.03,
                "fmt": "-0.03"
              },
              "90daysAgo": {
                "raw": -0.01,
                "fmt": "-0.01"
              }
            },
            "epsRevisions": {
              "upLast7days": {
                "raw": 3,
                "fmt": "3",
                "longFmt": "3"
              },
              "upLast30days": {
                "raw": 3,
                "fmt": "3",
                "longFmt": "3"
              },
              "downLast30days": {
                "raw": 3,
                "fmt": "3",
                "longFmt": "3"
              },
              "downLast90days": {}
            }
          },
          {
            "maxAge": 1,
            "period": "+1q",
            "endDate": "2021-06-30",
            "growth": {
              "raw": -6,
              "fmt": "-600.00%"
            },
            "earningsEstimate": {
              "avg": {
                "raw": -0.05,
                "fmt": "-0.05"
              },
              "low": {
                "raw": -0.12,
                "fmt": "-0.12"
              },
              "high": {
                "raw": 0,
                "fmt": "0"
              },
              "yearAgoEps": {
                "raw": 0.01,
                "fmt": "0.01"
              },
              "numberOfAnalysts": {
                "raw": 8,
                "fmt": "8",
                "longFmt": "8"
              },
              "growth": {
                "raw": -6,
                "fmt": "-600.00%"
              }
            },
            "revenueEstimate": {
              "avg": {
                "raw": 155140000,
                "fmt": "155.14M",
                "longFmt": "155,140,000"
              },
              "low": {
                "raw": 136000000,
                "fmt": "136M",
                "longFmt": "136,000,000"
              },
              "high": {
                "raw": 166250000,
                "fmt": "166.25M",
                "longFmt": "166,250,000"
              },
              "numberOfAnalysts": {
                "raw": 7,
                "fmt": "7",
                "longFmt": "7"
              },
              "yearAgoRevenue": {},
              "growth": {}
            },
            "epsTrend": {
              "current": {
                "raw": -0.05,
                "fmt": "-0.05"
              },
              "7daysAgo": {
                "raw": -0.06,
                "fmt": "-0.06"
              },
              "30daysAgo": {
                "raw": -0.04,
                "fmt": "-0.04"
              },
              "60daysAgo": {
                "raw": -0.01,
                "fmt": "-0.01"
              },
              "90daysAgo": {
                "raw": 0,
                "fmt": "0"
              }
            },
            "epsRevisions": {
              "upLast7days": {
                "raw": 3,
                "fmt": "3",
                "longFmt": "3"
              },
              "upLast30days": {
                "raw": 3,
                "fmt": "3",
                "longFmt": "3"
              },
              "downLast30days": {
                "raw": 2,
                "fmt": "2",
                "longFmt": "2"
              },
              "downLast90days": {}
            }
          },
          {
            "maxAge": 1,
            "period": "0y",
            "endDate": "2021-12-31",
            "growth": {
              "raw": -1.6,
              "fmt": "-160.00%"
            },
            "earningsEstimate": {
              "avg": {
                "raw": -0.13,
                "fmt": "-0.13"
              },
              "low": {
                "raw": -0.44,
                "fmt": "-0.44"
              },
              "high": {
                "raw": 0.15,
                "fmt": "0.15"
              },
              "yearAgoEps": {
                "raw": -0.05,
                "fmt": "-0.05"
              },
              "numberOfAnalysts": {
                "raw": 10,
                "fmt": "10",
                "longFmt": "10"
              },
              "growth": {
                "raw": -1.6,
                "fmt": "-160.00%"
              }
            },
            "revenueEstimate": {
              "avg": {
                "raw": 661320000,
                "fmt": "661.32M",
                "longFmt": "661,320,000"
              },
              "low": {
                "raw": 537500000,
                "fmt": "537.5M",
                "longFmt": "537,500,000"
              },
              "high": {
                "raw": 784100000,
                "fmt": "784.1M",
                "longFmt": "784,100,000"
              },
              "numberOfAnalysts": {
                "raw": 10,
                "fmt": "10",
                "longFmt": "10"
              },
              "yearAgoRevenue": {
                "raw": 614060000,
                "fmt": "614.06M",
                "longFmt": "614,060,000"
              },
              "growth": {
                "raw": 0.077,
                "fmt": "7.70%"
              }
            },
            "epsTrend": {
              "current": {
                "raw": -0.13,
                "fmt": "-0.13"
              },
              "7daysAgo": {
                "raw": -0.1,
                "fmt": "-0.1"
              },
              "30daysAgo": {
                "raw": -0.1,
                "fmt": "-0.1"
              },
              "60daysAgo": {
                "raw": 0.02,
                "fmt": "0.02"
              },
              "90daysAgo": {
                "raw": 0.17,
                "fmt": "0.17"
              }
            },
            "epsRevisions": {
              "upLast7days": {
                "raw": 4,
                "fmt": "4",
                "longFmt": "4"
              },
              "upLast30days": {
                "raw": 5,
                "fmt": "5",
                "longFmt": "5"
              },
              "downLast30days": {
                "raw": 2,
                "fmt": "2",
                "longFmt": "2"
              },
              "downLast90days": {}
            }
          },
          {
            "maxAge": 1,
            "period": "+1y",
            "endDate": "2022-12-31",
            "growth": {
              "raw": 1.8460001,
              "fmt": "184.60%"
            },
            "earningsEstimate": {
              "avg": {
                "raw": 0.11,
                "fmt": "0.11"
              },
              "low": {
                "raw": -0.56,
                "fmt": "-0.56"
              },
              "high": {
                "raw": 0.9,
                "fmt": "0.9"
              },
              "yearAgoEps": {
                "raw": -0.13,
                "fmt": "-0.13"
              },
              "numberOfAnalysts": {
                "raw": 11,
                "fmt": "11",
                "longFmt": "11"
              },
              "growth": {
                "raw": 1.8460001,
                "fmt": "184.60%"
              }
            },
            "revenueEstimate": {
              "avg": {
                "raw": 683710000,
                "fmt": "683.71M",
                "longFmt": "683,710,000"
              },
              "low": {
                "raw": 394100000,
                "fmt": "394.1M",
                "longFmt": "394,100,000"
              },
              "high": {
                "raw": 993210000,
                "fmt": "993.21M",
                "longFmt": "993,210,000"
              },
              "numberOfAnalysts": {
                "raw": 10,
                "fmt": "10",
                "longFmt": "10"
              },
              "yearAgoRevenue": {
                "raw": 661320000,
                "fmt": "661.32M",
                "longFmt": "661,320,000"
              },
              "growth": {
                "raw": 0.034,
                "fmt": "3.40%"
              }
            },
            "epsTrend": {
              "current": {
                "raw": 0.11,
                "fmt": "0.11"
              },
              "7daysAgo": {
                "raw": 0.11,
                "fmt": "0.11"
              },
              "30daysAgo": {
                "raw": 0.11,
                "fmt": "0.11"
              },
              "60daysAgo": {
                "raw": 0.25,
                "fmt": "0.25"
              },
              "90daysAgo": {
                "raw": 0.33,
                "fmt": "0.33"
              }
            },
            "epsRevisions": {
              "upLast7days": {
                "raw": 0,
                "fmt": null,
                "longFmt": "0"
              },
              "upLast30days": {
                "raw": 2,
                "fmt": "2",
                "longFmt": "2"
              },
              "downLast30days": {
                "raw": 1,
                "fmt": "1",
                "longFmt": "1"
              },
              "downLast90days": {}
            }
          },
          {
            "maxAge": 1,
            "period": "+5y",
            "endDate": null,
            "growth": {
              "raw": 0.38900003,
              "fmt": "38.90%"
            },
            "earningsEstimate": {
              "avg": {},
              "low": {},
              "high": {},
              "yearAgoEps": {},
              "numberOfAnalysts": {},
              "growth": {}
            },
            "revenueEstimate": {
              "avg": {},
              "low": {},
              "high": {},
              "numberOfAnalysts": {},
              "yearAgoRevenue": {},
              "growth": {}
            },
            "epsTrend": {
              "current": {},
              "7daysAgo": {},
              "30daysAgo": {},
              "60daysAgo": {},
              "90daysAgo": {}
            },
            "epsRevisions": {
              "upLast7days": {},
              "upLast30days": {},
              "downLast30days": {},
              "downLast90days": {}
            }
          },
          {
            "maxAge": 1,
            "period": "-5y",
            "endDate": null,
            "growth": {
              "raw": 0.65145,
              "fmt": "65.14%"
            },
            "earningsEstimate": {
              "avg": {},
              "low": {},
              "high": {},
              "yearAgoEps": {},
              "numberOfAnalysts": {},
              "growth": {}
            },
            "revenueEstimate": {
              "avg": {},
              "low": {},
              "high": {},
              "numberOfAnalysts": {},
              "yearAgoRevenue": {},
              "growth": {}
            },
            "epsTrend": {
              "current": {},
              "7daysAgo": {},
              "30daysAgo": {},
              "60daysAgo": {},
              "90daysAgo": {}
            },
            "epsRevisions": {
              "upLast7days": {},
              "upLast30days": {},
              "downLast30days": {},
              "downLast90days": {}
            }
          }
        ],
        "maxAge": 1
      },
      "quoteType": {
        "exchange": "NMS",
        "shortName": "Amarin Corporation plc",
        "longName": "Amarin Corporation plc",
        "exchangeTimezoneName": "America/New_York",
        "exchangeTimezoneShortName": "EST",
        "isEsgPopulated": false,
        "gmtOffSetMilliseconds": "-18000000",
        "quoteType": "EQUITY",
        "symbol": "AMRN",
        "messageBoardId": "finmb_407863",
        "market": "us_market"
      },
      "sectorTrend": {
        "maxAge": 1,
        "symbol": null,
        "peRatio": {},
        "pegRatio": {},
        "estimates": []
      },
      "summaryDetail": {
        "previousClose": {
          "raw": 6.19,
          "fmt": "6.19"
        },
        "regularMarketOpen": {
          "raw": 6.17,
          "fmt": "6.17"
        },
        "twoHundredDayAverage": {
          "raw": 5.570368,
          "fmt": "5.57"
        },
        "trailingAnnualDividendYield": {},
        "payoutRatio": {
          "raw": 0,
          "fmt": "0.00%"
        },
        "volume24Hr": {},
        "regularMarketDayHigh": {
          "raw": 6.28,
          "fmt": "6.28"
        },
        "navPrice": {},
        "averageDailyVolume10Day": {
          "raw": 7208950,
          "fmt": "7.21M",
          "longFmt": "7,208,950"
        },
        "totalAssets": {},
        "regularMarketPreviousClose": {
          "raw": 6.19,
          "fmt": "6.19"
        },
        "fiftyDayAverage": {
          "raw": 7.272647,
          "fmt": "7.27"
        },
        "trailingAnnualDividendRate": {},
        "open": {
          "raw": 6.17,
          "fmt": "6.17"
        },
        "toCurrency": null,
        "averageVolume10days": {
          "raw": 7208950,
          "fmt": "7.21M",
          "longFmt": "7,208,950"
        },
        "expireDate": {},
        "yield": {},
        "algorithm": null,
        "dividendRate": {},
        "exDividendDate": {},
        "beta": {
          "raw": 2.377801,
          "fmt": "2.38"
        },
        "circulatingSupply": {},
        "startDate": {},
        "regularMarketDayLow": {
          "raw": 5.94,
          "fmt": "5.94"
        },
        "priceHint": {
          "raw": 2,
          "fmt": "2",
          "longFmt": "2"
        },
        "currency": "USD",
        "regularMarketVolume": {
          "raw": 11547574,
          "fmt": "11.55M",
          "longFmt": "11,547,574"
        },
        "lastMarket": null,
        "maxSupply": {},
        "openInterest": {},
        "marketCap": {
          "raw": 2373619200,
          "fmt": "2.37B",
          "longFmt": "2,373,619,200"
        },
        "volumeAllCurrencies": {},
        "strikePrice": {},
        "averageVolume": {
          "raw": 7947921,
          "fmt": "7.95M",
          "longFmt": "7,947,921"
        },
        "priceToSalesTrailing12Months": {
          "raw": 3.8654513,
          "fmt": "3.87"
        },
        "dayLow": {
          "raw": 5.94,
          "fmt": "5.94"
        },
        "ask": {
          "raw": 6.05,
          "fmt": "6.05"
        },
        "ytdReturn": {},
        "askSize": {
          "raw": 4000,
          "fmt": "4k",
          "longFmt": "4,000"
        },
        "volume": {
          "raw": 11547574,
          "fmt": "11.55M",
          "longFmt": "11,547,574"
        },
        "fiftyTwoWeekHigh": {
          "raw": 16.47,
          "fmt": "16.47"
        },
        "forwardPE": {
          "raw": 54.818184,
          "fmt": "54.82"
        },
        "maxAge": 1,
        "fromCurrency": null,
        "fiveYearAvgDividendYield": {},
        "fiftyTwoWeekLow": {
          "raw": 3.36,
          "fmt": "3.36"
        },
        "bid": {
          "raw": 6.05,
          "fmt": "6.05"
        },
        "tradeable": false,
        "dividendYield": {},
        "bidSize": {
          "raw": 3100,
          "fmt": "3.1k",
          "longFmt": "3,100"
        },
        "dayHigh": {
          "raw": 6.28,
          "fmt": "6.28"
        }
      },
      "symbol": "AMRN",
      "upgradeDowngradeHistory": {
        "history": [
          {
            "epochGradeDate": 1612177851,
            "firm": "SVB Leerink",
            "toGrade": "Outperform",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1601287572,
            "firm": "SVB Leerink",
            "toGrade": "Outperform",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1598437603,
            "firm": "Piper Sandler",
            "toGrade": "Overweight",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1588598646,
            "firm": "Aegis Capital",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1588587999,
            "firm": "SVB Leerink",
            "toGrade": "Outperform",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1585817240,
            "firm": "Citigroup",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1585676067,
            "firm": "Stifel",
            "toGrade": "Hold",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1585659104,
            "firm": "Oppenheimer",
            "toGrade": "Perform",
            "fromGrade": "Underperform",
            "action": "up"
          },
          {
            "epochGradeDate": 1585654209,
            "firm": "Goldman Sachs",
            "toGrade": "Neutral",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1585646345,
            "firm": "Jefferies",
            "toGrade": "Hold",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1584092487,
            "firm": "Goldman Sachs",
            "toGrade": "Buy",
            "fromGrade": "Neutral",
            "action": "up"
          },
          {
            "epochGradeDate": 1583163508,
            "firm": "Cowen & Co.",
            "toGrade": "Outperform",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1582722640,
            "firm": "Oppenheimer",
            "toGrade": "Underperform",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1582022781,
            "firm": "Citigroup",
            "toGrade": "Buy",
            "fromGrade": "Neutral",
            "action": "up"
          },
          {
            "epochGradeDate": 1578311412,
            "firm": "JP Morgan",
            "toGrade": "Neutral",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1576523549,
            "firm": "Oppenheimer",
            "toGrade": "Underperform",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1576508203,
            "firm": "Stifel",
            "toGrade": "Hold",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1574249344,
            "firm": "Oppenheimer",
            "toGrade": "Underperform",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1574080080,
            "firm": "Citi",
            "toGrade": "Neutral",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1572529285,
            "firm": "Aegis Capital",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1571135970,
            "firm": "Goldman Sachs",
            "toGrade": "Neutral",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1553251840,
            "firm": "Stifel Nicolaus",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1541164830,
            "firm": "Citigroup",
            "toGrade": "Neutral",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1539777585,
            "firm": "Citigroup",
            "toGrade": "Buy",
            "fromGrade": "Buy",
            "action": "main"
          },
          {
            "epochGradeDate": 1537881077,
            "firm": "Jefferies",
            "toGrade": "Buy",
            "fromGrade": "Buy",
            "action": "main"
          },
          {
            "epochGradeDate": 1537874265,
            "firm": "Citigroup",
            "toGrade": "Buy",
            "fromGrade": "Buy",
            "action": "main"
          },
          {
            "epochGradeDate": 1476872074,
            "firm": "Citigroup",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1475656296,
            "firm": "Cantor Fitzgerald",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1475615452,
            "firm": "Cantor Fitzgerald",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1463037254,
            "firm": "Jefferies",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1426145107,
            "firm": "H.C. Wainwright",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "up"
          },
          {
            "epochGradeDate": 1424252755,
            "firm": "SunTrust Robinson Humphrey",
            "toGrade": "Buy",
            "fromGrade": "Neutral",
            "action": "up"
          },
          {
            "epochGradeDate": 1415610000,
            "firm": "Citigroup",
            "toGrade": "Neutral",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1399879752,
            "firm": "Citigroup",
            "toGrade": "Neutral",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1393584370,
            "firm": "Aegis Capital",
            "toGrade": "Hold",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1389888000,
            "firm": "MKM Partners",
            "toGrade": "Neutral",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1383893330,
            "firm": "Citigroup",
            "toGrade": "Buy",
            "fromGrade": "Neutral",
            "action": "up"
          },
          {
            "epochGradeDate": 1383148800,
            "firm": "Leerink Swann",
            "toGrade": "Market Perform",
            "fromGrade": "Outperform",
            "action": "down"
          },
          {
            "epochGradeDate": 1383130853,
            "firm": "Aegis Capital",
            "toGrade": "Hold",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1382080843,
            "firm": "Citigroup",
            "toGrade": "Neutral",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1382025600,
            "firm": "H.C. Wainwright",
            "toGrade": "Neutral",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1381991178,
            "firm": "JP Morgan",
            "toGrade": "Neutral",
            "fromGrade": "Overweight",
            "action": "down"
          },
          {
            "epochGradeDate": 1381990292,
            "firm": "Canaccord Genuity",
            "toGrade": "Hold",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1381943909,
            "firm": "Leerink Swann",
            "toGrade": "Outperform",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1381939883,
            "firm": "Aegis Capital",
            "toGrade": "Hold",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1379401701,
            "firm": "Goldman Sachs",
            "toGrade": "Neutral",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1378280947,
            "firm": "SunTrust Robinson Humphrey",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1376378215,
            "firm": "H.C. Wainwright",
            "toGrade": "Buy",
            "fromGrade": "Neutral",
            "action": "up"
          },
          {
            "epochGradeDate": 1371744899,
            "firm": "Oppenheimer",
            "toGrade": "Perform",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1362380154,
            "firm": "Citigroup",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1357633828,
            "firm": "Jefferies",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1354895044,
            "firm": "Aegis Capital",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1354877921,
            "firm": "Canaccord Genuity",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1354876024,
            "firm": "UBS",
            "toGrade": "Neutral",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1353999120,
            "firm": "Citigroup",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1350293700,
            "firm": "JP Morgan",
            "toGrade": "Overweight",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1349248560,
            "firm": "Wedbush",
            "toGrade": "Neutral",
            "fromGrade": "Outperform",
            "action": "down"
          },
          {
            "epochGradeDate": 1344507000,
            "firm": "Canaccord Genuity",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1341812820,
            "firm": "Aegis Capital",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1340780880,
            "firm": "Jefferies",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "main"
          }
        ],
        "maxAge": 86400
      },
      "pageViews": {
        "shortTermTrend": "UP",
        "midTermTrend": "UP",
        "longTermTrend": "UP",
        "maxAge": 1
      },
      "industryTrend": {
        "maxAge": 1,
        "symbol": null,
        "peRatio": {},
        "pegRatio": {},
        "estimates": []
      }
    }
     */
    #endregion

    #region Stock Summary JSON Object
    public partial class StockSummary
    {
        [JsonProperty("defaultKeyStatistics")]
        public DefaultKeyStatistics DefaultKeyStatistics { get; set; }

        [JsonProperty("details")]
        public Details Details { get; set; }

        [JsonProperty("summaryProfile")]
        public SummaryProfile SummaryProfile { get; set; }

        [JsonProperty("recommendationTrend")]
        public RecommendationTrendSummary RecommendationTrend { get; set; }

        [JsonProperty("financialsTemplate")]
        public FinancialsTemplate FinancialsTemplate { get; set; }

        [JsonProperty("majorDirectHolders")]
        public Holders MajorDirectHolders { get; set; }

        [JsonProperty("earnings")]
        public StockSummaryEarnings Earnings { get; set; }

        [JsonProperty("price")]
        public PriceSummary Price { get; set; }

        [JsonProperty("fundOwnership")]
        public Ownership FundOwnership { get; set; }

        [JsonProperty("insiderTransactions")]
        public InsiderTransactions InsiderTransactions { get; set; }

        [JsonProperty("insiderHolders")]
        public Holders InsiderHolders { get; set; }

        [JsonProperty("netSharePurchaseActivity")]
        public NetSharePurchaseActivity NetSharePurchaseActivity { get; set; }

        [JsonProperty("majorHoldersBreakdown")]
        public MajorHoldersBreakdown MajorHoldersBreakdown { get; set; }

        [JsonProperty("financialData")]
        public FinancialDataSummary FinancialData { get; set; }

        [JsonProperty("quoteType")]
        public QuoteTypeSummary QuoteType { get; set; }

        [JsonProperty("institutionOwnership")]
        public Ownership InstitutionOwnership { get; set; }

        [JsonProperty("calendarEvents")]
        public CalendarEvents CalendarEvents { get; set; }

        [JsonProperty("summaryDetail")]
        public SummaryDetails SummaryDetail { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("esgScores")]
        public Details EsgScores { get; set; }

        [JsonProperty("upgradeDowngradeHistory")]
        public UpgradeDowngradeHistorySummary UpgradeDowngradeHistory { get; set; }

        [JsonProperty("pageViews")]
        public PageViews PageViews { get; set; }
    }

    public partial class CalendarEvents
    {
        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("earnings")]
        public CalendarEventsEarnings Earnings { get; set; }

        [JsonProperty("exDividendDate")]
        public Details ExDividendDate { get; set; }

        [JsonProperty("dividendDate")]
        public Details DividendDate { get; set; }
    }

    public partial class Details
    {
    }

    public partial class CalendarEventsEarnings
    {
        [JsonProperty("earningsDate")]
        public List<The52_WeekChange> EarningsDate { get; set; }

        [JsonProperty("earningsAverage")]
        public The52_WeekChange EarningsAverage { get; set; }

        [JsonProperty("earningsLow")]
        public The52_WeekChange EarningsLow { get; set; }

        [JsonProperty("earningsHigh")]
        public The52_WeekChange EarningsHigh { get; set; }

        [JsonProperty("revenueAverage")]
        public EnterpriseValue RevenueAverage { get; set; }

        [JsonProperty("revenueLow")]
        public EnterpriseValue RevenueLow { get; set; }

        [JsonProperty("revenueHigh")]
        public EnterpriseValue RevenueHigh { get; set; }
    }

    public partial class The52_WeekChange
    {
        [JsonProperty("raw")]
        public double Raw { get; set; }

        [JsonProperty("fmt")]
        public string Fmt { get; set; }
    }

    public partial class EnterpriseValue
    {
        [JsonProperty("raw")]
        public long Raw { get; set; }

        [JsonProperty("fmt")]
        public string Fmt { get; set; }

        [JsonProperty("longFmt")]
        public string LongFmt { get; set; }
    }

    public partial class DefaultKeyStatistics
    {
        [JsonProperty("annualHoldingsTurnover")]
        public Details AnnualHoldingsTurnover { get; set; }

        [JsonProperty("enterpriseToRevenue")]
        public The52_WeekChange EnterpriseToRevenue { get; set; }

        [JsonProperty("beta3Year")]
        public Details Beta3Year { get; set; }

        [JsonProperty("profitMargins")]
        public The52_WeekChange ProfitMargins { get; set; }

        [JsonProperty("enterpriseToEbitda")]
        public The52_WeekChange EnterpriseToEbitda { get; set; }

        [JsonProperty("52WeekChange")]
        public The52_WeekChange The52WeekChange { get; set; }

        [JsonProperty("morningStarRiskRating")]
        public Details MorningStarRiskRating { get; set; }

        [JsonProperty("forwardEps")]
        public The52_WeekChange ForwardEps { get; set; }

        [JsonProperty("revenueQuarterlyGrowth")]
        public Details RevenueQuarterlyGrowth { get; set; }

        [JsonProperty("sharesOutstanding")]
        public EnterpriseValue SharesOutstanding { get; set; }

        [JsonProperty("fundInceptionDate")]
        public Details FundInceptionDate { get; set; }

        [JsonProperty("annualReportExpenseRatio")]
        public Details AnnualReportExpenseRatio { get; set; }

        [JsonProperty("totalAssets")]
        public Details TotalAssets { get; set; }

        [JsonProperty("bookValue")]
        public The52_WeekChange BookValue { get; set; }

        [JsonProperty("sharesShort")]
        public EnterpriseValue SharesShort { get; set; }

        [JsonProperty("sharesPercentSharesOut")]
        public The52_WeekChange SharesPercentSharesOut { get; set; }

        [JsonProperty("fundFamily")]
        public dynamic FundFamily { get; set; }

        [JsonProperty("lastFiscalYearEnd")]
        public The52_WeekChange LastFiscalYearEnd { get; set; }

        [JsonProperty("heldPercentInstitutions")]
        public The52_WeekChange HeldPercentInstitutions { get; set; }

        [JsonProperty("netIncomeToCommon")]
        public EnterpriseValue NetIncomeToCommon { get; set; }

        [JsonProperty("trailingEps")]
        public The52_WeekChange TrailingEps { get; set; }

        [JsonProperty("lastDividendValue")]
        public Details LastDividendValue { get; set; }

        [JsonProperty("SandP52WeekChange")]
        public The52_WeekChange SandP52WeekChange { get; set; }

        [JsonProperty("priceToBook")]
        public The52_WeekChange PriceToBook { get; set; }

        [JsonProperty("heldPercentInsiders")]
        public The52_WeekChange HeldPercentInsiders { get; set; }

        [JsonProperty("nextFiscalYearEnd")]
        public The52_WeekChange NextFiscalYearEnd { get; set; }

        [JsonProperty("yield")]
        public Details Yield { get; set; }

        [JsonProperty("mostRecentQuarter")]
        public The52_WeekChange MostRecentQuarter { get; set; }

        [JsonProperty("shortRatio")]
        public The52_WeekChange ShortRatio { get; set; }

        [JsonProperty("sharesShortPreviousMonthDate")]
        public The52_WeekChange SharesShortPreviousMonthDate { get; set; }

        [JsonProperty("floatShares")]
        public EnterpriseValue FloatShares { get; set; }

        [JsonProperty("beta")]
        public The52_WeekChange Beta { get; set; }

        [JsonProperty("enterpriseValue")]
        public EnterpriseValue EnterpriseValue { get; set; }

        [JsonProperty("priceHint")]
        public EnterpriseValue PriceHint { get; set; }

        [JsonProperty("threeYearAverageReturn")]
        public Details ThreeYearAverageReturn { get; set; }

        [JsonProperty("lastSplitDate")]
        public The52_WeekChange LastSplitDate { get; set; }

        [JsonProperty("lastSplitFactor")]
        public string LastSplitFactor { get; set; }

        [JsonProperty("legalType")]
        public dynamic LegalType { get; set; }

        [JsonProperty("lastDividendDate")]
        public Details LastDividendDate { get; set; }

        [JsonProperty("morningStarOverallRating")]
        public Details MorningStarOverallRating { get; set; }

        [JsonProperty("earningsQuarterlyGrowth")]
        public The52_WeekChange EarningsQuarterlyGrowth { get; set; }

        [JsonProperty("priceToSalesTrailing12Months")]
        public Details PriceToSalesTrailing12Months { get; set; }

        [JsonProperty("dateShortInterest")]
        public The52_WeekChange DateShortInterest { get; set; }

        [JsonProperty("pegRatio")]
        public The52_WeekChange PegRatio { get; set; }

        [JsonProperty("ytdReturn")]
        public Details YtdReturn { get; set; }

        [JsonProperty("forwardPE")]
        public The52_WeekChange ForwardPe { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("lastCapGain")]
        public Details LastCapGain { get; set; }

        [JsonProperty("shortPercentOfFloat")]
        public Details ShortPercentOfFloat { get; set; }

        [JsonProperty("sharesShortPriorMonth")]
        public EnterpriseValue SharesShortPriorMonth { get; set; }

        [JsonProperty("impliedSharesOutstanding")]
        public Details ImpliedSharesOutstanding { get; set; }

        [JsonProperty("category")]
        public dynamic Category { get; set; }

        [JsonProperty("fiveYearAverageReturn")]
        public Details FiveYearAverageReturn { get; set; }
    }

    public partial class StockSummaryEarnings
    {
        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("earningsChart")]
        public EarningsChart EarningsChart { get; set; }

        [JsonProperty("financialsChart")]
        public FinancialsChart FinancialsChart { get; set; }

        [JsonProperty("financialCurrency")]
        public string FinancialCurrency { get; set; }
    }

    public partial class EarningsChart
    {
        [JsonProperty("quarterly")]
        public List<EarningsChartQuarterly> Quarterly { get; set; }

        [JsonProperty("currentQuarterEstimate")]
        public The52_WeekChange CurrentQuarterEstimate { get; set; }

        [JsonProperty("currentQuarterEstimateDate")]
        public string CurrentQuarterEstimateDate { get; set; }

        [JsonProperty("currentQuarterEstimateYear")]
        public long CurrentQuarterEstimateYear { get; set; }

        [JsonProperty("earningsDate")]
        public List<The52_WeekChange> EarningsDate { get; set; }
    }

    public partial class EarningsChartQuarterly
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("actual")]
        public The52_WeekChange Actual { get; set; }

        [JsonProperty("estimate")]
        public The52_WeekChange Estimate { get; set; }
    }

    public partial class FinancialsChart
    {
        [JsonProperty("yearly")]
        public List<Yearly> Yearly { get; set; }

        [JsonProperty("quarterly")]
        public List<FinancialsChartQuarterly> Quarterly { get; set; }
    }

    public partial class FinancialsChartQuarterly
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("revenue")]
        public EnterpriseValue Revenue { get; set; }

        [JsonProperty("earnings")]
        public EnterpriseValue Earnings { get; set; }
    }

    public partial class Yearly
    {
        [JsonProperty("date")]
        public long Date { get; set; }

        [JsonProperty("revenue")]
        public EnterpriseValue Revenue { get; set; }

        [JsonProperty("earnings")]
        public EnterpriseValue Earnings { get; set; }
    }

    public partial class FinancialDataSummary
    {
        [JsonProperty("ebitdaMargins")]
        public The52_WeekChange EbitdaMargins { get; set; }

        [JsonProperty("profitMargins")]
        public The52_WeekChange ProfitMargins { get; set; }

        [JsonProperty("grossMargins")]
        public The52_WeekChange GrossMargins { get; set; }

        [JsonProperty("operatingCashflow")]
        public EnterpriseValue OperatingCashflow { get; set; }

        [JsonProperty("revenueGrowth")]
        public The52_WeekChange RevenueGrowth { get; set; }

        [JsonProperty("operatingMargins")]
        public The52_WeekChange OperatingMargins { get; set; }

        [JsonProperty("ebitda")]
        public EnterpriseValue Ebitda { get; set; }

        [JsonProperty("targetLowPrice")]
        public The52_WeekChange TargetLowPrice { get; set; }

        [JsonProperty("recommendationKey")]
        public string RecommendationKey { get; set; }

        [JsonProperty("grossProfits")]
        public EnterpriseValue GrossProfits { get; set; }

        [JsonProperty("freeCashflow")]
        public EnterpriseValue FreeCashflow { get; set; }

        [JsonProperty("targetMedianPrice")]
        public The52_WeekChange TargetMedianPrice { get; set; }

        [JsonProperty("currentPrice")]
        public The52_WeekChange CurrentPrice { get; set; }

        [JsonProperty("earningsGrowth")]
        public The52_WeekChange EarningsGrowth { get; set; }

        [JsonProperty("currentRatio")]
        public The52_WeekChange CurrentRatio { get; set; }

        [JsonProperty("returnOnAssets")]
        public The52_WeekChange ReturnOnAssets { get; set; }

        [JsonProperty("numberOfAnalystOpinions")]
        public EnterpriseValue NumberOfAnalystOpinions { get; set; }

        [JsonProperty("targetMeanPrice")]
        public The52_WeekChange TargetMeanPrice { get; set; }

        [JsonProperty("debtToEquity")]
        public The52_WeekChange DebtToEquity { get; set; }

        [JsonProperty("returnOnEquity")]
        public The52_WeekChange ReturnOnEquity { get; set; }

        [JsonProperty("targetHighPrice")]
        public The52_WeekChange TargetHighPrice { get; set; }

        [JsonProperty("totalCash")]
        public EnterpriseValue TotalCash { get; set; }

        [JsonProperty("totalDebt")]
        public EnterpriseValue TotalDebt { get; set; }

        [JsonProperty("totalRevenue")]
        public EnterpriseValue TotalRevenue { get; set; }

        [JsonProperty("totalCashPerShare")]
        public The52_WeekChange TotalCashPerShare { get; set; }

        [JsonProperty("financialCurrency")]
        public string FinancialCurrency { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("revenuePerShare")]
        public The52_WeekChange RevenuePerShare { get; set; }

        [JsonProperty("quickRatio")]
        public The52_WeekChange QuickRatio { get; set; }

        [JsonProperty("recommendationMean")]
        public The52_WeekChange RecommendationMean { get; set; }
    }

    public partial class FinancialsTemplate
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }
    }

    public partial class Ownership
    {
        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("ownershipList")]
        public List<OwnershipList> OwnershipList { get; set; }
    }

    public partial class OwnershipList
    {
        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("reportDate")]
        public The52_WeekChange ReportDate { get; set; }

        [JsonProperty("organization")]
        public string Organization { get; set; }

        [JsonProperty("pctHeld")]
        public The52_WeekChange PctHeld { get; set; }

        [JsonProperty("position")]
        public EnterpriseValue Position { get; set; }

        [JsonProperty("value")]
        public EnterpriseValue Value { get; set; }
    }

    public partial class Holders
    {
        [JsonProperty("holders")]
        public List<Holder> HoldersHolders { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }
    }

    public partial class Holder
    {
        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("name")]
        [JsonConverter(typeof(NameConverter))]
        public Name Name { get; set; }

        [JsonProperty("relation")]
        [JsonConverter(typeof(RelationConverter))]
        public Relation Relation { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("transactionDescription")]
        [JsonConverter(typeof(TransactionDescriptionConverter))]
        public dynamic TransactionDescription { get; set; }

        [JsonProperty("latestTransDate")]
        public The52_WeekChange LatestTransDate { get; set; }

        [JsonProperty("positionDirect", NullValueHandling = NullValueHandling.Ignore)]
        public EnterpriseValue PositionDirect { get; set; }

        [JsonProperty("positionDirectDate", NullValueHandling = NullValueHandling.Ignore)]
        public The52_WeekChange PositionDirectDate { get; set; }

        [JsonProperty("positionIndirectDate", NullValueHandling = NullValueHandling.Ignore)]
        public The52_WeekChange PositionIndirectDate { get; set; }
    }

    public partial class InsiderTransactions
    {
        [JsonProperty("transactions")]
        public List<Transaction> Transactions { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }
    }

    public partial class Transaction
    {
        [JsonProperty("filerName")]
        [JsonConverter(typeof(NameConverter))]
        public dynamic FilerName { get; set; }

        [JsonProperty("transactionText")]
        public string TransactionText { get; set; }

        [JsonProperty("moneyText")]
        public string MoneyText { get; set; }

        [JsonProperty("ownership")]
        [JsonConverter(typeof(OwnershipEnumConverter))]
        public dynamic Ownership { get; set; }

        [JsonProperty("startDate")]
        public The52_WeekChange StartDate { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public EnterpriseValue Value { get; set; }

        [JsonProperty("filerRelation")]
        [JsonConverter(typeof(RelationConverter))]
        public dynamic FilerRelation { get; set; }

        [JsonProperty("shares")]
        public EnterpriseValue Shares { get; set; }

        [JsonProperty("filerUrl")]
        public string FilerUrl { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }
    }

    public partial class MajorHoldersBreakdown
    {
        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("insidersPercentHeld")]
        public The52_WeekChange InsidersPercentHeld { get; set; }

        [JsonProperty("institutionsPercentHeld")]
        public The52_WeekChange InstitutionsPercentHeld { get; set; }

        [JsonProperty("institutionsFloatPercentHeld")]
        public The52_WeekChange InstitutionsFloatPercentHeld { get; set; }

        [JsonProperty("institutionsCount")]
        public EnterpriseValue InstitutionsCount { get; set; }
    }

    public partial class NetSharePurchaseActivity
    {
        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("netPercentInsiderShares")]
        public The52_WeekChange NetPercentInsiderShares { get; set; }

        [JsonProperty("netInfoCount")]
        public EnterpriseValue NetInfoCount { get; set; }

        [JsonProperty("totalInsiderShares")]
        public EnterpriseValue TotalInsiderShares { get; set; }

        [JsonProperty("buyInfoShares")]
        public EnterpriseValue BuyInfoShares { get; set; }

        [JsonProperty("buyPercentInsiderShares")]
        public The52_WeekChange BuyPercentInsiderShares { get; set; }

        [JsonProperty("sellInfoCount")]
        public EnterpriseValue SellInfoCount { get; set; }

        [JsonProperty("sellInfoShares")]
        public EnterpriseValue SellInfoShares { get; set; }

        [JsonProperty("sellPercentInsiderShares")]
        public The52_WeekChange SellPercentInsiderShares { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("buyInfoCount")]
        public EnterpriseValue BuyInfoCount { get; set; }

        [JsonProperty("netInfoShares")]
        public EnterpriseValue NetInfoShares { get; set; }
    }

    public partial class PriceSummary
    {
        [JsonProperty("quoteSourceName")]
        public string QuoteSourceName { get; set; }

        [JsonProperty("regularMarketOpen")]
        public The52_WeekChange RegularMarketOpen { get; set; }

        [JsonProperty("averageDailyVolume3Month")]
        public EnterpriseValue AverageDailyVolume3Month { get; set; }

        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("regularMarketTime")]
        public long RegularMarketTime { get; set; }

        [JsonProperty("volume24Hr")]
        public Details Volume24Hr { get; set; }

        [JsonProperty("regularMarketDayHigh")]
        public The52_WeekChange RegularMarketDayHigh { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("averageDailyVolume10Day")]
        public EnterpriseValue AverageDailyVolume10Day { get; set; }

        [JsonProperty("longName")]
        public string LongName { get; set; }

        [JsonProperty("regularMarketChange")]
        public The52_WeekChange RegularMarketChange { get; set; }

        [JsonProperty("currencySymbol")]
        public string CurrencySymbol { get; set; }

        [JsonProperty("regularMarketPreviousClose")]
        public The52_WeekChange RegularMarketPreviousClose { get; set; }

        [JsonProperty("postMarketTime")]
        public long PostMarketTime { get; set; }

        [JsonProperty("preMarketPrice")]
        public The52_WeekChange PreMarketPrice { get; set; }

        [JsonProperty("preMarketTime")]
        public long PreMarketTime { get; set; }

        [JsonProperty("exchangeDataDelayedBy")]
        public long ExchangeDataDelayedBy { get; set; }

        [JsonProperty("toCurrency")]
        public dynamic ToCurrency { get; set; }

        [JsonProperty("postMarketChange")]
        public The52_WeekChange PostMarketChange { get; set; }

        [JsonProperty("postMarketPrice")]
        public The52_WeekChange PostMarketPrice { get; set; }

        [JsonProperty("exchangeName")]
        public string ExchangeName { get; set; }

        [JsonProperty("preMarketChange")]
        public The52_WeekChange PreMarketChange { get; set; }

        [JsonProperty("circulatingSupply")]
        public Details CirculatingSupply { get; set; }

        [JsonProperty("regularMarketDayLow")]
        public The52_WeekChange RegularMarketDayLow { get; set; }

        [JsonProperty("priceHint")]
        public EnterpriseValue PriceHint { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("regularMarketPrice")]
        public The52_WeekChange RegularMarketPrice { get; set; }

        [JsonProperty("regularMarketVolume")]
        public EnterpriseValue RegularMarketVolume { get; set; }

        [JsonProperty("lastMarket")]
        public dynamic LastMarket { get; set; }

        [JsonProperty("regularMarketSource")]
        public string RegularMarketSource { get; set; }

        [JsonProperty("openInterest")]
        public Details OpenInterest { get; set; }

        [JsonProperty("marketState")]
        public string MarketState { get; set; }

        [JsonProperty("underlyingSymbol")]
        public dynamic UnderlyingSymbol { get; set; }

        [JsonProperty("marketCap")]
        public EnterpriseValue MarketCap { get; set; }

        [JsonProperty("quoteType")]
        public string QuoteType { get; set; }

        [JsonProperty("preMarketChangePercent")]
        public The52_WeekChange PreMarketChangePercent { get; set; }

        [JsonProperty("volumeAllCurrencies")]
        public Details VolumeAllCurrencies { get; set; }

        [JsonProperty("postMarketSource")]
        public string PostMarketSource { get; set; }

        [JsonProperty("strikePrice")]
        public Details StrikePrice { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("postMarketChangePercent")]
        public The52_WeekChange PostMarketChangePercent { get; set; }

        [JsonProperty("preMarketSource")]
        public string PreMarketSource { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("fromCurrency")]
        public dynamic FromCurrency { get; set; }

        [JsonProperty("regularMarketChangePercent")]
        public The52_WeekChange RegularMarketChangePercent { get; set; }
    }

    public partial class QuoteTypeSummary
    {
        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("longName")]
        public string LongName { get; set; }

        [JsonProperty("exchangeTimezoneName")]
        public string ExchangeTimezoneName { get; set; }

        [JsonProperty("exchangeTimezoneShortName")]
        public string ExchangeTimezoneShortName { get; set; }

        [JsonProperty("isEsgPopulated")]
        public bool IsEsgPopulated { get; set; }

        [JsonProperty("gmtOffSetMilliseconds")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long GmtOffSetMilliseconds { get; set; }

        [JsonProperty("quoteType")]
        public string QuoteTypeQuoteType { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("messageBoardId")]
        public string MessageBoardId { get; set; }

        [JsonProperty("market")]
        public string Market { get; set; }
    }

    public partial class RecommendationTrendSummary
    {
        [JsonProperty("trend")]
        public List<Trend> Trend { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }
    }

    public partial class Trend
    {
        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("strongBuy")]
        public long StrongBuy { get; set; }

        [JsonProperty("buy")]
        public long Buy { get; set; }

        [JsonProperty("hold")]
        public long Hold { get; set; }

        [JsonProperty("sell")]
        public long Sell { get; set; }

        [JsonProperty("strongSell")]
        public long StrongSell { get; set; }
    }

    public partial class SummaryDetails
    {
        [JsonProperty("previousClose")]
        public The52_WeekChange PreviousClose { get; set; }

        [JsonProperty("regularMarketOpen")]
        public The52_WeekChange RegularMarketOpen { get; set; }

        [JsonProperty("twoHundredDayAverage")]
        public The52_WeekChange TwoHundredDayAverage { get; set; }

        [JsonProperty("trailingAnnualDividendYield")]
        public Details TrailingAnnualDividendYield { get; set; }

        [JsonProperty("payoutRatio")]
        public The52_WeekChange PayoutRatio { get; set; }

        [JsonProperty("volume24Hr")]
        public Details Volume24Hr { get; set; }

        [JsonProperty("regularMarketDayHigh")]
        public The52_WeekChange RegularMarketDayHigh { get; set; }

        [JsonProperty("navPrice")]
        public Details NavPrice { get; set; }

        [JsonProperty("averageDailyVolume10Day")]
        public EnterpriseValue AverageDailyVolume10Day { get; set; }

        [JsonProperty("totalAssets")]
        public Details TotalAssets { get; set; }

        [JsonProperty("regularMarketPreviousClose")]
        public The52_WeekChange RegularMarketPreviousClose { get; set; }

        [JsonProperty("fiftyDayAverage")]
        public The52_WeekChange FiftyDayAverage { get; set; }

        [JsonProperty("trailingAnnualDividendRate")]
        public Details TrailingAnnualDividendRate { get; set; }

        [JsonProperty("open")]
        public The52_WeekChange Open { get; set; }

        [JsonProperty("toCurrency")]
        public dynamic ToCurrency { get; set; }

        [JsonProperty("averageVolume10days")]
        public EnterpriseValue AverageVolume10Days { get; set; }

        [JsonProperty("expireDate")]
        public Details ExpireDate { get; set; }

        [JsonProperty("yield")]
        public Details Yield { get; set; }

        [JsonProperty("algorithm")]
        public dynamic Algorithm { get; set; }

        [JsonProperty("dividendRate")]
        public Details DividendRate { get; set; }

        [JsonProperty("exDividendDate")]
        public Details ExDividendDate { get; set; }

        [JsonProperty("beta")]
        public The52_WeekChange Beta { get; set; }

        [JsonProperty("circulatingSupply")]
        public Details CirculatingSupply { get; set; }

        [JsonProperty("startDate")]
        public Details StartDate { get; set; }

        [JsonProperty("regularMarketDayLow")]
        public The52_WeekChange RegularMarketDayLow { get; set; }

        [JsonProperty("priceHint")]
        public EnterpriseValue PriceHint { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("regularMarketVolume")]
        public EnterpriseValue RegularMarketVolume { get; set; }

        [JsonProperty("lastMarket")]
        public dynamic LastMarket { get; set; }

        [JsonProperty("maxSupply")]
        public Details MaxSupply { get; set; }

        [JsonProperty("openInterest")]
        public Details OpenInterest { get; set; }

        [JsonProperty("marketCap")]
        public EnterpriseValue MarketCap { get; set; }

        [JsonProperty("volumeAllCurrencies")]
        public Details VolumeAllCurrencies { get; set; }

        [JsonProperty("strikePrice")]
        public Details StrikePrice { get; set; }

        [JsonProperty("averageVolume")]
        public EnterpriseValue AverageVolume { get; set; }

        [JsonProperty("priceToSalesTrailing12Months")]
        public The52_WeekChange PriceToSalesTrailing12Months { get; set; }

        [JsonProperty("dayLow")]
        public The52_WeekChange DayLow { get; set; }

        [JsonProperty("ask")]
        public The52_WeekChange Ask { get; set; }

        [JsonProperty("ytdReturn")]
        public Details YtdReturn { get; set; }

        [JsonProperty("askSize")]
        public EnterpriseValue AskSize { get; set; }

        [JsonProperty("volume")]
        public EnterpriseValue Volume { get; set; }

        [JsonProperty("fiftyTwoWeekHigh")]
        public The52_WeekChange FiftyTwoWeekHigh { get; set; }

        [JsonProperty("forwardPE")]
        public The52_WeekChange ForwardPe { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("fromCurrency")]
        public dynamic FromCurrency { get; set; }

        [JsonProperty("fiveYearAvgDividendYield")]
        public Details FiveYearAvgDividendYield { get; set; }

        [JsonProperty("fiftyTwoWeekLow")]
        public The52_WeekChange FiftyTwoWeekLow { get; set; }

        [JsonProperty("bid")]
        public The52_WeekChange Bid { get; set; }

        [JsonProperty("tradeable")]
        public bool Tradeable { get; set; }

        [JsonProperty("dividendYield")]
        public Details DividendYield { get; set; }

        [JsonProperty("bidSize")]
        public EnterpriseValue BidSize { get; set; }

        [JsonProperty("dayHigh")]
        public The52_WeekChange DayHigh { get; set; }
    }

    public partial class SummaryProfile
    {
        [JsonProperty("zip")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Zip { get; set; }

        [JsonProperty("sector")]
        public string Sector { get; set; }

        [JsonProperty("fullTimeEmployees")]
        public long FullTimeEmployees { get; set; }

        [JsonProperty("longBusinessSummary")]
        public string LongBusinessSummary { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("companyOfficers")]
        public List<dynamic> CompanyOfficers { get; set; }

        [JsonProperty("website")]
        public Uri Website { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("industry")]
        public string Industry { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }
    }

    public partial class UpgradeDowngradeHistorySummary
    {
        [JsonProperty("history")]
        public List<History> History { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }
    }

    public partial class History
    {
        [JsonProperty("epochGradeDate")]
        public long EpochGradeDate { get; set; }

        [JsonProperty("firm")]
        public string Firm { get; set; }

        [JsonProperty("toGrade")]
        public string ToGrade { get; set; }

        [JsonProperty("fromGrade")]
        [JsonConverter(typeof(FromGradeConverter))]
        public dynamic FromGrade { get; set; }

        [JsonProperty("action")]
        [JsonConverter(typeof(ActionConverter))]
        public dynamic Action { get; set; }
    }

    public enum Name { BergAaronD, EkmanLarsG, KalbMichaelWayne, KennedyJosephT, KetchumStevenB, PetersonKristine, StackDavidM, TheroJohnF, VanHeekGJan, ZakrzewskiJosephSJr };

    public enum Relation { ChiefExecutiveOfficer, ChiefFinancialOfficer, Director, GeneralCounsel, Officer };

    public enum TransactionDescription { ConversionOfExerciseOfDerivativeSecurity, Sale };

    public enum OwnershipEnum { D, I };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                NameConverter.Singleton,
                RelationConverter.Singleton,
                TransactionDescriptionConverter.Singleton,
                OwnershipEnumConverter.Singleton,
                ActionConverter.Singleton,
                FromGradeConverter.Singleton
            },
        };
    }

    internal class NameConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Name) || t == typeof(Name?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "BERG AARON D.":
                    return Name.BergAaronD;
                case "EKMAN LARS G":
                    return Name.EkmanLarsG;
                case "KALB MICHAEL WAYNE":
                    return Name.KalbMichaelWayne;
                case "KENNEDY JOSEPH T":
                    return Name.KennedyJosephT;
                case "KETCHUM STEVEN B":
                    return Name.KetchumStevenB;
                case "PETERSON KRISTINE":
                    return Name.PetersonKristine;
                case "STACK DAVID M":
                    return Name.StackDavidM;
                case "THERO JOHN F":
                    return Name.TheroJohnF;
                case "VAN HEEK G JAN":
                    return Name.VanHeekGJan;
                case "ZAKRZEWSKI JOSEPH S. JR":
                    return Name.ZakrzewskiJosephSJr;
            }
            throw new Exception("Cannot unmarshal type Name");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Name)untypedValue;
            switch (value)
            {
                case Name.BergAaronD:
                    serializer.Serialize(writer, "BERG AARON D.");
                    return;
                case Name.EkmanLarsG:
                    serializer.Serialize(writer, "EKMAN LARS G");
                    return;
                case Name.KalbMichaelWayne:
                    serializer.Serialize(writer, "KALB MICHAEL WAYNE");
                    return;
                case Name.KennedyJosephT:
                    serializer.Serialize(writer, "KENNEDY JOSEPH T");
                    return;
                case Name.KetchumStevenB:
                    serializer.Serialize(writer, "KETCHUM STEVEN B");
                    return;
                case Name.PetersonKristine:
                    serializer.Serialize(writer, "PETERSON KRISTINE");
                    return;
                case Name.StackDavidM:
                    serializer.Serialize(writer, "STACK DAVID M");
                    return;
                case Name.TheroJohnF:
                    serializer.Serialize(writer, "THERO JOHN F");
                    return;
                case Name.VanHeekGJan:
                    serializer.Serialize(writer, "VAN HEEK G JAN");
                    return;
                case Name.ZakrzewskiJosephSJr:
                    serializer.Serialize(writer, "ZAKRZEWSKI JOSEPH S. JR");
                    return;
            }
            throw new Exception("Cannot marshal type Name");
        }

        public static readonly NameConverter Singleton = new NameConverter();
    }

    internal class RelationConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Relation) || t == typeof(Relation?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Chief Executive Officer":
                    return Relation.ChiefExecutiveOfficer;
                case "Chief Financial Officer":
                    return Relation.ChiefFinancialOfficer;
                case "Director":
                    return Relation.Director;
                case "General Counsel":
                    return Relation.GeneralCounsel;
                case "Officer":
                    return Relation.Officer;
            }
            throw new Exception("Cannot unmarshal type Relation");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Relation)untypedValue;
            switch (value)
            {
                case Relation.ChiefExecutiveOfficer:
                    serializer.Serialize(writer, "Chief Executive Officer");
                    return;
                case Relation.ChiefFinancialOfficer:
                    serializer.Serialize(writer, "Chief Financial Officer");
                    return;
                case Relation.Director:
                    serializer.Serialize(writer, "Director");
                    return;
                case Relation.GeneralCounsel:
                    serializer.Serialize(writer, "General Counsel");
                    return;
                case Relation.Officer:
                    serializer.Serialize(writer, "Officer");
                    return;
            }
            throw new Exception("Cannot marshal type Relation");
        }

        public static readonly RelationConverter Singleton = new RelationConverter();
    }

    internal class TransactionDescriptionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TransactionDescription) || t == typeof(TransactionDescription?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Conversion of Exercise of derivative security":
                    return TransactionDescription.ConversionOfExerciseOfDerivativeSecurity;
                case "Sale":
                    return TransactionDescription.Sale;
            }
            throw new Exception("Cannot unmarshal type TransactionDescription");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TransactionDescription)untypedValue;
            switch (value)
            {
                case TransactionDescription.ConversionOfExerciseOfDerivativeSecurity:
                    serializer.Serialize(writer, "Conversion of Exercise of derivative security");
                    return;
                case TransactionDescription.Sale:
                    serializer.Serialize(writer, "Sale");
                    return;
            }
            throw new Exception("Cannot marshal type TransactionDescription");
        }

        public static readonly TransactionDescriptionConverter Singleton = new TransactionDescriptionConverter();
    }

    internal class OwnershipEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(OwnershipEnum) || t == typeof(OwnershipEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "D":
                    return OwnershipEnum.D;
                case "I":
                    return OwnershipEnum.I;
            }
            throw new Exception("Cannot unmarshal type OwnershipEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (OwnershipEnum)untypedValue;
            switch (value)
            {
                case OwnershipEnum.D:
                    serializer.Serialize(writer, "D");
                    return;
                case OwnershipEnum.I:
                    serializer.Serialize(writer, "I");
                    return;
            }
            throw new Exception("Cannot marshal type OwnershipEnum");
        }

        public static readonly OwnershipEnumConverter Singleton = new OwnershipEnumConverter();
    }

    /*
     {
      "defaultKeyStatistics": {
        "annualHoldingsTurnover": {},
        "enterpriseToRevenue": {
          "raw": 3.17,
          "fmt": "3.17"
        },
        "beta3Year": {},
        "profitMargins": {
          "raw": -0.02931,
          "fmt": "-2.93%"
        },
        "enterpriseToEbitda": {
          "raw": -110.478,
          "fmt": "-110.48"
        },
        "52WeekChange": {
          "raw": -0.6016731,
          "fmt": "-60.17%"
        },
        "morningStarRiskRating": {},
        "forwardEps": {
          "raw": 0.11,
          "fmt": "0.11"
        },
        "revenueQuarterlyGrowth": {},
        "sharesOutstanding": {
          "raw": 392537984,
          "fmt": "392.54M",
          "longFmt": "392,537,984"
        },
        "fundInceptionDate": {},
        "annualReportExpenseRatio": {},
        "totalAssets": {},
        "bookValue": {
          "raw": 1.599,
          "fmt": "1.60"
        },
        "sharesShort": {
          "raw": 20135782,
          "fmt": "20.14M",
          "longFmt": "20,135,782"
        },
        "sharesPercentSharesOut": {
          "raw": 0.051799998,
          "fmt": "5.18%"
        },
        "fundFamily": null,
        "lastFiscalYearEnd": {
          "raw": 1609372800,
          "fmt": "2020-12-31"
        },
        "heldPercentInstitutions": {
          "raw": 0.39435002,
          "fmt": "39.44%"
        },
        "netIncomeToCommon": {
          "raw": -18000000,
          "fmt": "-18M",
          "longFmt": "-18,000,000"
        },
        "trailingEps": {
          "raw": -0.05,
          "fmt": "-0.05"
        },
        "lastDividendValue": {},
        "SandP52WeekChange": {
          "raw": 0.29914725,
          "fmt": "29.91%"
        },
        "priceToBook": {
          "raw": 3.7711072,
          "fmt": "3.77"
        },
        "heldPercentInsiders": {
          "raw": 0.0103400005,
          "fmt": "1.03%"
        },
        "nextFiscalYearEnd": {
          "raw": 1672444800,
          "fmt": "2022-12-31"
        },
        "yield": {},
        "mostRecentQuarter": {
          "raw": 1609372800,
          "fmt": "2020-12-31"
        },
        "shortRatio": {
          "raw": 2.45,
          "fmt": "2.45"
        },
        "sharesShortPreviousMonthDate": {
          "raw": 1610668800,
          "fmt": "2021-01-15"
        },
        "floatShares": {
          "raw": 357814640,
          "fmt": "357.81M",
          "longFmt": "357,814,640"
        },
        "beta": {
          "raw": 2.377801,
          "fmt": "2.38"
        },
        "enterpriseValue": {
          "raw": 1946298496,
          "fmt": "1.95B",
          "longFmt": "1,946,298,496"
        },
        "priceHint": {
          "raw": 2,
          "fmt": "2",
          "longFmt": "2"
        },
        "threeYearAverageReturn": {},
        "lastSplitDate": {
          "raw": 1200614400,
          "fmt": "2008-01-18"
        },
        "lastSplitFactor": "1:10",
        "legalType": null,
        "lastDividendDate": {},
        "morningStarOverallRating": {},
        "earningsQuarterlyGrowth": {
          "raw": -0.303,
          "fmt": "-30.30%"
        },
        "priceToSalesTrailing12Months": {},
        "dateShortInterest": {
          "raw": 1613088000,
          "fmt": "2021-02-12"
        },
        "pegRatio": {
          "raw": -1.23,
          "fmt": "-1.23"
        },
        "ytdReturn": {},
        "forwardPE": {
          "raw": 54.818184,
          "fmt": "54.82"
        },
        "maxAge": 1,
        "lastCapGain": {},
        "shortPercentOfFloat": {},
        "sharesShortPriorMonth": {
          "raw": 21987649,
          "fmt": "21.99M",
          "longFmt": "21,987,649"
        },
        "impliedSharesOutstanding": {},
        "category": null,
        "fiveYearAverageReturn": {}
      },
      "details": {},
      "summaryProfile": {
        "zip": "2",
        "sector": "Healthcare",
        "fullTimeEmployees": 1000,
        "longBusinessSummary": "Amarin Corporation plc, a pharmaceutical company, develops and commercializes therapeutics for the treatment of cardiovascular diseases in the United States. The company's lead product is Vascepa, a prescription-only omega-3 fatty acid capsule, used as an adjunct to diet for reducing triglyceride levels in adult patients with severe hypertriglyceridemia. It is also involved in developing REDUCE-IT for the treatment of patients with high triglyceride levels who are also on statin therapy for elevated low-density lipoprotein cholesterol levels. The company sells its products principally to wholesalers and specialty pharmacy providers through direct sales force. It has a collaboration with Mochida Pharmaceutical Co., Ltd. to develop and commercialize drug products and indications based on the active pharmaceutical ingredient in Vascepa, the omega-3 acid, and eicosapentaenoic acid. The company was formerly known as Ethical Holdings plc and changed its name to Amarin Corporation plc in 1999. Amarin Corporation plc was incorporated in 1989 and is headquartered in Dublin, Ireland.",
        "city": "Dublin",
        "phone": "353 1 669 9020",
        "country": "Ireland",
        "companyOfficers": [],
        "website": "http://www.amarincorp.com",
        "maxAge": 86400,
        "address1": "Grand Canal Docklands",
        "industry": "Biotechnology",
        "address2": "Block C 77 Sir John Rogerson's Quay"
      },
      "recommendationTrend": {
        "trend": [
          {
            "period": "0m",
            "strongBuy": 2,
            "buy": 3,
            "hold": 0,
            "sell": 0,
            "strongSell": 0
          },
          {
            "period": "-1m",
            "strongBuy": 2,
            "buy": 6,
            "hold": 4,
            "sell": 0,
            "strongSell": 0
          },
          {
            "period": "-2m",
            "strongBuy": 2,
            "buy": 6,
            "hold": 5,
            "sell": 0,
            "strongSell": 0
          },
          {
            "period": "-3m",
            "strongBuy": 2,
            "buy": 3,
            "hold": 0,
            "sell": 0,
            "strongSell": 0
          }
        ],
        "maxAge": 86400
      },
      "financialsTemplate": {
        "code": "N",
        "maxAge": 1
      },
      "majorDirectHolders": {
        "holders": [],
        "maxAge": 1
      },
      "earnings": {
        "maxAge": 86400,
        "earningsChart": {
          "quarterly": [
            {
              "date": "1Q2020",
              "actual": {
                "raw": -0.06,
                "fmt": "-0.06"
              },
              "estimate": {
                "raw": -0.07,
                "fmt": "-0.07"
              }
            },
            {
              "date": "2Q2020",
              "actual": {
                "raw": 0.01,
                "fmt": "0.01"
              },
              "estimate": {
                "raw": -0.07,
                "fmt": "-0.07"
              }
            },
            {
              "date": "3Q2020",
              "actual": {
                "raw": -0.02,
                "fmt": "-0.02"
              },
              "estimate": {
                "raw": 0.01,
                "fmt": "0.01"
              }
            },
            {
              "date": "4Q2020",
              "actual": {
                "raw": 0.01,
                "fmt": "0.01"
              },
              "estimate": {
                "raw": -0.01,
                "fmt": "-0.01"
              }
            }
          ],
          "currentQuarterEstimate": {
            "raw": -0.05,
            "fmt": "-0.05"
          },
          "currentQuarterEstimateDate": "1Q",
          "currentQuarterEstimateYear": 2021,
          "earningsDate": [
            {
              "raw": 1614211200,
              "fmt": "2021-02-25"
            }
          ]
        },
        "financialsChart": {
          "yearly": [
            {
              "date": 2017,
              "revenue": {
                "raw": 181104000,
                "fmt": "181.1M",
                "longFmt": "181,104,000"
              },
              "earnings": {
                "raw": -67865000,
                "fmt": "-67.86M",
                "longFmt": "-67,865,000"
              }
            },
            {
              "date": 2018,
              "revenue": {
                "raw": 229214000,
                "fmt": "229.21M",
                "longFmt": "229,214,000"
              },
              "earnings": {
                "raw": -116445000,
                "fmt": "-116.44M",
                "longFmt": "-116,445,000"
              }
            },
            {
              "date": 2019,
              "revenue": {
                "raw": 429755000,
                "fmt": "429.75M",
                "longFmt": "429,755,000"
              },
              "earnings": {
                "raw": -22645000,
                "fmt": "-22.64M",
                "longFmt": "-22,645,000"
              }
            },
            {
              "date": 2020,
              "revenue": {
                "raw": 614060000,
                "fmt": "614.06M",
                "longFmt": "614,060,000"
              },
              "earnings": {
                "raw": -18000000,
                "fmt": "-18M",
                "longFmt": "-18,000,000"
              }
            }
          ],
          "quarterly": [
            {
              "date": "1Q2020",
              "revenue": {
                "raw": 154993000,
                "fmt": "154.99M",
                "longFmt": "154,993,000"
              },
              "earnings": {
                "raw": -20553000,
                "fmt": "-20.55M",
                "longFmt": "-20,553,000"
              }
            },
            {
              "date": "2Q2020",
              "revenue": {
                "raw": 135317000,
                "fmt": "135.32M",
                "longFmt": "135,317,000"
              },
              "earnings": {
                "raw": 4415000,
                "fmt": "4.42M",
                "longFmt": "4,415,000"
              }
            },
            {
              "date": "3Q2020",
              "revenue": {
                "raw": 156499000,
                "fmt": "156.5M",
                "longFmt": "156,499,000"
              },
              "earnings": {
                "raw": -6788000,
                "fmt": "-6.79M",
                "longFmt": "-6,788,000"
              }
            },
            {
              "date": "4Q2020",
              "revenue": {
                "raw": 167251000,
                "fmt": "167.25M",
                "longFmt": "167,251,000"
              },
              "earnings": {
                "raw": 4926000,
                "fmt": "4.93M",
                "longFmt": "4,926,000"
              }
            }
          ]
        },
        "financialCurrency": "USD"
      },
      "price": {
        "quoteSourceName": "Nasdaq Real Time Price",
        "regularMarketOpen": {
          "raw": 6.17,
          "fmt": "6.17"
        },
        "averageDailyVolume3Month": {
          "raw": 7947921,
          "fmt": "7.95M",
          "longFmt": "7,947,921"
        },
        "exchange": "NMS",
        "regularMarketTime": 1614718801,
        "volume24Hr": {},
        "regularMarketDayHigh": {
          "raw": 6.28,
          "fmt": "6.28"
        },
        "shortName": "Amarin Corporation plc",
        "averageDailyVolume10Day": {
          "raw": 7208950,
          "fmt": "7.21M",
          "longFmt": "7,208,950"
        },
        "longName": "Amarin Corporation plc",
        "regularMarketChange": {
          "raw": -0.15999985,
          "fmt": "-0.16"
        },
        "currencySymbol": "$",
        "regularMarketPreviousClose": {
          "raw": 6.19,
          "fmt": "6.19"
        },
        "postMarketTime": 1614725577,
        "preMarketPrice": {
          "raw": 6.15,
          "fmt": "6.15"
        },
        "preMarketTime": 1614695396,
        "exchangeDataDelayedBy": 0,
        "toCurrency": null,
        "postMarketChange": {
          "raw": 0,
          "fmt": "0.00"
        },
        "postMarketPrice": {
          "raw": 6.03,
          "fmt": "6.03"
        },
        "exchangeName": "NasdaqGS",
        "preMarketChange": {
          "raw": -0.04,
          "fmt": "-0.04"
        },
        "circulatingSupply": {},
        "regularMarketDayLow": {
          "raw": 5.94,
          "fmt": "5.94"
        },
        "priceHint": {
          "raw": 2,
          "fmt": "2",
          "longFmt": "2"
        },
        "currency": "USD",
        "regularMarketPrice": {
          "raw": 6.03,
          "fmt": "6.03"
        },
        "regularMarketVolume": {
          "raw": 11547574,
          "fmt": "11.55M",
          "longFmt": "11,547,574.00"
        },
        "lastMarket": null,
        "regularMarketSource": "FREE_REALTIME",
        "openInterest": {},
        "marketState": "POST",
        "underlyingSymbol": null,
        "marketCap": {
          "raw": 2373619200,
          "fmt": "2.37B",
          "longFmt": "2,373,619,200.00"
        },
        "quoteType": "EQUITY",
        "preMarketChangePercent": {
          "raw": -0.0064620296,
          "fmt": "-0.65%"
        },
        "volumeAllCurrencies": {},
        "postMarketSource": "FREE_REALTIME",
        "strikePrice": {},
        "symbol": "AMRN",
        "postMarketChangePercent": {
          "raw": 0,
          "fmt": "0.00%"
        },
        "preMarketSource": "FREE_REALTIME",
        "maxAge": 1,
        "fromCurrency": null,
        "regularMarketChangePercent": {
          "raw": -0.025848117,
          "fmt": "-2.58%"
        }
      },
      "fundOwnership": {
        "maxAge": 1,
        "ownershipList": [
          {
            "maxAge": 1,
            "reportDate": {
              "raw": 1604102400,
              "fmt": "2020-10-31"
            },
            "organization": "Federated Hermes Kaufmann Small Cap Fund",
            "pctHeld": {
              "raw": 0.009,
              "fmt": "0.90%"
            },
            "position": {
              "raw": 3500000,
              "fmt": "3.5M",
              "longFmt": "3,500,000"
            },
            "value": {
              "raw": 17010000,
              "fmt": "17.01M",
              "longFmt": "17,010,000"
            }
          },
          {
            "maxAge": 1,
            "reportDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "organization": "iShares NASDAQ Biotechnology ETF",
            "pctHeld": {
              "raw": 0.0087,
              "fmt": "0.87%"
            },
            "position": {
              "raw": 3387248,
              "fmt": "3.39M",
              "longFmt": "3,387,248"
            },
            "value": {
              "raw": 16563642,
              "fmt": "16.56M",
              "longFmt": "16,563,642"
            }
          },
          {
            "maxAge": 1,
            "reportDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "organization": "Legg Mason Glb Asset Mgt Tr-Clearbridge Small Cap Fd",
            "pctHeld": {
              "raw": 0.0082,
              "fmt": "0.82%"
            },
            "position": {
              "raw": 3174996,
              "fmt": "3.17M",
              "longFmt": "3,174,996"
            },
            "value": {
              "raw": 15525730,
              "fmt": "15.53M",
              "longFmt": "15,525,730"
            }
          },
          {
            "maxAge": 1,
            "reportDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "organization": "Value Line Capital Appreciation Fund",
            "pctHeld": {
              "raw": 0.0033000002,
              "fmt": "0.33%"
            },
            "position": {
              "raw": 1275000,
              "fmt": "1.27M",
              "longFmt": "1,275,000"
            },
            "value": {
              "raw": 6234750,
              "fmt": "6.23M",
              "longFmt": "6,234,750"
            }
          },
          {
            "maxAge": 1,
            "reportDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "organization": "Price (T.Rowe) Health Sciences Fund",
            "pctHeld": {
              "raw": 0.0026,
              "fmt": "0.26%"
            },
            "position": {
              "raw": 1011343,
              "fmt": "1.01M",
              "longFmt": "1,011,343"
            },
            "value": {
              "raw": 4945467,
              "fmt": "4.95M",
              "longFmt": "4,945,467"
            }
          },
          {
            "maxAge": 1,
            "reportDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "organization": "Guardian VP Tr-Guardian Small Cap Core VIP Fd",
            "pctHeld": {
              "raw": 0.0023,
              "fmt": "0.23%"
            },
            "position": {
              "raw": 907776,
              "fmt": "907.78k",
              "longFmt": "907,776"
            },
            "value": {
              "raw": 4439024,
              "fmt": "4.44M",
              "longFmt": "4,439,024"
            }
          },
          {
            "maxAge": 1,
            "reportDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "organization": "Value Line Larger Companies Focused Fund",
            "pctHeld": {
              "raw": 0.0023,
              "fmt": "0.23%"
            },
            "position": {
              "raw": 875000,
              "fmt": "875k",
              "longFmt": "875,000"
            },
            "value": {
              "raw": 4278750,
              "fmt": "4.28M",
              "longFmt": "4,278,750"
            }
          },
          {
            "maxAge": 1,
            "reportDate": {
              "raw": 1601424000,
              "fmt": "2020-09-30"
            },
            "organization": "Tekla  Healthcare Investors",
            "pctHeld": {
              "raw": 0.0014,
              "fmt": "0.14%"
            },
            "position": {
              "raw": 557618,
              "fmt": "557.62k",
              "longFmt": "557,618"
            },
            "value": {
              "raw": 2347571,
              "fmt": "2.35M",
              "longFmt": "2,347,571"
            }
          },
          {
            "maxAge": 1,
            "reportDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "organization": "VanEck Vectors ETF Tr-Pharmaceutical ETF",
            "pctHeld": {
              "raw": 0.0011999999,
              "fmt": "0.12%"
            },
            "position": {
              "raw": 478470,
              "fmt": "478.47k",
              "longFmt": "478,470"
            },
            "value": {
              "raw": 2339718,
              "fmt": "2.34M",
              "longFmt": "2,339,718"
            }
          },
          {
            "maxAge": 1,
            "reportDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "organization": "Columbia Fds Var Ser Tr II-Columbia Var Port-Overseas Core Fd",
            "pctHeld": {
              "raw": 0.00090000004,
              "fmt": "0.09%"
            },
            "position": {
              "raw": 360928,
              "fmt": "360.93k",
              "longFmt": "360,928"
            },
            "value": {
              "raw": 1764937,
              "fmt": "1.76M",
              "longFmt": "1,764,937"
            }
          }
        ]
      },
      "insiderTransactions": {
        "transactions": [
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "Sale at price 8.03 - 8.06 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1612137600,
              "fmt": "2021-02-01"
            },
            "value": {
              "raw": 1754134,
              "fmt": "1.75M",
              "longFmt": "1,754,134"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 217728,
              "fmt": "217.73k",
              "longFmt": "217,728"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "Conversion of Exercise of derivative security at price 2.50 - 2.95 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1612137600,
              "fmt": "2021-02-01"
            },
            "value": {
              "raw": 485882,
              "fmt": "485.88k",
              "longFmt": "485,882"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 192358,
              "fmt": "192.36k",
              "longFmt": "192,358"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1611878400,
              "fmt": "2021-01-29"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 209171,
              "fmt": "209.17k",
              "longFmt": "209,171"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1611878400,
              "fmt": "2021-01-29"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 42345,
              "fmt": "42.34k",
              "longFmt": "42,345"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "Conversion of Exercise of derivative security at price 1.02 - 3.80 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1611878400,
              "fmt": "2021-01-29"
            },
            "value": {
              "raw": 764566,
              "fmt": "764.57k",
              "longFmt": "764,566"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 320026,
              "fmt": "320.03k",
              "longFmt": "320,026"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1611878400,
              "fmt": "2021-01-29"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 41789,
              "fmt": "41.79k",
              "longFmt": "41,789"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1611878400,
              "fmt": "2021-01-29"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 15789,
              "fmt": "15.79k",
              "longFmt": "15,789"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "Sale at price 8.01 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1611705600,
              "fmt": "2021-01-27"
            },
            "value": {
              "raw": 3378131,
              "fmt": "3.38M",
              "longFmt": "3,378,131"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 421629,
              "fmt": "421.63k",
              "longFmt": "421,629"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 13472,
              "fmt": "13.47k",
              "longFmt": "13,472"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 3612,
              "fmt": "3.61k",
              "longFmt": "3,612"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1606694400,
              "fmt": "2020-11-30"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 13472,
              "fmt": "13.47k",
              "longFmt": "13,472"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1606694400,
              "fmt": "2020-11-30"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 3612,
              "fmt": "3.61k",
              "longFmt": "3,612"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1606694400,
              "fmt": "2020-11-30"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1606694400,
              "fmt": "2020-11-30"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1606694400,
              "fmt": "2020-11-30"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "Sale at price 4.07 - 4.13 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1605052800,
              "fmt": "2020-11-11"
            },
            "value": {
              "raw": 2317158,
              "fmt": "2.32M",
              "longFmt": "2,317,158"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 567405,
              "fmt": "567.4k",
              "longFmt": "567,405"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "Conversion of Exercise of derivative security at price 3.40 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1604966400,
              "fmt": "2020-11-10"
            },
            "value": {
              "raw": 2550000,
              "fmt": "2.55M",
              "longFmt": "2,550,000"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 750000,
              "fmt": "750k",
              "longFmt": "750,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1604016000,
              "fmt": "2020-10-30"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 13472,
              "fmt": "13.47k",
              "longFmt": "13,472"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1604016000,
              "fmt": "2020-10-30"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 3612,
              "fmt": "3.61k",
              "longFmt": "3,612"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1604016000,
              "fmt": "2020-10-30"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1604016000,
              "fmt": "2020-10-30"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1604016000,
              "fmt": "2020-10-30"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "ZAKRZEWSKI JOSEPH S. JR",
            "transactionText": "Conversion of Exercise of derivative security at price 3.40 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1603238400,
              "fmt": "2020-10-21"
            },
            "value": {
              "raw": 340000,
              "fmt": "340k",
              "longFmt": "340,000"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 100000,
              "fmt": "100k",
              "longFmt": "100,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1601424000,
              "fmt": "2020-09-30"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 13474,
              "fmt": "13.47k",
              "longFmt": "13,474"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1601424000,
              "fmt": "2020-09-30"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 3612,
              "fmt": "3.61k",
              "longFmt": "3,612"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1601424000,
              "fmt": "2020-09-30"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1601424000,
              "fmt": "2020-09-30"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1601424000,
              "fmt": "2020-09-30"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1598832000,
              "fmt": "2020-08-31"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 13472,
              "fmt": "13.47k",
              "longFmt": "13,472"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1598832000,
              "fmt": "2020-08-31"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 3610,
              "fmt": "3.61k",
              "longFmt": "3,610"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1598832000,
              "fmt": "2020-08-31"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 3054,
              "fmt": "3.05k",
              "longFmt": "3,054"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1598832000,
              "fmt": "2020-08-31"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 3054,
              "fmt": "3.05k",
              "longFmt": "3,054"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1598832000,
              "fmt": "2020-08-31"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 3054,
              "fmt": "3.05k",
              "longFmt": "3,054"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1596153600,
              "fmt": "2020-07-31"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 13472,
              "fmt": "13.47k",
              "longFmt": "13,472"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1596153600,
              "fmt": "2020-07-31"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 3610,
              "fmt": "3.61k",
              "longFmt": "3,610"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1596153600,
              "fmt": "2020-07-31"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 3054,
              "fmt": "3.05k",
              "longFmt": "3,054"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1596153600,
              "fmt": "2020-07-31"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 3054,
              "fmt": "3.05k",
              "longFmt": "3,054"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1596153600,
              "fmt": "2020-07-31"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 3054,
              "fmt": "3.05k",
              "longFmt": "3,054"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1593475200,
              "fmt": "2020-06-30"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 13472,
              "fmt": "13.47k",
              "longFmt": "13,472"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1593475200,
              "fmt": "2020-06-30"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 3610,
              "fmt": "3.61k",
              "longFmt": "3,610"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1593475200,
              "fmt": "2020-06-30"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 3054,
              "fmt": "3.05k",
              "longFmt": "3,054"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1593475200,
              "fmt": "2020-06-30"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 3054,
              "fmt": "3.05k",
              "longFmt": "3,054"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1593475200,
              "fmt": "2020-06-30"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 3054,
              "fmt": "3.05k",
              "longFmt": "3,054"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1590710400,
              "fmt": "2020-05-29"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 13472,
              "fmt": "13.47k",
              "longFmt": "13,472"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1590710400,
              "fmt": "2020-05-29"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 3610,
              "fmt": "3.61k",
              "longFmt": "3,610"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1590710400,
              "fmt": "2020-05-29"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1590710400,
              "fmt": "2020-05-29"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1590710400,
              "fmt": "2020-05-29"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1588291200,
              "fmt": "2020-05-01"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 29056,
              "fmt": "29.06k",
              "longFmt": "29,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1588204800,
              "fmt": "2020-04-30"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 13472,
              "fmt": "13.47k",
              "longFmt": "13,472"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1588204800,
              "fmt": "2020-04-30"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 3610,
              "fmt": "3.61k",
              "longFmt": "3,610"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1588204800,
              "fmt": "2020-04-30"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1588204800,
              "fmt": "2020-04-30"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1585612800,
              "fmt": "2020-03-31"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 13472,
              "fmt": "13.47k",
              "longFmt": "13,472"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1585612800,
              "fmt": "2020-03-31"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 3612,
              "fmt": "3.61k",
              "longFmt": "3,612"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1585612800,
              "fmt": "2020-03-31"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1585612800,
              "fmt": "2020-03-31"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1585612800,
              "fmt": "2020-03-31"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "Sale at price 15.99 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1583280000,
              "fmt": "2020-03-04"
            },
            "value": {
              "raw": 3198700,
              "fmt": "3.2M",
              "longFmt": "3,198,700"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 200000,
              "fmt": "200k",
              "longFmt": "200,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1582848000,
              "fmt": "2020-02-28"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 13472,
              "fmt": "13.47k",
              "longFmt": "13,472"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1582848000,
              "fmt": "2020-02-28"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 3612,
              "fmt": "3.61k",
              "longFmt": "3,612"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1582848000,
              "fmt": "2020-02-28"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1582848000,
              "fmt": "2020-02-28"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1582848000,
              "fmt": "2020-02-28"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 3056,
              "fmt": "3.06k",
              "longFmt": "3,056"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1582588800,
              "fmt": "2020-02-25"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 229030,
              "fmt": "229.03k",
              "longFmt": "229,030"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1582588800,
              "fmt": "2020-02-25"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 61394,
              "fmt": "61.39k",
              "longFmt": "61,394"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1582588800,
              "fmt": "2020-02-25"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 51948,
              "fmt": "51.95k",
              "longFmt": "51,948"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1582588800,
              "fmt": "2020-02-25"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 51948,
              "fmt": "51.95k",
              "longFmt": "51,948"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1582588800,
              "fmt": "2020-02-25"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 51948,
              "fmt": "51.95k",
              "longFmt": "51,948"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1580428800,
              "fmt": "2020-01-31"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 315367,
              "fmt": "315.37k",
              "longFmt": "315,367"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1580428800,
              "fmt": "2020-01-31"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 74734,
              "fmt": "74.73k",
              "longFmt": "74,734"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1580428800,
              "fmt": "2020-01-31"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 67734,
              "fmt": "67.73k",
              "longFmt": "67,734"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1580428800,
              "fmt": "2020-01-31"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 67734,
              "fmt": "67.73k",
              "longFmt": "67,734"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1580428800,
              "fmt": "2020-01-31"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 41734,
              "fmt": "41.73k",
              "longFmt": "41,734"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "ZAKRZEWSKI JOSEPH S. JR",
            "transactionText": "Sale at price 20.94 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1578009600,
              "fmt": "2020-01-03"
            },
            "value": {
              "raw": 6282870,
              "fmt": "6.28M",
              "longFmt": "6,282,870"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 300000,
              "fmt": "300k",
              "longFmt": "300,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "ZAKRZEWSKI JOSEPH S. JR",
            "transactionText": "Conversion of Exercise of derivative security at price 3.40 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1578009600,
              "fmt": "2020-01-03"
            },
            "value": {
              "raw": 1020000,
              "fmt": "1.02M",
              "longFmt": "1,020,000"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 300000,
              "fmt": "300k",
              "longFmt": "300,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "ZAKRZEWSKI JOSEPH S. JR",
            "transactionText": "Sale at price 25.83 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1576454400,
              "fmt": "2019-12-16"
            },
            "value": {
              "raw": 2583380,
              "fmt": "2.58M",
              "longFmt": "2,583,380"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 100000,
              "fmt": "100k",
              "longFmt": "100,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "ZAKRZEWSKI JOSEPH S. JR",
            "transactionText": "Conversion of Exercise of derivative security at price 9.00 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1576454400,
              "fmt": "2019-12-16"
            },
            "value": {
              "raw": 900000,
              "fmt": "900k",
              "longFmt": "900,000"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 100000,
              "fmt": "100k",
              "longFmt": "100,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "Sale at price 23.05 - 25.66 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1576454400,
              "fmt": "2019-12-16"
            },
            "value": {
              "raw": 584012,
              "fmt": "584.01k",
              "longFmt": "584,012"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 25000,
              "fmt": "25k",
              "longFmt": "25,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "Conversion of Exercise of derivative security at price 2.19 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1576454400,
              "fmt": "2019-12-16"
            },
            "value": {
              "raw": 54750,
              "fmt": "54.75k",
              "longFmt": "54,750"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 25000,
              "fmt": "25k",
              "longFmt": "25,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "Sale at price 25.46 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1576454400,
              "fmt": "2019-12-16"
            },
            "value": {
              "raw": 1101364,
              "fmt": "1.1M",
              "longFmt": "1,101,364"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 43253,
              "fmt": "43.25k",
              "longFmt": "43,253"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "Conversion of Exercise of derivative security at price 12.60 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1576454400,
              "fmt": "2019-12-16"
            },
            "value": {
              "raw": 544988,
              "fmt": "544.99k",
              "longFmt": "544,988"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 43253,
              "fmt": "43.25k",
              "longFmt": "43,253"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "Sale at price 22.65 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1574121600,
              "fmt": "2019-11-19"
            },
            "value": {
              "raw": 6217371,
              "fmt": "6.22M",
              "longFmt": "6,217,371"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 274454,
              "fmt": "274.45k",
              "longFmt": "274,454"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "EKMAN LARS G",
            "transactionText": "Sale at price 19.35 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1573516800,
              "fmt": "2019-11-12"
            },
            "value": {
              "raw": 746759,
              "fmt": "746.76k",
              "longFmt": "746,759"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 38600,
              "fmt": "38.6k",
              "longFmt": "38,600"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "EKMAN LARS G",
            "transactionText": "Conversion of Exercise of derivative security at price 14.40 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1573516800,
              "fmt": "2019-11-12"
            },
            "value": {
              "raw": 555840,
              "fmt": "555.84k",
              "longFmt": "555,840"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 38600,
              "fmt": "38.6k",
              "longFmt": "38,600"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "Sale at price 17.05 - 17.88 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1573430400,
              "fmt": "2019-11-11"
            },
            "value": {
              "raw": 8143452,
              "fmt": "8.14M",
              "longFmt": "8,143,452"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 475546,
              "fmt": "475.55k",
              "longFmt": "475,546"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "EKMAN LARS G",
            "transactionText": "Sale at price 18.00 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1572912000,
              "fmt": "2019-11-05"
            },
            "value": {
              "raw": 115230,
              "fmt": "115.23k",
              "longFmt": "115,230"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 6400,
              "fmt": "6.4k",
              "longFmt": "6,400"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "EKMAN LARS G",
            "transactionText": "Conversion of Exercise of derivative security at price 14.40 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1572912000,
              "fmt": "2019-11-05"
            },
            "value": {
              "raw": 92160,
              "fmt": "92.16k",
              "longFmt": "92,160"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 6400,
              "fmt": "6.4k",
              "longFmt": "6,400"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "Conversion of Exercise of derivative security at price 2.50 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1572220800,
              "fmt": "2019-10-28"
            },
            "value": {
              "raw": 29002,
              "fmt": "29k",
              "longFmt": "29,002"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 11601,
              "fmt": "11.6k",
              "longFmt": "11,601"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "Stock Award(Grant) at price 0.00 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1570406400,
              "fmt": "2019-10-07"
            },
            "value": {
              "raw": 0,
              "fmt": null,
              "longFmt": "0"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 1265250,
              "fmt": "1.27M",
              "longFmt": "1,265,250"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "Stock Award(Grant) at price 0.00 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1570406400,
              "fmt": "2019-10-07"
            },
            "value": {
              "raw": 0,
              "fmt": null,
              "longFmt": "0"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 199500,
              "fmt": "199.5k",
              "longFmt": "199,500"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "Stock Award(Grant) at price 0.00 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1570406400,
              "fmt": "2019-10-07"
            },
            "value": {
              "raw": 0,
              "fmt": null,
              "longFmt": "0"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 199500,
              "fmt": "199.5k",
              "longFmt": "199,500"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "Stock Award(Grant) at price 0.00 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1570406400,
              "fmt": "2019-10-07"
            },
            "value": {
              "raw": 0,
              "fmt": null,
              "longFmt": "0"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 199500,
              "fmt": "199.5k",
              "longFmt": "199,500"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "EKMAN LARS G",
            "transactionText": "Sale at price 15.01 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1567468800,
              "fmt": "2019-09-03"
            },
            "value": {
              "raw": 1365722,
              "fmt": "1.37M",
              "longFmt": "1,365,722"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 91016,
              "fmt": "91.02k",
              "longFmt": "91,016"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "EKMAN LARS G",
            "transactionText": "Conversion of Exercise of derivative security at price 3.06 - 5.58 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1567468800,
              "fmt": "2019-09-03"
            },
            "value": {
              "raw": 320280,
              "fmt": "320.28k",
              "longFmt": "320,280"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 91016,
              "fmt": "91.02k",
              "longFmt": "91,016"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "Conversion of Exercise of derivative security at price 1.02 - 2.50 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1566345600,
              "fmt": "2019-08-21"
            },
            "value": {
              "raw": 300010,
              "fmt": "300.01k",
              "longFmt": "300,010"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 191997,
              "fmt": "192k",
              "longFmt": "191,997"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "Conversion of Exercise of derivative security at price 1.02 - 2.50 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1566345600,
              "fmt": "2019-08-21"
            },
            "value": {
              "raw": 271008,
              "fmt": "271.01k",
              "longFmt": "271,008"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 180396,
              "fmt": "180.4k",
              "longFmt": "180,396"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "STACK DAVID M",
            "transactionText": "Sale at price 22.28 per share.",
            "moneyText": "",
            "ownership": "I",
            "startDate": {
              "raw": 1562803200,
              "fmt": "2019-07-11"
            },
            "value": {
              "raw": 1158354,
              "fmt": "1.16M",
              "longFmt": "1,158,354"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 51991,
              "fmt": "51.99k",
              "longFmt": "51,991"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "STACK DAVID M",
            "transactionText": "Conversion of Exercise of derivative security at price 2.19 - 2.50 per share.",
            "moneyText": "",
            "ownership": "I",
            "startDate": {
              "raw": 1562803200,
              "fmt": "2019-07-11"
            },
            "value": {
              "raw": 121035,
              "fmt": "121.03k",
              "longFmt": "121,035"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 51991,
              "fmt": "51.99k",
              "longFmt": "51,991"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "ZAKRZEWSKI JOSEPH S. JR",
            "transactionText": "Sale at price 23.82 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1562284800,
              "fmt": "2019-07-05"
            },
            "value": {
              "raw": 2381580,
              "fmt": "2.38M",
              "longFmt": "2,381,580"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 100000,
              "fmt": "100k",
              "longFmt": "100,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "ZAKRZEWSKI JOSEPH S. JR",
            "transactionText": "Conversion of Exercise of derivative security at price 9.00 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1562284800,
              "fmt": "2019-07-05"
            },
            "value": {
              "raw": 900000,
              "fmt": "900k",
              "longFmt": "900,000"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 100000,
              "fmt": "100k",
              "longFmt": "100,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "Sale at price 20.46 - 22.64 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1562112000,
              "fmt": "2019-07-03"
            },
            "value": {
              "raw": 2104378,
              "fmt": "2.1M",
              "longFmt": "2,104,378"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 98814,
              "fmt": "98.81k",
              "longFmt": "98,814"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "Conversion of Exercise of derivative security at price 2.50 - 2.80 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1562112000,
              "fmt": "2019-07-03"
            },
            "value": {
              "raw": 255810,
              "fmt": "255.81k",
              "longFmt": "255,810"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 98814,
              "fmt": "98.81k",
              "longFmt": "98,814"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "Sale at price 19.52 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1561939200,
              "fmt": "2019-07-01"
            },
            "value": {
              "raw": 1057906,
              "fmt": "1.06M",
              "longFmt": "1,057,906"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 54186,
              "fmt": "54.19k",
              "longFmt": "54,186"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "Conversion of Exercise of derivative security at price 1.40 - 3.80 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1561939200,
              "fmt": "2019-07-01"
            },
            "value": {
              "raw": 69962,
              "fmt": "69.96k",
              "longFmt": "69,962"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 27244,
              "fmt": "27.24k",
              "longFmt": "27,244"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "Sale at price 19.67 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1561939200,
              "fmt": "2019-07-01"
            },
            "value": {
              "raw": 983270,
              "fmt": "983.27k",
              "longFmt": "983,270"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 50000,
              "fmt": "50k",
              "longFmt": "50,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "Conversion of Exercise of derivative security at price 2.19 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1561939200,
              "fmt": "2019-07-01"
            },
            "value": {
              "raw": 109500,
              "fmt": "109.5k",
              "longFmt": "109,500"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 50000,
              "fmt": "50k",
              "longFmt": "50,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1561680000,
              "fmt": "2019-06-28"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 37500,
              "fmt": "37.5k",
              "longFmt": "37,500"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1561680000,
              "fmt": "2019-06-28"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 53437,
              "fmt": "53.44k",
              "longFmt": "53,437"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "Sale at price 18.59 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1561334400,
              "fmt": "2019-06-24"
            },
            "value": {
              "raw": 2104521,
              "fmt": "2.1M",
              "longFmt": "2,104,521"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 113195,
              "fmt": "113.19k",
              "longFmt": "113,195"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "Conversion of Exercise of derivative security at price 2.50 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1561334400,
              "fmt": "2019-06-24"
            },
            "value": {
              "raw": 250000,
              "fmt": "250k",
              "longFmt": "250,000"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 100000,
              "fmt": "100k",
              "longFmt": "100,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "ZAKRZEWSKI JOSEPH S. JR",
            "transactionText": "Sale at price 19.02 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1560816000,
              "fmt": "2019-06-18"
            },
            "value": {
              "raw": 1902090,
              "fmt": "1.9M",
              "longFmt": "1,902,090"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 100000,
              "fmt": "100k",
              "longFmt": "100,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "ZAKRZEWSKI JOSEPH S. JR",
            "transactionText": "Conversion of Exercise of derivative security at price 3.40 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1560816000,
              "fmt": "2019-06-18"
            },
            "value": {
              "raw": 340000,
              "fmt": "340k",
              "longFmt": "340,000"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 100000,
              "fmt": "100k",
              "longFmt": "100,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "Sale at price 17.92 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1559260800,
              "fmt": "2019-05-31"
            },
            "value": {
              "raw": 488058,
              "fmt": "488.06k",
              "longFmt": "488,058"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 27229,
              "fmt": "27.23k",
              "longFmt": "27,229"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "Conversion of Exercise of derivative security at price 1.40 - 3.80 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1559260800,
              "fmt": "2019-05-31"
            },
            "value": {
              "raw": 69925,
              "fmt": "69.92k",
              "longFmt": "69,925"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 27229,
              "fmt": "27.23k",
              "longFmt": "27,229"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "STACK DAVID M",
            "transactionText": "Sale at price 18.45 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1559088000,
              "fmt": "2019-05-29"
            },
            "value": {
              "raw": 623534,
              "fmt": "623.53k",
              "longFmt": "623,534"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 33799,
              "fmt": "33.8k",
              "longFmt": "33,799"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "STACK DAVID M",
            "transactionText": "Conversion of Exercise of derivative security at price 3.21 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1559088000,
              "fmt": "2019-05-29"
            },
            "value": {
              "raw": 108495,
              "fmt": "108.5k",
              "longFmt": "108,495"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 33799,
              "fmt": "33.8k",
              "longFmt": "33,799"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "STACK DAVID M",
            "transactionText": "Sale at price 18.00 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1558051200,
              "fmt": "2019-05-17"
            },
            "value": {
              "raw": 237139,
              "fmt": "237.14k",
              "longFmt": "237,139"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 13174,
              "fmt": "13.17k",
              "longFmt": "13,174"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "STACK DAVID M",
            "transactionText": "Conversion of Exercise of derivative security at price 3.21 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1558051200,
              "fmt": "2019-05-17"
            },
            "value": {
              "raw": 42289,
              "fmt": "42.29k",
              "longFmt": "42,289"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 13174,
              "fmt": "13.17k",
              "longFmt": "13,174"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "BERG AARON D.",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1556668800,
              "fmt": "2019-05-01"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 26000,
              "fmt": "26k",
              "longFmt": "26,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "Sale at price 18.69 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1556582400,
              "fmt": "2019-04-30"
            },
            "value": {
              "raw": 508839,
              "fmt": "508.84k",
              "longFmt": "508,839"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 27230,
              "fmt": "27.23k",
              "longFmt": "27,230"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "Conversion of Exercise of derivative security at price 1.40 - 3.80 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1556582400,
              "fmt": "2019-04-30"
            },
            "value": {
              "raw": 69927,
              "fmt": "69.93k",
              "longFmt": "69,927"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 27230,
              "fmt": "27.23k",
              "longFmt": "27,230"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "Sale at price 18.67 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1556582400,
              "fmt": "2019-04-30"
            },
            "value": {
              "raw": 178107,
              "fmt": "178.11k",
              "longFmt": "178,107"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 9541,
              "fmt": "9.54k",
              "longFmt": "9,541"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "Conversion of Exercise of derivative security at price 1.40 - 2.95 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1556582400,
              "fmt": "2019-04-30"
            },
            "value": {
              "raw": 22235,
              "fmt": "22.23k",
              "longFmt": "22,235"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 9541,
              "fmt": "9.54k",
              "longFmt": "9,541"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "Sale at price 20.03 - 20.80 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1554076800,
              "fmt": "2019-04-01"
            },
            "value": {
              "raw": 1086122,
              "fmt": "1.09M",
              "longFmt": "1,086,122"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 54172,
              "fmt": "54.17k",
              "longFmt": "54,172"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "Conversion of Exercise of derivative security at price 1.40 - 3.80 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1554076800,
              "fmt": "2019-04-01"
            },
            "value": {
              "raw": 69926,
              "fmt": "69.93k",
              "longFmt": "69,926"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 80667,
              "fmt": "80.67k",
              "longFmt": "80,667"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "Sale at price 20.05 - 20.78 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1554076800,
              "fmt": "2019-04-01"
            },
            "value": {
              "raw": 191392,
              "fmt": "191.39k",
              "longFmt": "191,392"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 9542,
              "fmt": "9.54k",
              "longFmt": "9,542"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "Conversion of Exercise of derivative security at price 1.40 - 2.95 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1554076800,
              "fmt": "2019-04-01"
            },
            "value": {
              "raw": 22236,
              "fmt": "22.24k",
              "longFmt": "22,236"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 9542,
              "fmt": "9.54k",
              "longFmt": "9,542"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1553817600,
              "fmt": "2019-03-29"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 37500,
              "fmt": "37.5k",
              "longFmt": "37,500"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "Sale at price 21.15 - 21.70 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1551657600,
              "fmt": "2019-03-04"
            },
            "value": {
              "raw": 5327234,
              "fmt": "5.33M",
              "longFmt": "5,327,234"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 250000,
              "fmt": "250k",
              "longFmt": "250,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "EKMAN LARS G",
            "transactionText": "Sale at price 21.16 - 21.71 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1551657600,
              "fmt": "2019-03-04"
            },
            "value": {
              "raw": 2884905,
              "fmt": "2.88M",
              "longFmt": "2,884,905"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 135377,
              "fmt": "135.38k",
              "longFmt": "135,377"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "EKMAN LARS G",
            "transactionText": "Conversion of Exercise of derivative security at price 1.03 - 2.50 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1551657600,
              "fmt": "2019-03-04"
            },
            "value": {
              "raw": 276743,
              "fmt": "276.74k",
              "longFmt": "276,743"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 135377,
              "fmt": "135.38k",
              "longFmt": "135,377"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "Sale at price 20.67 - 22.64 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1551398400,
              "fmt": "2019-03-01"
            },
            "value": {
              "raw": 1213524,
              "fmt": "1.21M",
              "longFmt": "1,213,524"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 55543,
              "fmt": "55.54k",
              "longFmt": "55,543"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "Conversion of Exercise of derivative security at price 1.40 - 2.95 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1551398400,
              "fmt": "2019-03-01"
            },
            "value": {
              "raw": 137239,
              "fmt": "137.24k",
              "longFmt": "137,239"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 55543,
              "fmt": "55.54k",
              "longFmt": "55,543"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "Sale at price 20.89 - 22.59 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1551398400,
              "fmt": "2019-03-01"
            },
            "value": {
              "raw": 2209098,
              "fmt": "2.21M",
              "longFmt": "2,209,098"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 100000,
              "fmt": "100k",
              "longFmt": "100,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "Conversion of Exercise of derivative security at price 2.19 - 3.80 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1551398400,
              "fmt": "2019-03-01"
            },
            "value": {
              "raw": 283350,
              "fmt": "283.35k",
              "longFmt": "283,350"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 100000,
              "fmt": "100k",
              "longFmt": "100,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "Sale at price 20.72 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1551312000,
              "fmt": "2019-02-28"
            },
            "value": {
              "raw": 564172,
              "fmt": "564.17k",
              "longFmt": "564,172"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 27231,
              "fmt": "27.23k",
              "longFmt": "27,231"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KENNEDY JOSEPH T",
            "transactionText": "Conversion of Exercise of derivative security at price 1.40 - 3.80 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1551312000,
              "fmt": "2019-02-28"
            },
            "value": {
              "raw": 69928,
              "fmt": "69.93k",
              "longFmt": "69,928"
            },
            "filerRelation": "General Counsel",
            "shares": {
              "raw": 27231,
              "fmt": "27.23k",
              "longFmt": "27,231"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "VAN HEEK G JAN",
            "transactionText": "Sale at price 20.02 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1550793600,
              "fmt": "2019-02-22"
            },
            "value": {
              "raw": 4071423,
              "fmt": "4.07M",
              "longFmt": "4,071,423"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 203382,
              "fmt": "203.38k",
              "longFmt": "203,382"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "VAN HEEK G JAN",
            "transactionText": "Conversion of Exercise of derivative security at price 1.03 - 14.40 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1550793600,
              "fmt": "2019-02-22"
            },
            "value": {
              "raw": 857170,
              "fmt": "857.17k",
              "longFmt": "857,170"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 192347,
              "fmt": "192.35k",
              "longFmt": "192,347"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "ZAKRZEWSKI JOSEPH S. JR",
            "transactionText": "Sale at price 19.90 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1550793600,
              "fmt": "2019-02-22"
            },
            "value": {
              "raw": 3594438,
              "fmt": "3.59M",
              "longFmt": "3,594,438"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 180625,
              "fmt": "180.62k",
              "longFmt": "180,625"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "ZAKRZEWSKI JOSEPH S. JR",
            "transactionText": "Conversion of Exercise of derivative security at price 8.10 - 8.86 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1550793600,
              "fmt": "2019-02-22"
            },
            "value": {
              "raw": 1572312,
              "fmt": "1.57M",
              "longFmt": "1,572,312"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 180625,
              "fmt": "180.62k",
              "longFmt": "180,625"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "PETERSON KRISTINE",
            "transactionText": "Sale at price 20.02 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1550793600,
              "fmt": "2019-02-22"
            },
            "value": {
              "raw": 3274038,
              "fmt": "3.27M",
              "longFmt": "3,274,038"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 163500,
              "fmt": "163.5k",
              "longFmt": "163,500"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "PETERSON KRISTINE",
            "transactionText": "Conversion of Exercise of derivative security at price 3.67 - 14.40 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1550793600,
              "fmt": "2019-02-22"
            },
            "value": {
              "raw": 947730,
              "fmt": "947.73k",
              "longFmt": "947,730"
            },
            "filerRelation": "Director",
            "shares": {
              "raw": 163500,
              "fmt": "163.5k",
              "longFmt": "163,500"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "Sale at price 18.97 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1550793600,
              "fmt": "2019-02-22"
            },
            "value": {
              "raw": 948500,
              "fmt": "948.5k",
              "longFmt": "948,500"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 50000,
              "fmt": "50k",
              "longFmt": "50,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "Conversion of Exercise of derivative security at price 2.95 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1550793600,
              "fmt": "2019-02-22"
            },
            "value": {
              "raw": 147500,
              "fmt": "147.5k",
              "longFmt": "147,500"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 50000,
              "fmt": "50k",
              "longFmt": "50,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KALB MICHAEL WAYNE",
            "transactionText": "Sale at price 17.58 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1550188800,
              "fmt": "2019-02-15"
            },
            "value": {
              "raw": 439525,
              "fmt": "439.52k",
              "longFmt": "439,525"
            },
            "filerRelation": "Chief Financial Officer",
            "shares": {
              "raw": 25000,
              "fmt": "25k",
              "longFmt": "25,000"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "KETCHUM STEVEN B",
            "transactionText": "Sale at price 17.06 - 17.49 per share.",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1548979200,
              "fmt": "2019-02-01"
            },
            "value": {
              "raw": 644373,
              "fmt": "644.37k",
              "longFmt": "644,373"
            },
            "filerRelation": "Officer",
            "shares": {
              "raw": 37530,
              "fmt": "37.53k",
              "longFmt": "37,530"
            },
            "filerUrl": "",
            "maxAge": 1
          },
          {
            "filerName": "THERO JOHN F",
            "transactionText": "",
            "moneyText": "",
            "ownership": "D",
            "startDate": {
              "raw": 1548892800,
              "fmt": "2019-01-31"
            },
            "filerRelation": "Chief Executive Officer",
            "shares": {
              "raw": 363334,
              "fmt": "363.33k",
              "longFmt": "363,334"
            },
            "filerUrl": "",
            "maxAge": 1
          }
        ],
        "maxAge": 1
      },
      "insiderHolders": {
        "holders": [
          {
            "maxAge": 1,
            "name": "BERG AARON D.",
            "relation": "Officer",
            "url": "",
            "transactionDescription": "Conversion of Exercise of derivative security",
            "latestTransDate": {
              "raw": 1611878400,
              "fmt": "2021-01-29"
            },
            "positionDirect": {
              "raw": 227100,
              "fmt": "227.1k",
              "longFmt": "227,100"
            },
            "positionDirectDate": {
              "raw": 1611878400,
              "fmt": "2021-01-29"
            }
          },
          {
            "maxAge": 1,
            "name": "EKMAN LARS G",
            "relation": "Director",
            "url": "",
            "transactionDescription": "Sale",
            "latestTransDate": {
              "raw": 1573516800,
              "fmt": "2019-11-12"
            },
            "positionDirectDate": {
              "raw": 1573516800,
              "fmt": "2019-11-12"
            }
          },
          {
            "maxAge": 1,
            "name": "KALB MICHAEL WAYNE",
            "relation": "Chief Financial Officer",
            "url": "",
            "transactionDescription": "Conversion of Exercise of derivative security",
            "latestTransDate": {
              "raw": 1611878400,
              "fmt": "2021-01-29"
            },
            "positionDirect": {
              "raw": 130310,
              "fmt": "130.31k",
              "longFmt": "130,310"
            },
            "positionDirectDate": {
              "raw": 1611878400,
              "fmt": "2021-01-29"
            }
          },
          {
            "maxAge": 1,
            "name": "KENNEDY JOSEPH T",
            "relation": "General Counsel",
            "url": "",
            "transactionDescription": "Conversion of Exercise of derivative security",
            "latestTransDate": {
              "raw": 1611878400,
              "fmt": "2021-01-29"
            },
            "positionDirect": {
              "raw": 240033,
              "fmt": "240.03k",
              "longFmt": "240,033"
            },
            "positionDirectDate": {
              "raw": 1611878400,
              "fmt": "2021-01-29"
            }
          },
          {
            "maxAge": 1,
            "name": "KETCHUM STEVEN B",
            "relation": "Officer",
            "url": "",
            "transactionDescription": "Sale",
            "latestTransDate": {
              "raw": 1612137600,
              "fmt": "2021-02-01"
            },
            "positionDirect": {
              "raw": 382996,
              "fmt": "383k",
              "longFmt": "382,996"
            },
            "positionDirectDate": {
              "raw": 1612137600,
              "fmt": "2021-02-01"
            }
          },
          {
            "maxAge": 1,
            "name": "PETERSON KRISTINE",
            "relation": "Director",
            "url": "",
            "transactionDescription": "Sale",
            "latestTransDate": {
              "raw": 1550793600,
              "fmt": "2019-02-22"
            },
            "positionDirectDate": {
              "raw": 1550793600,
              "fmt": "2019-02-22"
            }
          },
          {
            "maxAge": 1,
            "name": "STACK DAVID M",
            "relation": "Director",
            "url": "",
            "transactionDescription": "Sale",
            "latestTransDate": {
              "raw": 1562803200,
              "fmt": "2019-07-11"
            },
            "positionIndirectDate": {
              "raw": 1562803200,
              "fmt": "2019-07-11"
            }
          },
          {
            "maxAge": 1,
            "name": "THERO JOHN F",
            "relation": "Chief Executive Officer",
            "url": "",
            "transactionDescription": "Conversion of Exercise of derivative security",
            "latestTransDate": {
              "raw": 1611878400,
              "fmt": "2021-01-29"
            },
            "positionDirect": {
              "raw": 2881750,
              "fmt": "2.88M",
              "longFmt": "2,881,750"
            },
            "positionDirectDate": {
              "raw": 1611878400,
              "fmt": "2021-01-29"
            }
          },
          {
            "maxAge": 1,
            "name": "VAN HEEK G JAN",
            "relation": "Director",
            "url": "",
            "transactionDescription": "Sale",
            "latestTransDate": {
              "raw": 1550793600,
              "fmt": "2019-02-22"
            },
            "positionDirect": {
              "raw": 14168,
              "fmt": "14.17k",
              "longFmt": "14,168"
            },
            "positionDirectDate": {
              "raw": 1550793600,
              "fmt": "2019-02-22"
            }
          },
          {
            "maxAge": 1,
            "name": "ZAKRZEWSKI JOSEPH S. JR",
            "relation": "Director",
            "url": "",
            "transactionDescription": "Conversion of Exercise of derivative security",
            "latestTransDate": {
              "raw": 1603238400,
              "fmt": "2020-10-21"
            },
            "positionDirect": {
              "raw": 184547,
              "fmt": "184.55k",
              "longFmt": "184,547"
            },
            "positionDirectDate": {
              "raw": 1603238400,
              "fmt": "2020-10-21"
            }
          }
        ],
        "maxAge": 1
      },
      "netSharePurchaseActivity": {
        "period": "6m",
        "netPercentInsiderShares": {
          "raw": 0.175,
          "fmt": "17.50%"
        },
        "netInfoCount": {
          "raw": 32,
          "fmt": "32",
          "longFmt": "32"
        },
        "totalInsiderShares": {
          "raw": 4070190,
          "fmt": "4.07M",
          "longFmt": "4,070,190"
        },
        "buyInfoShares": {
          "raw": 1811902,
          "fmt": "1.81M",
          "longFmt": "1,811,902"
        },
        "buyPercentInsiderShares": {
          "raw": 0.523,
          "fmt": "52.30%"
        },
        "sellInfoCount": {
          "raw": 3,
          "fmt": "3",
          "longFmt": "3"
        },
        "sellInfoShares": {
          "raw": 1206762,
          "fmt": "1.21M",
          "longFmt": "1,206,762"
        },
        "sellPercentInsiderShares": {
          "raw": 0.348,
          "fmt": "34.80%"
        },
        "maxAge": 1,
        "buyInfoCount": {
          "raw": 29,
          "fmt": "29",
          "longFmt": "29"
        },
        "netInfoShares": {
          "raw": 605140,
          "fmt": "605.14k",
          "longFmt": "605,140"
        }
      },
      "majorHoldersBreakdown": {
        "maxAge": 1,
        "insidersPercentHeld": {
          "raw": 0.0103400005,
          "fmt": "1.03%"
        },
        "institutionsPercentHeld": {
          "raw": 0.39435002,
          "fmt": "39.44%"
        },
        "institutionsFloatPercentHeld": {
          "raw": 0.39847,
          "fmt": "39.85%"
        },
        "institutionsCount": {
          "raw": 319,
          "fmt": "319",
          "longFmt": "319"
        }
      },
      "financialData": {
        "ebitdaMargins": {
          "raw": -0.02869,
          "fmt": "-2.87%"
        },
        "profitMargins": {
          "raw": -0.02931,
          "fmt": "-2.93%"
        },
        "grossMargins": {
          "raw": 0.78594,
          "fmt": "78.59%"
        },
        "operatingCashflow": {
          "raw": -21746000,
          "fmt": "-21.75M",
          "longFmt": "-21,746,000"
        },
        "revenueGrowth": {
          "raw": 0.167,
          "fmt": "16.70%"
        },
        "operatingMargins": {
          "raw": -0.03201,
          "fmt": "-3.20%"
        },
        "ebitda": {
          "raw": -17617000,
          "fmt": "-17.62M",
          "longFmt": "-17,617,000"
        },
        "targetLowPrice": {
          "raw": 6,
          "fmt": "6.00"
        },
        "recommendationKey": "buy",
        "grossProfits": {
          "raw": 482616000,
          "fmt": "482.62M",
          "longFmt": "482,616,000"
        },
        "freeCashflow": {
          "raw": -19711500,
          "fmt": "-19.71M",
          "longFmt": "-19,711,500"
        },
        "targetMedianPrice": {
          "raw": 10,
          "fmt": "10.00"
        },
        "currentPrice": {
          "raw": 6.03,
          "fmt": "6.03"
        },
        "earningsGrowth": {
          "raw": -0.424,
          "fmt": "-42.40%"
        },
        "currentRatio": {
          "raw": 2.86,
          "fmt": "2.86"
        },
        "returnOnAssets": {
          "raw": -0.01329,
          "fmt": "-1.33%"
        },
        "numberOfAnalystOpinions": {
          "raw": 11,
          "fmt": "11",
          "longFmt": "11"
        },
        "targetMeanPrice": {
          "raw": 10.64,
          "fmt": "10.64"
        },
        "debtToEquity": {
          "raw": 1.694,
          "fmt": "1.69"
        },
        "returnOnEquity": {
          "raw": -0.02913,
          "fmt": "-2.91%"
        },
        "targetHighPrice": {
          "raw": 19,
          "fmt": "19.00"
        },
        "totalCash": {
          "raw": 500932992,
          "fmt": "500.93M",
          "longFmt": "500,932,992"
        },
        "totalDebt": {
          "raw": 10628000,
          "fmt": "10.63M",
          "longFmt": "10,628,000"
        },
        "totalRevenue": {
          "raw": 614060032,
          "fmt": "614.06M",
          "longFmt": "614,060,032"
        },
        "totalCashPerShare": {
          "raw": 1.273,
          "fmt": "1.27"
        },
        "financialCurrency": "USD",
        "maxAge": 86400,
        "revenuePerShare": {
          "raw": 1.609,
          "fmt": "1.61"
        },
        "quickRatio": {
          "raw": 2.132,
          "fmt": "2.13"
        },
        "recommendationMean": {
          "raw": 2.2,
          "fmt": "2.20"
        }
      },
      "quoteType": {
        "exchange": "NMS",
        "shortName": "Amarin Corporation plc",
        "longName": "Amarin Corporation plc",
        "exchangeTimezoneName": "America/New_York",
        "exchangeTimezoneShortName": "EST",
        "isEsgPopulated": false,
        "gmtOffSetMilliseconds": "-18000000",
        "quoteType": "EQUITY",
        "symbol": "AMRN",
        "messageBoardId": "finmb_407863",
        "market": "us_market"
      },
      "institutionOwnership": {
        "maxAge": 1,
        "ownershipList": [
          {
            "maxAge": 1,
            "reportDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "organization": "Baker Brothers Advisors, LLC",
            "pctHeld": {
              "raw": 0.072,
              "fmt": "7.20%"
            },
            "position": {
              "raw": 27991761,
              "fmt": "27.99M",
              "longFmt": "27,991,761"
            },
            "value": {
              "raw": 136879711,
              "fmt": "136.88M",
              "longFmt": "136,879,711"
            }
          },
          {
            "maxAge": 1,
            "reportDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "organization": "Eversept Partners, LP",
            "pctHeld": {
              "raw": 0.040999997,
              "fmt": "4.10%"
            },
            "position": {
              "raw": 15943915,
              "fmt": "15.94M",
              "longFmt": "15,943,915"
            },
            "value": {
              "raw": 77965744,
              "fmt": "77.97M",
              "longFmt": "77,965,744"
            }
          },
          {
            "maxAge": 1,
            "reportDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "organization": "BVF Inc.",
            "pctHeld": {
              "raw": 0.0277,
              "fmt": "2.77%"
            },
            "position": {
              "raw": 10761593,
              "fmt": "10.76M",
              "longFmt": "10,761,593"
            },
            "value": {
              "raw": 52624189,
              "fmt": "52.62M",
              "longFmt": "52,624,189"
            }
          },
          {
            "maxAge": 1,
            "reportDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "organization": "Boxer Capital, LLC",
            "pctHeld": {
              "raw": 0.0154,
              "fmt": "1.54%"
            },
            "position": {
              "raw": 6000000,
              "fmt": "6M",
              "longFmt": "6,000,000"
            },
            "value": {
              "raw": 29340000,
              "fmt": "29.34M",
              "longFmt": "29,340,000"
            }
          },
          {
            "maxAge": 1,
            "reportDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "organization": "Avoro Capital Advisors LLC",
            "pctHeld": {
              "raw": 0.0154,
              "fmt": "1.54%"
            },
            "position": {
              "raw": 6000000,
              "fmt": "6M",
              "longFmt": "6,000,000"
            },
            "value": {
              "raw": 29340000,
              "fmt": "29.34M",
              "longFmt": "29,340,000"
            }
          },
          {
            "maxAge": 1,
            "reportDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "organization": "Morgan Stanley",
            "pctHeld": {
              "raw": 0.0154,
              "fmt": "1.54%"
            },
            "position": {
              "raw": 5975266,
              "fmt": "5.98M",
              "longFmt": "5,975,266"
            },
            "value": {
              "raw": 29219050,
              "fmt": "29.22M",
              "longFmt": "29,219,050"
            }
          },
          {
            "maxAge": 1,
            "reportDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "organization": "Grosvenor Holdings, L.L.C.",
            "pctHeld": {
              "raw": 0.0152,
              "fmt": "1.52%"
            },
            "position": {
              "raw": 5890071,
              "fmt": "5.89M",
              "longFmt": "5,890,071"
            },
            "value": {
              "raw": 28802447,
              "fmt": "28.8M",
              "longFmt": "28,802,447"
            }
          },
          {
            "maxAge": 1,
            "reportDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "organization": "SCP Investment, LP",
            "pctHeld": {
              "raw": 0.014199999,
              "fmt": "1.42%"
            },
            "position": {
              "raw": 5500000,
              "fmt": "5.5M",
              "longFmt": "5,500,000"
            },
            "value": {
              "raw": 26895000,
              "fmt": "26.89M",
              "longFmt": "26,895,000"
            }
          },
          {
            "maxAge": 1,
            "reportDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "organization": "ClearBridge Investments, LLC",
            "pctHeld": {
              "raw": 0.0119,
              "fmt": "1.19%"
            },
            "position": {
              "raw": 4625120,
              "fmt": "4.63M",
              "longFmt": "4,625,120"
            },
            "value": {
              "raw": 22616836,
              "fmt": "22.62M",
              "longFmt": "22,616,836"
            }
          },
          {
            "maxAge": 1,
            "reportDate": {
              "raw": 1609372800,
              "fmt": "2020-12-31"
            },
            "organization": "Rock Springs Capital Management, LP",
            "pctHeld": {
              "raw": 0.0108,
              "fmt": "1.08%"
            },
            "position": {
              "raw": 4195000,
              "fmt": "4.2M",
              "longFmt": "4,195,000"
            },
            "value": {
              "raw": 20513550,
              "fmt": "20.51M",
              "longFmt": "20,513,550"
            }
          }
        ]
      },
      "calendarEvents": {
        "maxAge": 1,
        "earnings": {
          "earningsDate": [
            {
              "raw": 1614211200,
              "fmt": "2021-02-25"
            }
          ],
          "earningsAverage": {
            "raw": -0.05,
            "fmt": "-0.05"
          },
          "earningsLow": {
            "raw": -0.12,
            "fmt": "-0.12"
          },
          "earningsHigh": {
            "raw": -0.01,
            "fmt": "-0.01"
          },
          "revenueAverage": {
            "raw": 150950000,
            "fmt": "150.95M",
            "longFmt": "150,950,000"
          },
          "revenueLow": {
            "raw": 140000000,
            "fmt": "140M",
            "longFmt": "140,000,000"
          },
          "revenueHigh": {
            "raw": 161250000,
            "fmt": "161.25M",
            "longFmt": "161,250,000"
          }
        },
        "exDividendDate": {},
        "dividendDate": {}
      },
      "summaryDetail": {
        "previousClose": {
          "raw": 6.19,
          "fmt": "6.19"
        },
        "regularMarketOpen": {
          "raw": 6.17,
          "fmt": "6.17"
        },
        "twoHundredDayAverage": {
          "raw": 5.570368,
          "fmt": "5.57"
        },
        "trailingAnnualDividendYield": {},
        "payoutRatio": {
          "raw": 0,
          "fmt": "0.00%"
        },
        "volume24Hr": {},
        "regularMarketDayHigh": {
          "raw": 6.28,
          "fmt": "6.28"
        },
        "navPrice": {},
        "averageDailyVolume10Day": {
          "raw": 7208950,
          "fmt": "7.21M",
          "longFmt": "7,208,950"
        },
        "totalAssets": {},
        "regularMarketPreviousClose": {
          "raw": 6.19,
          "fmt": "6.19"
        },
        "fiftyDayAverage": {
          "raw": 7.272647,
          "fmt": "7.27"
        },
        "trailingAnnualDividendRate": {},
        "open": {
          "raw": 6.17,
          "fmt": "6.17"
        },
        "toCurrency": null,
        "averageVolume10days": {
          "raw": 7208950,
          "fmt": "7.21M",
          "longFmt": "7,208,950"
        },
        "expireDate": {},
        "yield": {},
        "algorithm": null,
        "dividendRate": {},
        "exDividendDate": {},
        "beta": {
          "raw": 2.377801,
          "fmt": "2.38"
        },
        "circulatingSupply": {},
        "startDate": {},
        "regularMarketDayLow": {
          "raw": 5.94,
          "fmt": "5.94"
        },
        "priceHint": {
          "raw": 2,
          "fmt": "2",
          "longFmt": "2"
        },
        "currency": "USD",
        "regularMarketVolume": {
          "raw": 11547574,
          "fmt": "11.55M",
          "longFmt": "11,547,574"
        },
        "lastMarket": null,
        "maxSupply": {},
        "openInterest": {},
        "marketCap": {
          "raw": 2373619200,
          "fmt": "2.37B",
          "longFmt": "2,373,619,200"
        },
        "volumeAllCurrencies": {},
        "strikePrice": {},
        "averageVolume": {
          "raw": 7947921,
          "fmt": "7.95M",
          "longFmt": "7,947,921"
        },
        "priceToSalesTrailing12Months": {
          "raw": 3.8654513,
          "fmt": "3.87"
        },
        "dayLow": {
          "raw": 5.94,
          "fmt": "5.94"
        },
        "ask": {
          "raw": 6.06,
          "fmt": "6.06"
        },
        "ytdReturn": {},
        "askSize": {
          "raw": 4000,
          "fmt": "4k",
          "longFmt": "4,000"
        },
        "volume": {
          "raw": 11547574,
          "fmt": "11.55M",
          "longFmt": "11,547,574"
        },
        "fiftyTwoWeekHigh": {
          "raw": 16.47,
          "fmt": "16.47"
        },
        "forwardPE": {
          "raw": 54.818184,
          "fmt": "54.82"
        },
        "maxAge": 1,
        "fromCurrency": null,
        "fiveYearAvgDividendYield": {},
        "fiftyTwoWeekLow": {
          "raw": 3.36,
          "fmt": "3.36"
        },
        "bid": {
          "raw": 6.05,
          "fmt": "6.05"
        },
        "tradeable": false,
        "dividendYield": {},
        "bidSize": {
          "raw": 3100,
          "fmt": "3.1k",
          "longFmt": "3,100"
        },
        "dayHigh": {
          "raw": 6.28,
          "fmt": "6.28"
        }
      },
      "symbol": "AMRN",
      "esgScores": {},
      "upgradeDowngradeHistory": {
        "history": [
          {
            "epochGradeDate": 1612177851,
            "firm": "SVB Leerink",
            "toGrade": "Outperform",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1601287572,
            "firm": "SVB Leerink",
            "toGrade": "Outperform",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1598437603,
            "firm": "Piper Sandler",
            "toGrade": "Overweight",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1588598646,
            "firm": "Aegis Capital",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1588587999,
            "firm": "SVB Leerink",
            "toGrade": "Outperform",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1585817240,
            "firm": "Citigroup",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1585676067,
            "firm": "Stifel",
            "toGrade": "Hold",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1585659104,
            "firm": "Oppenheimer",
            "toGrade": "Perform",
            "fromGrade": "Underperform",
            "action": "up"
          },
          {
            "epochGradeDate": 1585654209,
            "firm": "Goldman Sachs",
            "toGrade": "Neutral",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1585646345,
            "firm": "Jefferies",
            "toGrade": "Hold",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1584092487,
            "firm": "Goldman Sachs",
            "toGrade": "Buy",
            "fromGrade": "Neutral",
            "action": "up"
          },
          {
            "epochGradeDate": 1583163508,
            "firm": "Cowen & Co.",
            "toGrade": "Outperform",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1582722640,
            "firm": "Oppenheimer",
            "toGrade": "Underperform",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1582022781,
            "firm": "Citigroup",
            "toGrade": "Buy",
            "fromGrade": "Neutral",
            "action": "up"
          },
          {
            "epochGradeDate": 1578311412,
            "firm": "JP Morgan",
            "toGrade": "Neutral",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1576523549,
            "firm": "Oppenheimer",
            "toGrade": "Underperform",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1576508203,
            "firm": "Stifel",
            "toGrade": "Hold",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1574249344,
            "firm": "Oppenheimer",
            "toGrade": "Underperform",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1574080080,
            "firm": "Citi",
            "toGrade": "Neutral",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1572529285,
            "firm": "Aegis Capital",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1571135970,
            "firm": "Goldman Sachs",
            "toGrade": "Neutral",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1553251840,
            "firm": "Stifel Nicolaus",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1541164830,
            "firm": "Citigroup",
            "toGrade": "Neutral",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1539777585,
            "firm": "Citigroup",
            "toGrade": "Buy",
            "fromGrade": "Buy",
            "action": "main"
          },
          {
            "epochGradeDate": 1537881077,
            "firm": "Jefferies",
            "toGrade": "Buy",
            "fromGrade": "Buy",
            "action": "main"
          },
          {
            "epochGradeDate": 1537874265,
            "firm": "Citigroup",
            "toGrade": "Buy",
            "fromGrade": "Buy",
            "action": "main"
          },
          {
            "epochGradeDate": 1476872074,
            "firm": "Citigroup",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1475656296,
            "firm": "Cantor Fitzgerald",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1475615452,
            "firm": "Cantor Fitzgerald",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1463037254,
            "firm": "Jefferies",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1426145107,
            "firm": "H.C. Wainwright",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "up"
          },
          {
            "epochGradeDate": 1424252755,
            "firm": "SunTrust Robinson Humphrey",
            "toGrade": "Buy",
            "fromGrade": "Neutral",
            "action": "up"
          },
          {
            "epochGradeDate": 1415610000,
            "firm": "Citigroup",
            "toGrade": "Neutral",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1399879752,
            "firm": "Citigroup",
            "toGrade": "Neutral",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1393584370,
            "firm": "Aegis Capital",
            "toGrade": "Hold",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1389888000,
            "firm": "MKM Partners",
            "toGrade": "Neutral",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1383893330,
            "firm": "Citigroup",
            "toGrade": "Buy",
            "fromGrade": "Neutral",
            "action": "up"
          },
          {
            "epochGradeDate": 1383148800,
            "firm": "Leerink Swann",
            "toGrade": "Market Perform",
            "fromGrade": "Outperform",
            "action": "down"
          },
          {
            "epochGradeDate": 1383130853,
            "firm": "Aegis Capital",
            "toGrade": "Hold",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1382080843,
            "firm": "Citigroup",
            "toGrade": "Neutral",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1382025600,
            "firm": "H.C. Wainwright",
            "toGrade": "Neutral",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1381991178,
            "firm": "JP Morgan",
            "toGrade": "Neutral",
            "fromGrade": "Overweight",
            "action": "down"
          },
          {
            "epochGradeDate": 1381990292,
            "firm": "Canaccord Genuity",
            "toGrade": "Hold",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1381943909,
            "firm": "Leerink Swann",
            "toGrade": "Outperform",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1381939883,
            "firm": "Aegis Capital",
            "toGrade": "Hold",
            "fromGrade": "Buy",
            "action": "down"
          },
          {
            "epochGradeDate": 1379401701,
            "firm": "Goldman Sachs",
            "toGrade": "Neutral",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1378280947,
            "firm": "SunTrust Robinson Humphrey",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1376378215,
            "firm": "H.C. Wainwright",
            "toGrade": "Buy",
            "fromGrade": "Neutral",
            "action": "up"
          },
          {
            "epochGradeDate": 1371744899,
            "firm": "Oppenheimer",
            "toGrade": "Perform",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1362380154,
            "firm": "Citigroup",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1357633828,
            "firm": "Jefferies",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1354895044,
            "firm": "Aegis Capital",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1354877921,
            "firm": "Canaccord Genuity",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1354876024,
            "firm": "UBS",
            "toGrade": "Neutral",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1353999120,
            "firm": "Citigroup",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1350293700,
            "firm": "JP Morgan",
            "toGrade": "Overweight",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1349248560,
            "firm": "Wedbush",
            "toGrade": "Neutral",
            "fromGrade": "Outperform",
            "action": "down"
          },
          {
            "epochGradeDate": 1344507000,
            "firm": "Canaccord Genuity",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "main"
          },
          {
            "epochGradeDate": 1341812820,
            "firm": "Aegis Capital",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "init"
          },
          {
            "epochGradeDate": 1340780880,
            "firm": "Jefferies",
            "toGrade": "Buy",
            "fromGrade": "",
            "action": "main"
          }
        ],
        "maxAge": 86400
      },
      "pageViews": {
        "shortTermTrend": "UP",
        "midTermTrend": "UP",
        "longTermTrend": "UP",
        "maxAge": 1
      }
    }
     */
    #endregion

    #region Stock Profile JSON Object
    public partial class StockProfile
    {
        [JsonProperty("financialsTemplate")]
        public FinancialsTemplate FinancialsTemplate { get; set; }

        [JsonProperty("price")]
        public PriceProfile Price { get; set; }

        [JsonProperty("secFilings")]
        public SecFilings SecFilings { get; set; }

        [JsonProperty("quoteType")]
        public QuoteTypeProfile QuoteType { get; set; }

        [JsonProperty("calendarEvents")]
        public CalendarEventsProfile CalendarEvents { get; set; }

        [JsonProperty("summaryDetail")]
        public SummaryDetailProfile SummaryDetail { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("assetProfile")]
        public AssetProfile AssetProfile { get; set; }

        [JsonProperty("pageViews")]
        public PageViews PageViews { get; set; }
    }

    public partial class AssetProfile
    {
        [JsonProperty("zip")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Zip { get; set; }

        [JsonProperty("sector")]
        public string Sector { get; set; }

        [JsonProperty("fullTimeEmployees")]
        public long FullTimeEmployees { get; set; }

        [JsonProperty("compensationRisk")]
        public long CompensationRisk { get; set; }

        [JsonProperty("auditRisk")]
        public long AuditRisk { get; set; }

        [JsonProperty("longBusinessSummary")]
        public string LongBusinessSummary { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("shareHolderRightsRisk")]
        public long ShareHolderRightsRisk { get; set; }

        [JsonProperty("compensationAsOfEpochDate")]
        public long CompensationAsOfEpochDate { get; set; }

        [JsonProperty("governanceEpochDate")]
        public long GovernanceEpochDate { get; set; }

        [JsonProperty("boardRisk")]
        public long BoardRisk { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("companyOfficers")]
        public List<CompanyOfficer> CompanyOfficers { get; set; }

        [JsonProperty("website")]
        public Uri Website { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("overallRisk")]
        public long OverallRisk { get; set; }

        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("industry")]
        public string Industry { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }
    }

    public partial class CompanyOfficer
    {
        [JsonProperty("totalPay", NullValueHandling = NullValueHandling.Ignore)]
        public AverageDailyVolume10Day TotalPay { get; set; }

        [JsonProperty("exercisedValue")]
        public AverageDailyVolume10Day ExercisedValue { get; set; }

        [JsonProperty("yearBorn", NullValueHandling = NullValueHandling.Ignore)]
        public long? YearBorn { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("fiscalYear", NullValueHandling = NullValueHandling.Ignore)]
        public long? FiscalYear { get; set; }

        [JsonProperty("unexercisedValue")]
        public AverageDailyVolume10Day UnexercisedValue { get; set; }

        [JsonProperty("age", NullValueHandling = NullValueHandling.Ignore)]
        public long? Age { get; set; }
    }

    public partial class AverageDailyVolume10Day
    {
        [JsonProperty("raw")]
        public long Raw { get; set; }

        [JsonProperty("fmt")]
        public string Fmt { get; set; }

        [JsonProperty("longFmt")]
        public string LongFmt { get; set; }
    }

    public partial class CalendarEventsProfile
    {
        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("earnings")]
        public Earnings Earnings { get; set; }

        [JsonProperty("exDividendDate")]
        public DividendDate ExDividendDate { get; set; }

        [JsonProperty("dividendDate")]
        public DividendDate DividendDate { get; set; }
    }

    public partial class DividendDate
    {
    }

    public partial class Earnings
    {
        [JsonProperty("earningsDate")]
        public List<PostMarketChange> EarningsDate { get; set; }

        [JsonProperty("earningsAverage")]
        public PostMarketChange EarningsAverage { get; set; }

        [JsonProperty("earningsLow")]
        public PostMarketChange EarningsLow { get; set; }

        [JsonProperty("earningsHigh")]
        public PostMarketChange EarningsHigh { get; set; }

        [JsonProperty("revenueAverage")]
        public AverageDailyVolume10Day RevenueAverage { get; set; }

        [JsonProperty("revenueLow")]
        public AverageDailyVolume10Day RevenueLow { get; set; }

        [JsonProperty("revenueHigh")]
        public AverageDailyVolume10Day RevenueHigh { get; set; }
    }

    public partial class PostMarketChange
    {
        [JsonProperty("raw")]
        public double Raw { get; set; }

        [JsonProperty("fmt")]
        public string Fmt { get; set; }
    }

    public partial class PriceProfile
    {
        [JsonProperty("quoteSourceName")]
        public string QuoteSourceName { get; set; }

        [JsonProperty("regularMarketOpen")]
        public PostMarketChange RegularMarketOpen { get; set; }

        [JsonProperty("averageDailyVolume3Month")]
        public AverageDailyVolume10Day AverageDailyVolume3Month { get; set; }

        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("regularMarketTime")]
        public long RegularMarketTime { get; set; }

        [JsonProperty("volume24Hr")]
        public DividendDate Volume24Hr { get; set; }

        [JsonProperty("regularMarketDayHigh")]
        public PostMarketChange RegularMarketDayHigh { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("averageDailyVolume10Day")]
        public AverageDailyVolume10Day AverageDailyVolume10Day { get; set; }

        [JsonProperty("longName")]
        public string LongName { get; set; }

        [JsonProperty("regularMarketChange")]
        public PostMarketChange RegularMarketChange { get; set; }

        [JsonProperty("currencySymbol")]
        public string CurrencySymbol { get; set; }

        [JsonProperty("regularMarketPreviousClose")]
        public PostMarketChange RegularMarketPreviousClose { get; set; }

        [JsonProperty("postMarketTime")]
        public long PostMarketTime { get; set; }

        [JsonProperty("preMarketPrice")]
        public PostMarketChange PreMarketPrice { get; set; }

        [JsonProperty("preMarketTime")]
        public long PreMarketTime { get; set; }

        [JsonProperty("exchangeDataDelayedBy")]
        public long ExchangeDataDelayedBy { get; set; }

        [JsonProperty("toCurrency")]
        public dynamic ToCurrency { get; set; }

        [JsonProperty("postMarketChange")]
        public PostMarketChange PostMarketChange { get; set; }

        [JsonProperty("postMarketPrice")]
        public PostMarketChange PostMarketPrice { get; set; }

        [JsonProperty("exchangeName")]
        public string ExchangeName { get; set; }

        [JsonProperty("preMarketChange")]
        public PostMarketChange PreMarketChange { get; set; }

        [JsonProperty("circulatingSupply")]
        public DividendDate CirculatingSupply { get; set; }

        [JsonProperty("regularMarketDayLow")]
        public PostMarketChange RegularMarketDayLow { get; set; }

        [JsonProperty("priceHint")]
        public AverageDailyVolume10Day PriceHint { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("regularMarketPrice")]
        public PostMarketChange RegularMarketPrice { get; set; }

        [JsonProperty("regularMarketVolume")]
        public AverageDailyVolume10Day RegularMarketVolume { get; set; }

        [JsonProperty("lastMarket")]
        public dynamic LastMarket { get; set; }

        [JsonProperty("regularMarketSource")]
        public string RegularMarketSource { get; set; }

        [JsonProperty("openInterest")]
        public DividendDate OpenInterest { get; set; }

        [JsonProperty("marketState")]
        public string MarketState { get; set; }

        [JsonProperty("underlyingSymbol")]
        public dynamic UnderlyingSymbol { get; set; }

        [JsonProperty("marketCap")]
        public AverageDailyVolume10Day MarketCap { get; set; }

        [JsonProperty("quoteType")]
        public string QuoteType { get; set; }

        [JsonProperty("preMarketChangePercent")]
        public PostMarketChange PreMarketChangePercent { get; set; }

        [JsonProperty("volumeAllCurrencies")]
        public DividendDate VolumeAllCurrencies { get; set; }

        [JsonProperty("postMarketSource")]
        public string PostMarketSource { get; set; }

        [JsonProperty("strikePrice")]
        public DividendDate StrikePrice { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("postMarketChangePercent")]
        public PostMarketChange PostMarketChangePercent { get; set; }

        [JsonProperty("preMarketSource")]
        public string PreMarketSource { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("fromCurrency")]
        public dynamic FromCurrency { get; set; }

        [JsonProperty("regularMarketChangePercent")]
        public PostMarketChange RegularMarketChangePercent { get; set; }
    }

    public partial class QuoteTypeProfile
    {
        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("longName")]
        public string LongName { get; set; }

        [JsonProperty("exchangeTimezoneName")]
        public string ExchangeTimezoneName { get; set; }

        [JsonProperty("exchangeTimezoneShortName")]
        public string ExchangeTimezoneShortName { get; set; }

        [JsonProperty("isEsgPopulated")]
        public bool IsEsgPopulated { get; set; }

        [JsonProperty("gmtOffSetMilliseconds")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long GmtOffSetMilliseconds { get; set; }

        [JsonProperty("quoteType")]
        public string QuoteTypeQuoteType { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("messageBoardId")]
        public string MessageBoardId { get; set; }

        [JsonProperty("market")]
        public string Market { get; set; }
    }

    public partial class SecFilings
    {
        [JsonProperty("filings")]
        public List<Filing> Filings { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }
    }

    public partial class Filing
    {
        [JsonProperty("date")]
        public dynamic Date { get; set; }

        [JsonProperty("epochDate")]
        public long EpochDate { get; set; }

        [JsonProperty("type")]
        public dynamic Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("edgarUrl")]
        public Uri EdgarUrl { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }
    }

    public partial class SummaryDetailProfile
    {
        [JsonProperty("previousClose")]
        public PostMarketChange PreviousClose { get; set; }

        [JsonProperty("regularMarketOpen")]
        public PostMarketChange RegularMarketOpen { get; set; }

        [JsonProperty("twoHundredDayAverage")]
        public PostMarketChange TwoHundredDayAverage { get; set; }

        [JsonProperty("trailingAnnualDividendYield")]
        public DividendDate TrailingAnnualDividendYield { get; set; }

        [JsonProperty("payoutRatio")]
        public PostMarketChange PayoutRatio { get; set; }

        [JsonProperty("volume24Hr")]
        public DividendDate Volume24Hr { get; set; }

        [JsonProperty("regularMarketDayHigh")]
        public PostMarketChange RegularMarketDayHigh { get; set; }

        [JsonProperty("navPrice")]
        public DividendDate NavPrice { get; set; }

        [JsonProperty("averageDailyVolume10Day")]
        public AverageDailyVolume10Day AverageDailyVolume10Day { get; set; }

        [JsonProperty("totalAssets")]
        public DividendDate TotalAssets { get; set; }

        [JsonProperty("regularMarketPreviousClose")]
        public PostMarketChange RegularMarketPreviousClose { get; set; }

        [JsonProperty("fiftyDayAverage")]
        public PostMarketChange FiftyDayAverage { get; set; }

        [JsonProperty("trailingAnnualDividendRate")]
        public DividendDate TrailingAnnualDividendRate { get; set; }

        [JsonProperty("open")]
        public PostMarketChange Open { get; set; }

        [JsonProperty("toCurrency")]
        public dynamic ToCurrency { get; set; }

        [JsonProperty("averageVolume10days")]
        public AverageDailyVolume10Day AverageVolume10Days { get; set; }

        [JsonProperty("expireDate")]
        public DividendDate ExpireDate { get; set; }

        [JsonProperty("yield")]
        public DividendDate Yield { get; set; }

        [JsonProperty("algorithm")]
        public dynamic Algorithm { get; set; }

        [JsonProperty("dividendRate")]
        public DividendDate DividendRate { get; set; }

        [JsonProperty("exDividendDate")]
        public DividendDate ExDividendDate { get; set; }

        [JsonProperty("beta")]
        public PostMarketChange Beta { get; set; }

        [JsonProperty("circulatingSupply")]
        public DividendDate CirculatingSupply { get; set; }

        [JsonProperty("startDate")]
        public DividendDate StartDate { get; set; }

        [JsonProperty("regularMarketDayLow")]
        public PostMarketChange RegularMarketDayLow { get; set; }

        [JsonProperty("priceHint")]
        public AverageDailyVolume10Day PriceHint { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("regularMarketVolume")]
        public AverageDailyVolume10Day RegularMarketVolume { get; set; }

        [JsonProperty("lastMarket")]
        public dynamic LastMarket { get; set; }

        [JsonProperty("maxSupply")]
        public DividendDate MaxSupply { get; set; }

        [JsonProperty("openInterest")]
        public DividendDate OpenInterest { get; set; }

        [JsonProperty("marketCap")]
        public AverageDailyVolume10Day MarketCap { get; set; }

        [JsonProperty("volumeAllCurrencies")]
        public DividendDate VolumeAllCurrencies { get; set; }

        [JsonProperty("strikePrice")]
        public DividendDate StrikePrice { get; set; }

        [JsonProperty("averageVolume")]
        public AverageDailyVolume10Day AverageVolume { get; set; }

        [JsonProperty("priceToSalesTrailing12Months")]
        public PostMarketChange PriceToSalesTrailing12Months { get; set; }

        [JsonProperty("dayLow")]
        public PostMarketChange DayLow { get; set; }

        [JsonProperty("ask")]
        public PostMarketChange Ask { get; set; }

        [JsonProperty("ytdReturn")]
        public DividendDate YtdReturn { get; set; }

        [JsonProperty("askSize")]
        public AverageDailyVolume10Day AskSize { get; set; }

        [JsonProperty("volume")]
        public AverageDailyVolume10Day Volume { get; set; }

        [JsonProperty("fiftyTwoWeekHigh")]
        public PostMarketChange FiftyTwoWeekHigh { get; set; }

        [JsonProperty("forwardPE")]
        public PostMarketChange ForwardPe { get; set; }

        [JsonProperty("maxAge")]
        public long MaxAge { get; set; }

        [JsonProperty("fromCurrency")]
        public dynamic FromCurrency { get; set; }

        [JsonProperty("fiveYearAvgDividendYield")]
        public DividendDate FiveYearAvgDividendYield { get; set; }

        [JsonProperty("fiftyTwoWeekLow")]
        public PostMarketChange FiftyTwoWeekLow { get; set; }

        [JsonProperty("bid")]
        public PostMarketChange Bid { get; set; }

        [JsonProperty("tradeable")]
        public bool Tradeable { get; set; }

        [JsonProperty("dividendYield")]
        public DividendDate DividendYield { get; set; }

        [JsonProperty("bidSize")]
        public AverageDailyVolume10Day BidSize { get; set; }

        [JsonProperty("dayHigh")]
        public PostMarketChange DayHigh { get; set; }
    }

    public enum TypeEnum { The10K, The10Q, The8K, The8KA };

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "10-K":
                    return TypeEnum.The10K;
                case "10-Q":
                    return TypeEnum.The10Q;
                case "8-K":
                    return TypeEnum.The8K;
                case "8-K/A":
                    return TypeEnum.The8KA;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            switch (value)
            {
                case TypeEnum.The10K:
                    serializer.Serialize(writer, "10-K");
                    return;
                case TypeEnum.The10Q:
                    serializer.Serialize(writer, "10-Q");
                    return;
                case TypeEnum.The8K:
                    serializer.Serialize(writer, "8-K");
                    return;
                case TypeEnum.The8KA:
                    serializer.Serialize(writer, "8-K/A");
                    return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }

    /*
     {
      "financialsTemplate": {
        "code": "N",
        "maxAge": 1
      },
      "price": {
        "quoteSourceName": "Nasdaq Real Time Price",
        "regularMarketOpen": {
          "raw": 6.17,
          "fmt": "6.17"
        },
        "averageDailyVolume3Month": {
          "raw": 7947921,
          "fmt": "7.95M",
          "longFmt": "7,947,921"
        },
        "exchange": "NMS",
        "regularMarketTime": 1614718801,
        "volume24Hr": {},
        "regularMarketDayHigh": {
          "raw": 6.28,
          "fmt": "6.28"
        },
        "shortName": "Amarin Corporation plc",
        "averageDailyVolume10Day": {
          "raw": 7208950,
          "fmt": "7.21M",
          "longFmt": "7,208,950"
        },
        "longName": "Amarin Corporation plc",
        "regularMarketChange": {
          "raw": -0.15999985,
          "fmt": "-0.16"
        },
        "currencySymbol": "$",
        "regularMarketPreviousClose": {
          "raw": 6.19,
          "fmt": "6.19"
        },
        "postMarketTime": 1614729650,
        "preMarketPrice": {
          "raw": 6.15,
          "fmt": "6.15"
        },
        "preMarketTime": 1614695396,
        "exchangeDataDelayedBy": 0,
        "toCurrency": null,
        "postMarketChange": {
          "raw": -0.02,
          "fmt": "-0.02"
        },
        "postMarketPrice": {
          "raw": 6.01,
          "fmt": "6.01"
        },
        "exchangeName": "NasdaqGS",
        "preMarketChange": {
          "raw": -0.04,
          "fmt": "-0.04"
        },
        "circulatingSupply": {},
        "regularMarketDayLow": {
          "raw": 5.94,
          "fmt": "5.94"
        },
        "priceHint": {
          "raw": 2,
          "fmt": "2",
          "longFmt": "2"
        },
        "currency": "USD",
        "regularMarketPrice": {
          "raw": 6.03,
          "fmt": "6.03"
        },
        "regularMarketVolume": {
          "raw": 11547574,
          "fmt": "11.55M",
          "longFmt": "11,547,574.00"
        },
        "lastMarket": null,
        "regularMarketSource": "FREE_REALTIME",
        "openInterest": {},
        "marketState": "POST",
        "underlyingSymbol": null,
        "marketCap": {
          "raw": 2373619200,
          "fmt": "2.37B",
          "longFmt": "2,373,619,200.00"
        },
        "quoteType": "EQUITY",
        "preMarketChangePercent": {
          "raw": -0.0064620296,
          "fmt": "-0.65%"
        },
        "volumeAllCurrencies": {},
        "postMarketSource": "FREE_REALTIME",
        "strikePrice": {},
        "symbol": "AMRN",
        "postMarketChangePercent": {
          "raw": -0.0033167498,
          "fmt": "-0.33%"
        },
        "preMarketSource": "FREE_REALTIME",
        "maxAge": 1,
        "fromCurrency": null,
        "regularMarketChangePercent": {
          "raw": -0.025848117,
          "fmt": "-2.58%"
        }
      },
      "secFilings": {
        "filings": [
          {
            "date": "2021-02-25",
            "epochDate": 1614251424,
            "type": "10-K",
            "title": "Annual Report",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001564590-21-008382&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2021-02-25",
            "epochDate": 1614251221,
            "type": "8-K",
            "title": "Results of Operations and Financial Condition, Financial Statements and Exhibits",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001564590-21-008376&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2021-01-29",
            "epochDate": 1611957688,
            "type": "8-K",
            "title": "Change in Directors or Principal Officers, Financial Statements and Exhibits",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001193125-21-022781&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2021-01-07",
            "epochDate": 1610018200,
            "type": "8-K",
            "title": "Results of Operations and Financial Condition, Financial Statements and Exhibits",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001193125-21-003641&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2020-11-05",
            "epochDate": 1604574953,
            "type": "10-Q",
            "title": "Quarterly Report",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001564590-20-050798&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2020-11-05",
            "epochDate": 1604574656,
            "type": "8-K",
            "title": "Results of Operations and Financial Condition, Financial Statements and Exhibits",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001564590-20-050793&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2020-09-03",
            "epochDate": 1599168093,
            "type": "8-K",
            "title": "Other Events, Financial Statements and Exhibits",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001193125-20-239062&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2020-08-04",
            "epochDate": 1596539856,
            "type": "10-Q",
            "title": "Quarterly Report",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001564590-20-035730&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2020-08-04",
            "epochDate": 1596539499,
            "type": "8-K",
            "title": "Results of Operations and Financial Condition, Financial Statements and Exhibits",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001564590-20-035729&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2020-07-14",
            "epochDate": 1594765247,
            "type": "8-K",
            "title": "Submission of Matters to a Vote of Security Holders, Financial Statements and Exhibits",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001193125-20-192691&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2020-06-16",
            "epochDate": 1592346574,
            "type": "8-K",
            "title": "Other Events",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001193125-20-170550&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2020-05-08",
            "epochDate": 1588972798,
            "type": "8-K",
            "title": "Disclosing Other Events",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001193125-20-137711&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2020-04-30",
            "epochDate": 1588243157,
            "type": "10-Q",
            "title": "Quarterly Report",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001564590-20-019937&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2020-04-30",
            "epochDate": 1588242746,
            "type": "8-K",
            "title": "Disclosing Results of Operations and Financial Condition, Financial Statements and Exhibi",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001564590-20-019933&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2020-04-13",
            "epochDate": 1586810252,
            "type": "8-K",
            "title": "Disclosing Results of Operations and Financial Condition",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001193125-20-105284&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2020-03-31",
            "epochDate": 1585660981,
            "type": "8-K",
            "title": "Disclosing Other Events, Financial Statements and Exhibits",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001193125-20-092017&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2020-02-25",
            "epochDate": 1582665115,
            "type": "10-K",
            "title": "Annual Report",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001564590-20-006390&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2020-02-25",
            "epochDate": 1582664850,
            "type": "8-K",
            "title": "Disclosing Results of Operations and Financial Condition, Financial Statements and Exhibi",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001564590-20-006386&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2020-01-07",
            "epochDate": 1578431806,
            "type": "8-K",
            "title": "Disclosing Results of Operations and Financial Condition, Financial Statements and Exhibi",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001193125-20-003036&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2019-11-05",
            "epochDate": 1572992959,
            "type": "8-K/A",
            "title": "AMARIN CORP PLCUK FILES (8-K/A) Disclosing Results of Operations and Financial Condition, Financial Statements and Exhi",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001564590-19-040492&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2019-11-05",
            "epochDate": 1572953848,
            "type": "10-Q",
            "title": "Quarterly Report",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001564590-19-040047&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2019-11-05",
            "epochDate": 1572953498,
            "type": "8-K",
            "title": "Disclosing Results of Operations and Financial Condition, Financial Statements and Exhibi",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001564590-19-040044&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2019-08-09",
            "epochDate": 1565349733,
            "type": "8-K",
            "title": "Disclosing Other Events, Financial Statements and Exhibits",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001193125-19-217425&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2019-07-31",
            "epochDate": 1564569288,
            "type": "10-Q",
            "title": "Quarterly Report",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001564590-19-027188&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2019-07-31",
            "epochDate": 1564569041,
            "type": "8-K",
            "title": "Disclosing Results of Operations and Financial Condition, Financial Statements and Exhibi",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001564590-19-027186&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2019-07-24",
            "epochDate": 1564000378,
            "type": "8-K",
            "title": "Disclosing Entry into a Material Definitive Agreement, Other Events, Financial Statements",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001193125-19-201274&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2019-07-17",
            "epochDate": 1563395924,
            "type": "8-K",
            "title": "Disclosing Other Events",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001193125-19-195687&nav=1&src=Yahoo",
            "maxAge": 1
          },
          {
            "date": "2019-07-02",
            "epochDate": 1562069719,
            "type": "8-K",
            "title": "Disclosing Results of Operations and Financial Condition, Financial Statements and Exhibi",
            "edgarUrl": "https://yahoo.brand.edgar-online.com/DisplayFiling.aspx?TabIndex=2&dcn=0001193125-19-187778&nav=1&src=Yahoo",
            "maxAge": 1
          }
        ],
        "maxAge": 86400
      },
      "quoteType": {
        "exchange": "NGM",
        "shortName": "Amarin Corporation plc",
        "longName": "Amarin Corporation plc",
        "exchangeTimezoneName": "America/New_York",
        "exchangeTimezoneShortName": "EST",
        "isEsgPopulated": false,
        "gmtOffSetMilliseconds": "-18000000",
        "quoteType": "EQUITY",
        "symbol": "AMRN",
        "messageBoardId": "finmb_407863",
        "market": "us_market"
      },
      "calendarEvents": {
        "maxAge": 1,
        "earnings": {
          "earningsDate": [
            {
              "raw": 1614211200,
              "fmt": "2021-02-25"
            }
          ],
          "earningsAverage": {
            "raw": -0.05,
            "fmt": "-0.05"
          },
          "earningsLow": {
            "raw": -0.12,
            "fmt": "-0.12"
          },
          "earningsHigh": {
            "raw": -0.01,
            "fmt": "-0.01"
          },
          "revenueAverage": {
            "raw": 150950000,
            "fmt": "150.95M",
            "longFmt": "150,950,000"
          },
          "revenueLow": {
            "raw": 140000000,
            "fmt": "140M",
            "longFmt": "140,000,000"
          },
          "revenueHigh": {
            "raw": 161250000,
            "fmt": "161.25M",
            "longFmt": "161,250,000"
          }
        },
        "exDividendDate": {},
        "dividendDate": {}
      },
      "summaryDetail": {
        "previousClose": {
          "raw": 6.19,
          "fmt": "6.19"
        },
        "regularMarketOpen": {
          "raw": 6.17,
          "fmt": "6.17"
        },
        "twoHundredDayAverage": {
          "raw": 5.570368,
          "fmt": "5.57"
        },
        "trailingAnnualDividendYield": {},
        "payoutRatio": {
          "raw": 0,
          "fmt": "0.00%"
        },
        "volume24Hr": {},
        "regularMarketDayHigh": {
          "raw": 6.28,
          "fmt": "6.28"
        },
        "navPrice": {},
        "averageDailyVolume10Day": {
          "raw": 7208950,
          "fmt": "7.21M",
          "longFmt": "7,208,950"
        },
        "totalAssets": {},
        "regularMarketPreviousClose": {
          "raw": 6.19,
          "fmt": "6.19"
        },
        "fiftyDayAverage": {
          "raw": 7.272647,
          "fmt": "7.27"
        },
        "trailingAnnualDividendRate": {},
        "open": {
          "raw": 6.17,
          "fmt": "6.17"
        },
        "toCurrency": null,
        "averageVolume10days": {
          "raw": 7208950,
          "fmt": "7.21M",
          "longFmt": "7,208,950"
        },
        "expireDate": {},
        "yield": {},
        "algorithm": null,
        "dividendRate": {},
        "exDividendDate": {},
        "beta": {
          "raw": 2.377801,
          "fmt": "2.38"
        },
        "circulatingSupply": {},
        "startDate": {},
        "regularMarketDayLow": {
          "raw": 5.94,
          "fmt": "5.94"
        },
        "priceHint": {
          "raw": 2,
          "fmt": "2",
          "longFmt": "2"
        },
        "currency": "USD",
        "regularMarketVolume": {
          "raw": 11547574,
          "fmt": "11.55M",
          "longFmt": "11,547,574"
        },
        "lastMarket": null,
        "maxSupply": {},
        "openInterest": {},
        "marketCap": {
          "raw": 2373619200,
          "fmt": "2.37B",
          "longFmt": "2,373,619,200"
        },
        "volumeAllCurrencies": {},
        "strikePrice": {},
        "averageVolume": {
          "raw": 7947921,
          "fmt": "7.95M",
          "longFmt": "7,947,921"
        },
        "priceToSalesTrailing12Months": {
          "raw": 3.8654513,
          "fmt": "3.87"
        },
        "dayLow": {
          "raw": 5.94,
          "fmt": "5.94"
        },
        "ask": {
          "raw": 6.06,
          "fmt": "6.06"
        },
        "ytdReturn": {},
        "askSize": {
          "raw": 4000,
          "fmt": "4k",
          "longFmt": "4,000"
        },
        "volume": {
          "raw": 11547574,
          "fmt": "11.55M",
          "longFmt": "11,547,574"
        },
        "fiftyTwoWeekHigh": {
          "raw": 16.47,
          "fmt": "16.47"
        },
        "forwardPE": {
          "raw": 54.818184,
          "fmt": "54.82"
        },
        "maxAge": 1,
        "fromCurrency": null,
        "fiveYearAvgDividendYield": {},
        "fiftyTwoWeekLow": {
          "raw": 3.36,
          "fmt": "3.36"
        },
        "bid": {
          "raw": 6.04,
          "fmt": "6.04"
        },
        "tradeable": false,
        "dividendYield": {},
        "bidSize": {
          "raw": 3100,
          "fmt": "3.1k",
          "longFmt": "3,100"
        },
        "dayHigh": {
          "raw": 6.28,
          "fmt": "6.28"
        }
      },
      "symbol": "AMRN",
      "assetProfile": {
        "zip": "2",
        "sector": "Healthcare",
        "fullTimeEmployees": 1000,
        "compensationRisk": 7,
        "auditRisk": 7,
        "longBusinessSummary": "Amarin Corporation plc, a pharmaceutical company, develops and commercializes therapeutics for the treatment of cardiovascular diseases in the United States. The company's lead product is Vascepa, a prescription-only omega-3 fatty acid capsule, used as an adjunct to diet for reducing triglyceride levels in adult patients with severe hypertriglyceridemia. It is also involved in developing REDUCE-IT for the treatment of patients with high triglyceride levels who are also on statin therapy for elevated low-density lipoprotein cholesterol levels. The company sells its products principally to wholesalers and specialty pharmacy providers through direct sales force. It has a collaboration with Mochida Pharmaceutical Co., Ltd. to develop and commercialize drug products and indications based on the active pharmaceutical ingredient in Vascepa, the omega-3 acid, and eicosapentaenoic acid. The company was formerly known as Ethical Holdings plc and changed its name to Amarin Corporation plc in 1999. Amarin Corporation plc was incorporated in 1989 and is headquartered in Dublin, Ireland.",
        "city": "Dublin",
        "phone": "353 1 669 9020",
        "shareHolderRightsRisk": 8,
        "compensationAsOfEpochDate": 1577750400,
        "governanceEpochDate": 1606953600,
        "boardRisk": 8,
        "country": "Ireland",
        "companyOfficers": [
          {
            "totalPay": {
              "raw": 1339029,
              "fmt": "1.34M",
              "longFmt": "1,339,029"
            },
            "exercisedValue": {
              "raw": 2787458,
              "fmt": "2.79M",
              "longFmt": "2,787,458"
            },
            "yearBorn": 1961,
            "name": "Mr. John F. Thero",
            "title": "Pres, CEO & Director",
            "maxAge": 1,
            "fiscalYear": 2019,
            "unexercisedValue": {
              "raw": 81114024,
              "fmt": "81.11M",
              "longFmt": "81,114,024"
            },
            "age": 59
          },
          {
            "totalPay": {
              "raw": 672602,
              "fmt": "672.6k",
              "longFmt": "672,602"
            },
            "exercisedValue": {
              "raw": 6742863,
              "fmt": "6.74M",
              "longFmt": "6,742,863"
            },
            "yearBorn": 1971,
            "name": "Mr. Michael W. Kalb",
            "title": "CFO, Sr. VP & Assistant Sec.",
            "maxAge": 1,
            "fiscalYear": 2019,
            "unexercisedValue": {
              "raw": 2388921,
              "fmt": "2.39M",
              "longFmt": "2,388,921"
            },
            "age": 49
          },
          {
            "totalPay": {
              "raw": 837700,
              "fmt": "837.7k",
              "longFmt": "837,700"
            },
            "exercisedValue": {
              "raw": 1545950,
              "fmt": "1.55M",
              "longFmt": "1,545,950"
            },
            "yearBorn": 1965,
            "name": "Dr. Steven B. Ketchum",
            "title": "Pres of R&D, Sr. VP and Chief Scientific Officer",
            "maxAge": 1,
            "fiscalYear": 2019,
            "unexercisedValue": {
              "raw": 7650791,
              "fmt": "7.65M",
              "longFmt": "7,650,791"
            },
            "age": 55
          },
          {
            "totalPay": {
              "raw": 797700,
              "fmt": "797.7k",
              "longFmt": "797,700"
            },
            "exercisedValue": {
              "raw": 12402954,
              "fmt": "12.4M",
              "longFmt": "12,402,954"
            },
            "yearBorn": 1968,
            "name": "Mr. Joseph T. Kennedy",
            "title": "Exec. VP, Gen. Counsel & Strategic Initiatives and Sec.",
            "maxAge": 1,
            "fiscalYear": 2019,
            "unexercisedValue": {
              "raw": 998220,
              "fmt": "998.22k",
              "longFmt": "998,220"
            },
            "age": 52
          },
          {
            "totalPay": {
              "raw": 673590,
              "fmt": "673.59k",
              "longFmt": "673,590"
            },
            "exercisedValue": {
              "raw": 5433272,
              "fmt": "5.43M",
              "longFmt": "5,433,272"
            },
            "yearBorn": 1963,
            "name": "Mr. Aaron D. Berg",
            "title": "Sr. VP & Chief Commercial Officer",
            "maxAge": 1,
            "fiscalYear": 2019,
            "unexercisedValue": {
              "raw": 2683887,
              "fmt": "2.68M",
              "longFmt": "2,683,887"
            },
            "age": 57
          },
          {
            "maxAge": 1,
            "name": "Ms. Elisabeth  Schwartz",
            "title": "Sr. Director of Investor Relations",
            "exercisedValue": {
              "raw": 0,
              "fmt": null,
              "longFmt": "0"
            },
            "unexercisedValue": {
              "raw": 0,
              "fmt": null,
              "longFmt": "0"
            }
          },
          {
            "maxAge": 1,
            "name": "Mr. Daniel S. Dunham",
            "title": "Sr. VP & Chief Pharmaceutical Compliance Officer",
            "exercisedValue": {
              "raw": 0,
              "fmt": null,
              "longFmt": "0"
            },
            "unexercisedValue": {
              "raw": 0,
              "fmt": null,
              "longFmt": "0"
            }
          },
          {
            "maxAge": 1,
            "name": "Alina  Kolomeyer",
            "title": "Director of Communications",
            "exercisedValue": {
              "raw": 0,
              "fmt": null,
              "longFmt": "0"
            },
            "unexercisedValue": {
              "raw": 0,
              "fmt": null,
              "longFmt": "0"
            }
          },
          {
            "maxAge": 1,
            "name": "Mr. Rami  Daoud",
            "title": "Sr. VP of Corp. Devel.",
            "exercisedValue": {
              "raw": 0,
              "fmt": null,
              "longFmt": "0"
            },
            "unexercisedValue": {
              "raw": 0,
              "fmt": null,
              "longFmt": "0"
            }
          },
          {
            "maxAge": 1,
            "name": "Ms. Donna  Pasek",
            "title": "Sr. VP of HR",
            "exercisedValue": {
              "raw": 0,
              "fmt": null,
              "longFmt": "0"
            },
            "unexercisedValue": {
              "raw": 0,
              "fmt": null,
              "longFmt": "0"
            }
          }
        ],
        "website": "http://www.amarincorp.com",
        "maxAge": 86400,
        "overallRisk": 8,
        "address1": "Grand Canal Docklands",
        "industry": "Biotechnology",
        "address2": "Block C 77 Sir John Rogerson's Quay"
      },
      "pageViews": {
        "shortTermTrend": "UP",
        "midTermTrend": "UP",
        "longTermTrend": "UP",
        "maxAge": 1
      }
    }
     */
    #endregion

    #endregion
}
