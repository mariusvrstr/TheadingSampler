
namespace Spike.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Providers.Providers;

    [TestClass]
    public class SmallQuantityThreadingTests
    {
        private const int SmallSampleSize = 100;

        [TestMethod]
        public void SingleThreadSmallQuantityTest()
        {
            var provider = new SingleThreadProvider();
            var result = provider.RunSampleTest(SmallSampleSize);

            Assert.AreEqual(SmallSampleSize, result.NumberOfItemsProcessed);
            Assert.AreEqual(0, result.FailedItems);
            Assert.AreEqual(1, result.NumberOfThreads);
        }

        [TestMethod]
        public void MultiThreadUsingManagedThreadPoolSmallQuantityTest()
        {
            var provider = new ManagedThreadPoolProvider();
            var result = provider.RunSampleTest(SmallSampleSize);

            Assert.AreEqual(SmallSampleSize, result.NumberOfItemsProcessed);
            Assert.IsTrue(result.NumberOfThreads > 1);
            Assert.AreEqual(0, result.FailedItems);
        }

        [TestMethod]
        public void MultiThreadUsingCustomThreadPoolSmallQuantityTest()
        {
            var threadPoolSize = 5;
            var provider = new ManagedThreadPoolProvider();
            var result = provider.RunSampleTest(SmallSampleSize);

            Assert.AreEqual(SmallSampleSize, result.NumberOfItemsProcessed);
            Assert.AreEqual(0, result.FailedItems);
            Assert.IsTrue(result.NumberOfThreads <= threadPoolSize);
            Assert.IsTrue(result.NumberOfThreads > 1);
        }

        [TestMethod]
        public void ParallelProgrammingUsingTplSmallQuantityTest()
        {
            var provider = new TaskParallelLibraryProvider();
            var result = provider.RunSampleTest(SmallSampleSize);

            Assert.AreEqual(SmallSampleSize, result.NumberOfItemsProcessed);
            Assert.AreEqual(0, result.FailedItems);
            Assert.IsTrue(result.NumberOfThreads > 1);
        }
    }
}
