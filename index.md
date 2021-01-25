## Welcome to GitHub Pages

You can use the [editor on GitHub](https://github.com/leeman7/StockProfiler/edit/gh-pages/index.md) to maintain and preview the content for your website in Markdown files.

Whenever you commit to this repository, GitHub Pages will run [Jekyll](https://jekyllrb.com/) to rebuild the pages in your site, from the content in your Markdown files.


### Summary
This is a stock profiler application that leverages the RAPID API to Yahoo Finance to get Stock data and display it. This application offers a multitude of options for Market research by providing a handful of the most common requests for Stock Market data. Anything from Stock Quotes to Stock History and more! Also note that since this uses Yahoo Finance, Market data is most likely delayed 15 minutes and should not be used for Day Trading and High Volume trading due to the delay. The Stock Profiler is meant mostly for Stock Market analysis and prediction. Lastly this Project is still actively being worked on, so functionalty is subject to change and some issues may appear.

### Getting Started
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to use the application on a live system.

### Prerequisites
To use this application you will need to install Redis Server and MongoDB. Both of these applications are used as data storage on the localhost machine, but can be configured if needed to be remote.

Redis - Used as intermediary cache storage for data Ubuntu Windows
MongoDB - Used as primary storage of data

### Installing
A step by step series of examples that tell you how to get a development env running. This should be straight forward after setting up the Prerequisites, as no futher setup is needed other than downloading and configuring you service connections.

Git:
git clone https://github.com/leeman7/StockProfiler.git

### Usage
Using Stock Profiler should be fairly straight forward at the top level. Aside from that there are a good sum of different requests one can make to the Rapid API for Yahoo Market data. These requests determine the data you want to view at the time and are all logged accordingly in case one needed to go back and review older data. The different requests are as follows:

1. Quotes(v2/get-quotes) -
2. Watchlist Performance(get-watchlist-performance) -
3. Earnings(get-earnings) -
4. Historical Data(v3/get-historical-data) -
5. Summary(get-summary) -
6. Stock Analysis(v2/get-analysis) -
7. Market Charts(get-charts) -
8. Trending Stocks(get-trending-tickers) -
9. Stock Profile(v2/get-profile) -

More to be added...
Other Stuff There are also Poll timers that will be configurable, but let one determine how often to do automated requests for specific data.

There will also be options to take in data from the DB/log files and format them into CSV/Excel files for your own analysis. This most likely will actively track daily data with a few options with less verbose stats for weekly and monthly data.

### Deployment
Deploying the application involves running either in the Cloud, on Ubuntu, or Windows. Ensure that the computer has the correct.NET Framework 3.1+ and the proper Binaries and Runtimes required to run the application.

### Built With
- Visual-Studio-2019 - IDE Utilities
- .NET-Framework - The framework used
- Nuget - Package dependency management
- Rapid-API - API data service
- MongoDB - Used for primary storage
- Redis - Used for cache storage


**Bold** and _Italic_ and `Code` text

```markdown
Syntax highlighted code block
[Link](url) and ![Image](src)
```

For more details see [GitHub Flavored Markdown](https://guides.github.com/features/mastering-markdown/).

### Jekyll Themes

Your Pages site will use the layout and styles from the Jekyll theme you have selected in your [repository settings](https://github.com/leeman7/StockProfiler/settings). The name of this theme is saved in the Jekyll `_config.yml` configuration file.

### Support or Contact

Having trouble with Pages? Check out our [documentation](https://docs.github.com/categories/github-pages-basics/) or [contact support](https://support.github.com/contact) and we’ll help you sort it out.
