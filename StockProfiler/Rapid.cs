using RestSharp;

namespace StockProfiler
{
    public class Rapid
    {
        // Constants
        private const string RAPIDHOST = "apidojo-yahoo-finance-v1.p.rapidapi.com";
        private const string RAPIDKEY = "dbf9709d05msh063e6dfc0a65f37p1e763ejsn155b8b59ced5";
        private const string ADDRESS = "https://apidojo-yahoo-finance-v1.p.rapidapi.com/";

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
            request.AddHeader("x-rapidapi-host", RAPIDHOST);
            request.AddHeader("x-rapidapi-key", RAPIDKEY);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        // TODO: Add more Rapid requests here
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
