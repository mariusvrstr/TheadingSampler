
namespace Spike.Providers.Common
{
    public class SampleResult
    {
        public int NumberOfThreads { get; set; }

        public int SucceededItems { get; set; }

        public int FailedItems { get; set; }

        public int NumberOfItemsProcessed => SucceededItems + FailedItems;

        public string Duration { get; set; }
    }
}
