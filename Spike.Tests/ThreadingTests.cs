
namespace Spike.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Providers;
    using Providers.Providers;

    [TestClass]
    public class ThreadingTests
    {
        private const int SmallSampleSize = 20;

        [TestMethod]
        public void SingleThreadSmallQuanityTest()
        {
            var provider = new SingleThreadProvider();
            var result = provider.RunSampleTest(SmallSampleSize);

            Assert.AreEqual(SmallSampleSize, result.NumberOfItemsProcessed);
            Assert.AreEqual(0, result.FailedItems);
            Assert.AreEqual(1, result.NumberOfThreads);
        }

        [TestMethod]
        public void MultiThreadUsingManagedThreadQueueSmallQuantityTest()
        {
            var provider = new QueueUserWorkItemProvider();
            var result = provider.RunSampleTest(SmallSampleSize);

            Assert.AreEqual(SmallSampleSize, result.NumberOfItemsProcessed);
            Assert.IsTrue(result.NumberOfThreads > 1);
            Assert.AreEqual(0, result.FailedItems);
        }

        [TestMethod]
        public void MultiThreadUsingThreadStartSmallQuantityTest()
        {
            var threadPoolSize = 5;
            var provider = new QueueUserWorkItemProvider();
            var result = provider.RunSampleTest(SmallSampleSize);

            Assert.AreEqual(SmallSampleSize, result.NumberOfItemsProcessed);
            Assert.AreEqual(0, result.FailedItems);
            Assert.AreEqual(threadPoolSize, result.NumberOfThreads);
        }
    }
}
