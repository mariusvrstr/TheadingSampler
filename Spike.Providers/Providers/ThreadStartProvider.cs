
namespace Spike.Providers.Providers
{
    using System;
    using Common;

    public class ThreadStartProvider : HashProviderBase
    {
        public int ThreadPoolSize { get; private set; }

        public ThreadStartProvider(int threadPoolSize)
        {
            ThreadPoolSize = threadPoolSize;
        }
        
        protected override void HashSampleSet(ref ProcessCounters counters)
        {
            throw new NotImplementedException();
        }
    }
}
