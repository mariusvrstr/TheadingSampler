
using System;
using Spike.Providers;
using Spike.Providers.Providers;
using Spike.Tests.Contracts;

namespace Spike.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class VolumeTest
    {
        private const int SizeOfManualThreadPoolIfApplicable = 5;

        [TestMethod]
        public void HighVolumeThreadingTest()
        {
            var numberOfItemsToProcess = 100000;
            var threadType = ThreadingType.ManagedThreadPool;

            HashProviderBase provider;

            switch (threadType)
            {
                    case ThreadingType.SingleThread: provider = new SingleThreadProvider();
                        break;
                    case ThreadingType.ManagedThreadPool: provider = new QueueUserWorkItemProvider();
                        break;
                    case ThreadingType.ManualThreadPool: provider = new ThreadStartProvider(SizeOfManualThreadPoolIfApplicable);
                        break;
                default: throw new InvalidOperationException();
            }

            var result = provider.RunSampleTest(numberOfItemsToProcess);
        }
    }
}
