using System;
using System.Linq;
using MongoDB.Driver.Core.Operations;
using RestSharp;

namespace StockProfiler
{
    public class Rapid
    {
        // Constants
        private const string HEADERHOST = "x-rapidapi-host";
        private const string HEADERKEY = "x-rapidapi-key";
        private const string RAPIDHOST = "apidojo-yahoo-finance-v1.p.rapidapi.com";
        private const string RAPIDKEY = "dbf9709d05msh063e6dfc0a65f37p1e763ejsn155b8b59ced5";
        private const string ADDRESS = "https://apidojo-yahoo-finance-v1.p.rapidapi.com/";
        private const string USERID = "X3NJ2A7VDSABUI4URBWME2PZNM";
        public string[] WATCHLIST = { "MSFT", "AAPL", "FB", "TSLA", "JNJ", "PG", "T", "SCHW", "MS", "BRKB", "CSCO", "SBUX" };
        // Special characters for creating custom request string. TODO: break out subsets and create handling for them leaving only necessary special characters.
        public string[] SPECIAL = { "%252C", "%255E", "symbols=", "comparisons=", "&", "interval=", "pfId=", "userId=", "startDate=", "endDate=", "range=", "size=", "?" };

        /// <summary>
        /// Used to set the market region for the given request.
        /// Cases: US|BR|AU|CA|FR|DE|HK|IN|IT|ES|GB|SG
        /// </summary>
        public enum RequestRegion
        {
            US, // United States
            BR, // Brazil
            AU, // Australia
            CA, // Canada
            FR, // France
            DE, // Germany
            HK, // Hong Kong
            IN, // India
            IT, // Italy
            ES, // Spain
            GB, // United Kingdom
            SG  // Singapore
        }

        /// <summary>
        /// Request Type
        /// TODO: Add the others in case this gets bigger.
        /// </summary>
        public enum RequestType
        {
            MARKET,
            STOCK,
            NEWS
        }

        /// <summary>
        /// Request version code; some of the requests have multiple versions and or deprecated.
        /// So it's important to include these for future development.
        /// </summary>
        public enum RequestVersion
        {
            NONE,
            V1,
            V2,
            V3
        }

        /// <summary>
        /// Request sub-type. This denotes the actual request we will be making.
        /// Note that not all of these have a version and or are under different Request types.
        /// </summary>
        public enum RequestSubType
        {
            QUOTES,
            WATCHLIST,
            SUMMARY,
            CHARTS,
            PROFILE,
            EARNINGS,
            ANALYSIS,
            HISTORICAL,
            TRENDING
        }

        /// <summary>
        /// TODO: write parsing logic to get details we need and leave user custom string.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string ParseRequestString(string request)
        {
            string parsed = string.Empty;

            return parsed;
        }

        /// <summary>
        /// Get the region for the request we are building.
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public string GetRequestRegion(RequestRegion region)
        {
            switch (region)
            {
                case RequestRegion.US:
                    return "US";
                case RequestRegion.BR:
                    return "BR";
                case RequestRegion.AU:
                    return "AU";
                case RequestRegion.CA:
                    return "CA";
                case RequestRegion.FR:
                    return "FR";
                case RequestRegion.DE:
                    return "DE";
                case RequestRegion.HK:
                    return "HK";
                case RequestRegion.IN:
                    return "IN";
                case RequestRegion.IT:
                    return "IT";
                case RequestRegion.ES:
                    return "ES";
                case RequestRegion.GB:
                    return "GB";
                case RequestRegion.SG:
                    return "SG";
                default:
                    return "US";
            }
        }

        /// <summary>
        /// Generate the request string that will be sent to make the Rapid Request call.
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="requestVersion"></param>
        /// <param name="requestSubType"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GenerateRequestString(RequestType requestType, RequestVersion requestVersion, RequestSubType requestSubType, string request)
        {
            string reqTypeStr = GetRequestType(requestType);
            string reqVersionStr = GetRequestVersion(requestVersion);
            string reqSubTypeStr = GetRequestSubType(requestSubType);
            string reqUserCustomStr = GetRequestUserCustom(request);
            // TODO: add handling to place the region in this request since its in multiple different locations depending on the request.
            string fullRequest = ADDRESS + reqTypeStr + reqVersionStr + reqSubTypeStr + reqUserCustomStr;
            return fullRequest;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string GetRequestUserCustom(string request)
        {
            bool symbolsRequired = false;
            string custom = string.Empty;

            // Symbol string formatting
            if (symbolsRequired)
            {
                string symbolStr = "symbols=";
                foreach (var symbol in WATCHLIST)
                {
                    // %252C vs &
                    symbolStr += symbol + (symbol != WATCHLIST.Last() ? SPECIAL[0] : SPECIAL[4]);
                }
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the request sub-type for the request we are building.
        /// These distinguish which request we are calling.
        /// </summary>
        /// <param name="requestSubType"></param>
        /// <returns>request sub-type string</returns>
        private string GetRequestSubType(RequestSubType requestSubType)
        {
            string prefix = "get-";
            string subType = string.Empty;
            switch (requestSubType)
            {
                case RequestSubType.QUOTES:
                    subType = "quotes";
                    break;
                case RequestSubType.WATCHLIST:
                    subType = "watchlist-performance";
                    break;
                case RequestSubType.SUMMARY:
                    subType = "summary";
                    break;
                case RequestSubType.CHARTS:
                    subType = "charts";
                    break;
                case RequestSubType.PROFILE:
                    subType = "profile";
                    break;
                case RequestSubType.EARNINGS:
                    subType = "earnings";
                    break;
                case RequestSubType.ANALYSIS:
                    subType = "analysis";
                    break;
                case RequestSubType.HISTORICAL:
                    subType = "historical-data";
                    break;
                case RequestSubType.TRENDING:
                    subType = "trending-tickers";
                    break;
                default:
                    // Trending Tickers is default as it requires no custom parameters other than region.
                    subType = "trending-tickers";
                    break;
            }
            string fullSubType = prefix + subType + "?";
            return fullSubType;
        }

        /// <summary>
        /// Gets the request type for the request string we are building.
        /// </summary>
        /// <param name="requestType"></param>
        /// <returns>request type string</returns>
        public string GetRequestType(RequestType requestType)
        {

            switch (requestType)
            {                
                case RequestType.MARKET:
                    return "market";
                case RequestType.STOCK:
                    return "stock";
                case RequestType.NEWS:
                    return "news";
                default:
                    return "market";
            }
        }

        /// <summary>
        /// Gets the request version for the request we are building.
        /// </summary>
        /// <param name="requestVersion"></param>
        /// <returns>request version string</returns>
        public string GetRequestVersion(RequestVersion requestVersion)
        {
            switch (requestVersion)
            {
                case RequestVersion.V1:
                    return "v1";
                case RequestVersion.V2:
                    return "v2";
                case RequestVersion.V3:
                    return "v3";
                case RequestVersion.NONE:                    
                default:
                    return "";
            }
        }

        /// <summary>
        /// TODO: Make generic request client for all Requests
        /// </summary>
        /// <returns></returns>
        public RestClient RequestClient()
        {
            var client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/v2/get-quotes?symbols=MSFT%252CIBM%252CAAPL%252CTSLA%252CT%252CSCHW%252CFB%252CPG%252CMS%252CJNJ%252CSBUX&region=US");
            return client;
        }

        #region Rapid Requests
        /// <summary>
        /// Request a stock quote from Rapid API.
        /// </summary>
        /// <returns></returns>
        public string RequestQuote()
        {
            var client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/v2/get-quotes?symbols=MSFT%252CIBM%252CAAPL%252CTSLA%252CT%252CSCHW%252CFB%252CPG%252CMS%252CJNJ%252CSBUX&region=US");
            var request = new RestRequest(Method.GET);
            request.AddHeader(HEADERHOST, RAPIDHOST);
            request.AddHeader(HEADERKEY, RAPIDKEY);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        // Charts
        public string RequestCharts()
        {
            var client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/get-charts?region=US&comparisons=%255EGDAXI%252C%255EFCHI&symbol=HYDR.ME&interval=5m&range=1d");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", RAPIDHOST);
            request.AddHeader("x-rapidapi-key", RAPIDKEY);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        // Watchlist
        public string RequestWatchlist()
        {
            var client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/get-watchlist-performance?region=US&symbols=%255EGSPC&pfId=the_berkshire_hathaway_portfolio&userId=X3NJ2A7VDSABUI4URBWME2PZNM");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", RAPIDHOST);
            request.AddHeader("x-rapidapi-key", RAPIDKEY);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        // Earnings
        public string RequestEarnings()
        {
            var client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/get-earnings?size=10&region=US&startDate=1585155600000&endDate=1589475600000");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", RAPIDHOST);
            request.AddHeader("x-rapidapi-key", RAPIDKEY);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        // Trending Tickers
        public string RequestTrendingStocks()
        {
            var client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/get-trending-tickers?region=US");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", RAPIDHOST);
            request.AddHeader("x-rapidapi-key", RAPIDKEY);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        // STOCK SPECIFIC
        // Historical Data
        public string RequestHistoricalData()
        {
            var client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/stock/v3/get-historical-data?region=US&symbol=AMRN");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", RAPIDHOST);
            request.AddHeader("x-rapidapi-key", RAPIDKEY);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        // Stock Analysis
        public string RequestStockAnalysis()
        {
            var client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/stock/v2/get-analysis?region=US&symbol=AMRN");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", RAPIDHOST);
            request.AddHeader("x-rapidapi-key", RAPIDKEY);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        // Stock Summary
        public string RequestStockSummary()
        {
            var client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/stock/v2/get-summary?region=US&symbol=AMRN");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", RAPIDHOST);
            request.AddHeader("x-rapidapi-key", RAPIDKEY);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        // Stock Profile
        public string RequestStockProfile()
        {
            var client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/stock/v2/get-profile?region=US&symbol=AMRN");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", RAPIDHOST);
            request.AddHeader("x-rapidapi-key", RAPIDKEY);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        #endregion

    }
}
