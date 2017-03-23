
namespace Spike.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using Providers;
    using Providers.Providers;
    using Contracts;

    [TestClass]
    public class VolumeTest
    {
        private const int SizeOfManualThreadPoolIfApplicable = 5;

        [TestMethod]
        public void HighVolumeThreadingTest()
        {
            var numberOfItemsToProcess = 50000;
            var threadType = ThreadingType.CustomThreadPool;

            ThreadingProviderBase provider;

            switch (threadType)
            {
                    case ThreadingType.SingleThread: provider = new SingleThreadProvider();
                        break;
                    case ThreadingType.ManagedThreadPool: provider = new ManagedThreadPoolProvider();
                        break;
                    case ThreadingType.CustomThreadPool: provider = new CustomThreadPoolProvider(SizeOfManualThreadPoolIfApplicable);
                        break;
                    default: throw new InvalidOperationException();
            }

            var result = provider.RunSampleTest(numberOfItemsToProcess);

            Assert.AreEqual(numberOfItemsToProcess, result.NumberOfItemsProcessed);
        }
    }
}
