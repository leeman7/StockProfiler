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

            return custom;
        }

        /// <summary>
        /// Get the region for the request we are building.
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public string GetRequestRegion(RequestRegion region)
        {
            return region switch
            {
                RequestRegion.US => "US",
                RequestRegion.BR => "BR",
                RequestRegion.AU => "AU",
                RequestRegion.CA => "CA",
                RequestRegion.FR => "FR",
                RequestRegion.DE => "DE",
                RequestRegion.HK => "HK",
                RequestRegion.IN => "IN",
                RequestRegion.IT => "IT",
                RequestRegion.ES => "ES",
                RequestRegion.GB => "GB",
                RequestRegion.SG => "SG",
                _ => "US",
            };
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
            string subType = requestSubType switch
            {
                RequestSubType.QUOTES => "quotes",
                RequestSubType.WATCHLIST => "watchlist-performance",
                RequestSubType.SUMMARY => "summary",
                RequestSubType.CHARTS => "charts",
                RequestSubType.PROFILE => "profile",
                RequestSubType.EARNINGS => "earnings",
                RequestSubType.ANALYSIS => "analysis",
                RequestSubType.HISTORICAL => "historical-data",
                RequestSubType.TRENDING => "trending-tickers",
                _ => "trending-tickers",// Trending Tickers is default as it requires no custom parameters other than region.
            };
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
            return requestType switch
            {
                RequestType.MARKET => "market",
                RequestType.STOCK => "stock",
                RequestType.NEWS => "news",
                _ => "market",
            };
        }

        /// <summary>
        /// Gets the request version for the request we are building.
        /// </summary>
        /// <param name="requestVersion"></param>
        /// <returns>request version string</returns>
        public string GetRequestVersion(RequestVersion requestVersion)
        {
            return requestVersion switch
            {
                RequestVersion.V1 => "v1",
                RequestVersion.V2 => "v2",
                RequestVersion.V3 => "v3",
                _ => "",
            };
        }

        /// <summary>
        /// Make generic request client for all Requests
        /// </summary>
        /// <returns></returns>
        public RestClient RequestClient(string request)
        {
            RestClient client = new RestClient(request);
            return client;
        }

        /// <summary>
        /// Make a generic request for all Requests
        /// </summary>
        /// <param name="restClient"></param>
        /// <returns></returns>
        public string SendRequest(RestClient restClient)
        {
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader(HEADERHOST, RAPIDHOST);
            request.AddHeader(HEADERKEY, RAPIDKEY);
            IRestResponse response = restClient.Execute(request);
            return response.Content;
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
            request.AddHeader(HEADERHOST, RAPIDHOST);
            request.AddHeader(HEADERKEY, RAPIDKEY);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        // Watchlist
        public string RequestWatchlist()
        {
            var client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/get-watchlist-performance?region=US&symbols=%255EGSPC&pfId=the_berkshire_hathaway_portfolio&userId=X3NJ2A7VDSABUI4URBWME2PZNM");
            var request = new RestRequest(Method.GET);
            request.AddHeader(HEADERHOST, RAPIDHOST);
            request.AddHeader(HEADERKEY, RAPIDKEY);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        // Earnings
        public string RequestEarnings()
        {
            var client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/get-earnings?size=10&region=US&startDate=1585155600000&endDate=1589475600000");
            var request = new RestRequest(Method.GET);
            request.AddHeader(HEADERHOST, RAPIDHOST);
            request.AddHeader(HEADERKEY, RAPIDKEY);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        // Trending Tickers
        public string RequestTrendingStocks()
        {
            var client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/get-trending-tickers?region=US");
            var request = new RestRequest(Method.GET);
            request.AddHeader(HEADERHOST, RAPIDHOST);
            request.AddHeader(HEADERKEY, RAPIDKEY);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        // STOCK SPECIFIC
        // Historical Data
        public string RequestHistoricalData()
        {
            var client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/stock/v3/get-historical-data?region=US&symbol=AMRN");
            var request = new RestRequest(Method.GET);
            request.AddHeader(HEADERHOST, RAPIDHOST);
            request.AddHeader(HEADERKEY, RAPIDKEY);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        // Stock Analysis
        public string RequestStockAnalysis()
        {
            var client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/stock/v2/get-analysis?region=US&symbol=AMRN");
            var request = new RestRequest(Method.GET);
            request.AddHeader(HEADERHOST, RAPIDHOST);
            request.AddHeader(HEADERKEY, RAPIDKEY);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        // Stock Summary
        public string RequestStockSummary()
        {
            var client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/stock/v2/get-summary?region=US&symbol=AMRN");
            var request = new RestRequest(Method.GET);
            request.AddHeader(HEADERHOST, RAPIDHOST);
            request.AddHeader(HEADERKEY, RAPIDKEY);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        // Stock Profile
        public string RequestStockProfile()
        {
            var client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/stock/v2/get-profile?region=US&symbol=AMRN");
            var request = new RestRequest(Method.GET);
            request.AddHeader(HEADERHOST, RAPIDHOST);
            request.AddHeader(HEADERKEY, RAPIDKEY);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        #endregion
    }
}
