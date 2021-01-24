# StockProfiler

This is a stock profiler application that leverages the RAPID API to Yahoo Finance to get Stock data and display it. This application offers a multitude of options for Market research by providing a handful of the most common requests for Stock Market data. Anything from Stock Quotes to Stock History and more! Also note that since this uses Yahoo Finance, Market data is most likely delayed 15 minutes and should not be used for Day Trading and High Volume trading due to the delay. The Stock Profiler is meant mostly for Stock Market analysis and prediction. Lastly this Project is still actively being worked on, so functionalty is subject to change and some issues may appear.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to use the application on a live system.

### Prerequisites

To use this application you will need to install Redis Server and MongoDB. Both of these applications are used as data storage on the localhost machine, but can be configured if needed to be remote.

* Redis - Used as intermediary cache storage for data
[Ubuntu](https://redis.io/download)
[Windows](https://github.com/dmajkic/redis/downloads)
* [MongoDB](https://www.mongodb.com/try/download/community) - Used as primary storage of data 

### Installing

A step by step series of examples that tell you how to get a development env running. This should be straight forward after setting up the Prerequisites, as no futher setup is needed other than downloading and configuring you service connections.

Git:
```
git clone https://github.com/leeman7/StockProfiler.git
```

### Usage

Using Stock Profiler should be fairly straight forward at the top level. Aside from that there are a good sum of different requests one can make to the Rapid API for Yahoo Market data. These requests determine the data you want to view at the time and are all logged accordingly in case one needed to go back and review older data. The different requests are as follows:

* Quotes(v2/get-quotes) - 
* Watchlist Performance(get-watchlist-performance) - 
* Earnings(get-earnings) - 
* Historical Data(v3/get-historical-data) - 
* Summary(get-summary) - 
* Stock Analysis(v2/get-analysis) - 
* Market Charts(get-charts) - 
* Trending Stocks(get-trending-tickers) - 
* Stock Profile(v2/get-profile) - 
* More to be added...

Other Stuff
There are also Poll timers that will be configurable, but let one determine how often to do automated requests for specific data. 

There will also be options to take in data from the DB/log files and format them into CSV/Excel files for your own analysis. This most likely will actively track daily data with a few options with less verbose stats for weekly and monthly data.

## Deployment

Deploying the application involves running either in the Cloud, on Ubuntu, or Windows. Ensure that the computer has the correct.NET Framework 3.1+ and the proper Binaries and Runtimes required to run the application.

## Built With

* [Visual-Studio-2019](https://visualstudio.microsoft.com/vs/community/) - IDE Utilities
* [.NET-Framework](https://dotnet.microsoft.com/) - The framework used
* [Nuget](https://www.nuget.org/) - Package dependency management
* [Rapid-API](https://rapidapi.com/category/Finance) - API data service
* [MongoDB](https://www.mongodb.com/try/download/community) - Used for primary storage
* [Redis](https://github.com/dmajkic/redis/downloads) - Used for cache storage

* [Dropwizard](http://www.dropwizard.io/1.0.2/docs/) - The web framework used
* [Maven](https://maven.apache.org/) - Dependency Management
* [ROME](https://rometools.github.io/rome/) - Used to generate RSS Feeds

## Contributing

For details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Lee Nardo** - *Initial work* - [Leeman7](https://github.com/leeman7)

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## License

None.

## Acknowledgments

* Hat tip to anyone whose code was used
* Inspiration

