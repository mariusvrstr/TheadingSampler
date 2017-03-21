
namespace Spike.Providers.Providers
{
    using System;
    using Common;

    public class SingleThreadProvider : HashProviderBase
    {
        protected override void HashSampleSet(ref ProcessCounters counters)
        {
            counters.ThreadCount++;

            foreach (var task in TaskList)
            {
                try
                {
                    task.Execute();
                    counters.SuccessCount++;
                }
                catch (Exception ex)
                {
                    counters.FailureCount++;
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
