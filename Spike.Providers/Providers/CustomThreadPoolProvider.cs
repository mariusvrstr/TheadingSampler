
using System.Collections.Generic;

namespace Spike.Providers.Providers
{
    using System;
    using System.Threading;
    using MultiThreading;
    using Common;

    public class CustomThreadPoolProvider : ThreadingProviderBase
    {
        public int ThreadPoolSize { get; private set; }
        private const int WaitBufferInTics = 2;
        

        public CustomThreadPoolProvider(int threadPoolSize)
        {
            ThreadPoolSize = threadPoolSize;
        }
      
        
        protected override void HashSampleSet(ref ProcessCounters counters)
        {
            var lastCount = 0;
            var unchangedTics = 0;
            var threadWorker = new CustomThreadPoolWorker(ThreadPoolSize);

            threadWorker.AddWork(TaskList);
            threadWorker.InitializeThreadPool(ref counters);
            
            while (counters.ProcessedItems < NumberOfItemsToProcess && unchangedTics < WaitBufferInTics)
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
