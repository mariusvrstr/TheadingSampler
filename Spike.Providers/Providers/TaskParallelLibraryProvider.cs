
namespace Spike.Providers.Providers
{
    using System;
    using Common;

    public class TaskParallelLibraryProvider : ThreadingProviderBase
    {
        //TODO: Task Parallel Libriary (TPL) - Task Based Asynchronous Programming
        //https://msdn.microsoft.com/en-us/library/dd537609(v=vs.110).aspx

        protected override void HashSampleSet(ref ProcessCounters counters)
        {
            throw new NotImplementedException();
        }
    }
}
