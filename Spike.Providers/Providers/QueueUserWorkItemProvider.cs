


namespace Spike.Providers.Providers
{
    using Common;
    using MultiThreading;
    using System;
    using System.Threading;

    public class QueueUserWorkItemProvider : HashProviderBase
    {
        private const int WaitBufferInTics = 2;

        protected override void HashSampleSet(ref ProcessCounters counters)
        {
            var lastCount = 0;
            var unchangedTics = 0;

            var threadWorker = new ThreadWorker
            {
                WorkQueue = TaskList
            };

            threadWorker.ProcessAllWorkItems(ref counters);


            while (counters.ProcessedItems <  NumberOfItemsToProcess && unchangedTics < WaitBufferInTics)
            {
                if (lastCount == counters.ProcessedItems && lastCount > 0)
                {
                    unchangedTics++;
                }

                lastCount = counters.ProcessedItems;

                if (unchangedTics == WaitBufferInTics)
                {
                    Console.WriteLine("Exiting all pending work has been completed. Warning some items have not been processed!");
                }

                Thread.Sleep(1000);
            }
        }
    }
}
