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
        public void Poll()
        {
            int delay = 3000;
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;
            Task listener = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    // poll hardware
                    TestPoll();
                    Thread.Sleep(delay);
                    if (token.IsCancellationRequested)
                        break;
                }

                // cleanup, e.g. close connection
            }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void TestPoll()
        {
            Console.WriteLine("Test Polling");
        }

        public void AlternatePoller()
        {
            // TODO: Run timers in background to make requests.
            // Create a timer with a ten second interval.
            System.Timers.Timer aTimer;
            aTimer = new System.Timers.Timer(60000);

            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            // Set the Interval to 2 seconds (2000 milliseconds).
            aTimer.Interval = 30000;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"Timed Event: ");
            var quotes = Program.JsonHandler();

            #if false
            Program.RedisClient.Save(quotes);
            Program.MongoClient.InsertMany();
            #endif
        }
    }
}
