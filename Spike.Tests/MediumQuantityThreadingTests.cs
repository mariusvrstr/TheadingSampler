
namespace Spike.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Providers.Providers;

    [TestClass]
    public class ThreadingTests
    {
        private const int MediumSampleSize = 25000;
        
        [TestMethod]
        public void SingleThreadMediumQuanityTest()
        {
            var provider = new SingleThreadProvider();
            var result = provider.RunSampleTest(MediumSampleSize);

            Assert.AreEqual(MediumSampleSize, result.NumberOfItemsProcessed);
            Assert.AreEqual(0, result.FailedItems);
            Assert.AreEqual(1, result.NumberOfThreads);
        }

        [TestMethod]
        public void MultiThreadUsingManagedThreadPoolMediumQuantityTest()
        {
            var provider = new ManagedThreadPoolProvider();
            var result = provider.RunSampleTest(MediumSampleSize);

            Assert.AreEqual(MediumSampleSize, result.NumberOfItemsProcessed);
            Assert.IsTrue(result.NumberOfThreads > 1);
            Assert.AreEqual(0, result.FailedItems);
        }

        [TestMethod]
        public void MultiThreadUsingCustomThreadPoolMediumQuantityTest()
        {
            var threadPoolSize = 5;
            var provider = new ManagedThreadPoolProvider();
            var result = provider.RunSampleTest(MediumSampleSize);

            Assert.AreEqual(MediumSampleSize, result.NumberOfItemsProcessed);
            Assert.AreEqual(0, result.FailedItems);
            Assert.IsTrue(result.NumberOfThreads <= threadPoolSize);
            Assert.IsTrue(result.NumberOfThreads > 1);
        }

        [TestMethod]
        public void ParallelProgrammingUsingTplMediumQuantityTest()
        {
            var provider = new TaskParallelLibraryProvider();
            var result = provider.RunSampleTest(MediumSampleSize);

            Assert.AreEqual(MediumSampleSize, result.NumberOfItemsProcessed);
            Assert.AreEqual(0, result.FailedItems);
            Assert.IsTrue(result.NumberOfThreads > 1);
        }
    }
}
