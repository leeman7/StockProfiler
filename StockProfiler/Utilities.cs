using System;
using System.Collections.Generic;
using System.Text;

namespace StockProfiler
{
    public class Utilities
    {

        // Schedule Timer

        // DateTime Schedule check

        // DateTime converter
        public string DateTimeLogFormat()
        {
            return DateTime.Today.ToUniversalTime().ToLocalTime().ToString() + ">  ";
        }

        // Message and Log formatting
        public string LogFormatter()
        {
            return DateTime.Today.ToUniversalTime().ToLocalTime().ToString() + ">  ";
        }

        // File Handler Rotater

        // CSV and Table file generator
        public bool CSVGenerator()
        {
            return true;
        }

        public bool GenerateTable()
        {
            return true;
        }

        // Stock Merge Delta
    }
}
