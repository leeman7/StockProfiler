using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace StockProfiler
{
    public enum LogTarget
    {
        File,
        Database,
        Cache,
        EventLog,
        Exception
    }

    public abstract class LogBase
    {
        protected readonly object lockObj = new object();
        public abstract void Log(string message);
    }

    public class FileLogger : LogBase
    {
        string logPath = @"..\Logs\StockProfiler-" + DateTime.Today.Month + "-" + DateTime.Today.Day + "-" + DateTime.Today.Year + ".txt";
        public override void Log(string message)
        {
            lock (lockObj)
            {
                using (StreamWriter streamWriter = File.AppendText(logPath))
                {                   
                    streamWriter.WriteLine($"{message}");
                }
            }
        }


        public void LogAll(string message)
        {
            lock (lockObj)
            {
                using (StreamWriter streamWriter = File.AppendText(logPath))
                {
                    streamWriter.WriteLine(message);
                }
            }
        }
    }

    public class MongoDBLogger : LogBase
    {
        string connectionString = string.Empty;
        public override void Log(string message)
        {
            lock (lockObj)
            {
                //Code to log data to the database
            }
        }
    }

    public class CacheLogger : LogBase
    {
        string connectionString = string.Empty;
        public override void Log(string message)
        {
            lock (lockObj)
            {
                //Code to log data to the database
            }
        }
    }

    public class EventLogger : LogBase
    {
        public override void Log(string message)
        {
            lock (lockObj)
            {
                EventLog eventLog = new EventLog();
                eventLog.Source = "StockProfiler";
                eventLog.WriteEntry(message);
            }
        }
    }

    public class ExceptionLogger : LogBase
    {
        string exceptionPath = @"..\Logs\ExceptionLog-" + DateTime.Today.Month + "-" + DateTime.Today.Day + "-" + DateTime.Today.Year + ".txt";
        public override void Log(string message)
        {
            lock (lockObj)
            {
                using (StreamWriter streamWriter = File.AppendText(exceptionPath))
                {
                    streamWriter.WriteLine(message);
                }
            }
        }
    }

    public static class Logger
    {
        private static LogBase logger = null;
        public static void Log(LogTarget target, string message)
        {
            switch (target)
            {
                case LogTarget.File:
                    logger = new FileLogger();
                    logger.Log(message);
                    break;
                case LogTarget.Database:
                    logger = new MongoDBLogger();
                    logger.Log(message);
                    break;
                case LogTarget.EventLog:
                    logger = new EventLogger();
                    logger.Log(message);
                    break;
                case LogTarget.Exception:
                    logger = new ExceptionLogger();
                    logger.Log(message);
                    break;
                case LogTarget.Cache:
                    logger = new CacheLogger();
                    logger.Log(message);
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// Validate the path file directory provided.
        /// </summary>
        /// <param name="path"></param>
        private static void ValidatePath(string path)
        {
            try
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (!directory.Exists)
                    directory.Create();
            }
            catch (Exception ex)
            {
                Log(LogTarget.Exception, path + $"{ex}");
            }
        }

        /// <summary>
        /// Validate the file path for the log file provided.
        /// </summary>
        /// <param name="filePath"></param>
        private static void ValidateFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    File.Create(filePath);
            }
            catch (Exception ex)
            {
                Log(LogTarget.Exception, filePath + $"{ex}");
            }
        }

        /// <summary>
        /// Open and or Create a new log file for the day.
        /// </summary>
        public static void LogFileHelper()
        {
            try
            {
                //TODO: Remove strings later and replace with config settings to set the path file.
                string path = @"..\Logs\";
                string filePath = path + @"\StockProfiler-" + DateTime.Today.Month + "-" + DateTime.Today.Day + "-" + DateTime.Today.Year + ".txt";
                ValidatePath(path);
                ValidateFile(filePath);
                Log(LogTarget.File, $"{filePath} Generated");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
                Log(LogTarget.Exception, $"{ex}");
            }
        }
    }
}
