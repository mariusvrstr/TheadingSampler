
namespace Spike.Providers.Common
{
    public class ProcessCounters
    {
        public int SuccessCount { get; set; }
        
        public int FailureCount { get; set; }

        public int ThreadCount { get; set; }

        public int ProcessedItems => SuccessCount + FailureCount;
    }
}
