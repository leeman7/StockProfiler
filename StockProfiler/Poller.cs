using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace StockProfiler
{
    public class Poller
    {
        private System.Timers.Timer pollTimer { get; set; }
        public System.Timers.Timer PollTimer => pollTimer;

        public enum EventHandlerType
        {
            Generic,
            Connections,
            Requests
        }

        public Poller()
        {
        }

        /// <summary>
        /// Poller alternate Constructor.
        /// </summary>
        /// <param name="eventType">Type of Event from above enums</param>
        /// <param name="timer">how long timer should last</param>
        /// <param name="interval">interval in which to check</param>
        public Poller(EventHandlerType eventType, int timer, int interval)
        {
            pollTimer = new System.Timers.Timer(timer);
            pollTimer.Elapsed += SetEventHandler(eventType);
            pollTimer.Interval = interval;
            pollTimer.Enabled = true;            
        }

        /// <summary>
        /// Determines the EventHandler route based on the EvenType passed to it. 
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns>EventHandler delegate</returns>
        public ElapsedEventHandler SetEventHandler(EventHandlerType eventType)
        {
            
            switch (eventType)
            {
                case EventHandlerType.Generic:
                    return new ElapsedEventHandler(PollGenericEvent);
                case EventHandlerType.Connections:
                    return new ElapsedEventHandler(PollConnectionEvent);
                case EventHandlerType.Requests:
                    return new ElapsedEventHandler(PollRequestEvent);
                default:
                    return new ElapsedEventHandler(PollGenericEvent);
            }
        }

        /// <summary>
        /// Creates a new Poller. Might get rid of this now that we have constructors
        /// </summary>
        /// <param name="timer">Time the timer lasts</param>
        /// <param name="interval">Interval in which the timer checks</param>
        public void CreatePoller(int timer, int interval)
        {
            // Create a timer.
            pollTimer = new System.Timers.Timer(timer); // timer

            // Hook up the Elapsed event for the timer.
            pollTimer.Elapsed += new ElapsedEventHandler(PollGenericEvent);

            // Set the Interval in milliseconds.
            pollTimer.Interval = interval; // interval goes here
            pollTimer.Enabled = true;
        }

        /// <summary>
        /// Stops a running Poll until started again.
        /// </summary>
        /// <returns>true if stopped</returns>
        public bool StopPoller()
        {
            bool stopped = false;
            pollTimer.Stop();
            if (pollTimer.Enabled)
            {
                Logger.Log(LogTarget.File, $"{pollTimer.GetHashCode()}: {pollTimer.SynchronizingObject} Poller STOPPED");
                stopped = true;
            }
            return stopped;
        }

        /// <summary>
        /// Starts a stopped Poll.
        /// </summary>
        /// <returns></returns>
        public bool StartPoller()
        {
            bool started = false;
            pollTimer.Start();
            if (!pollTimer.Enabled)
            {
                Logger.Log(LogTarget.File, $"{pollTimer.GetHashCode()}: {pollTimer.SynchronizingObject} Poller STARTED");
                started = true;
            }
            return started;
        }

        /// <summary>
        /// Main application Polling event. Should display/log generic stuff.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PollGenericEvent(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"Timed Event: ");
            //var quotes = Program.JsonHandler();
        }

        /// <summary>
        /// For Polling external service connections are still connected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PollConnectionEvent(object sender, ElapsedEventArgs e)
        {
            Program.MongoClient.IsConnected = Program.MongoClient.PingDatabase();
            Program.RedisClient.IsConnected = Program.RedisClient.TestConnection();
        }

        /// <summary>
        /// Polls to execute Rapid requests and save data to the cache and DB.
        /// Priority: Rapid >> Redis >> MongoDB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PollRequestEvent(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"Timed Event: ");
            var quotes = Program.ProcessJSONRequest();
            Program.RedisClient.Save(quotes);
            Program.MongoClient.InsertMany();
        }
    }
}
