
namespace Spike.Providers
{
    using System;
    using System.Collections.Generic;
    using Common;

    public abstract class ThreadingProviderBase
    {
        protected List<WorkTask> TaskList { get; set; }
        protected int NumberOfItemsToProcess { get; set; }

        private static readonly Func<int, string> HashIndividual = num =>
        {
            var valueToHash = $"The test string sample for item [{num}].";
            var hashedValue = HashWorker.ShaHash(valueToHash);
            DisplayWorker.WriteSampleToScreen(num, hashedValue);

            return hashedValue;
        };

        public SampleResult RunSampleTest(int sampleSize)
        {
            Console.WriteLine($"Attempting to hash [{sampleSize}] values in stress test:");
            Console.WriteLine();

            TaskList = CreateWorkTasks(sampleSize);
            var stopwatch = new StopwatchWorker();
            stopwatch.Start();

            var processCounters = new ProcessCounters();
            HashSampleSet(ref processCounters);

            stopwatch.Stop();
            var result = new SampleResult
            {
                NumberOfThreads = processCounters.ThreadCount,
                SucceededItems = processCounters.SuccessCount,
                FailedItems = processCounters.FailureCount,
                Duration = stopwatch.ElapsedTime()
            };

            DisplayWorker.DisplayResultSummary(result.SucceededItems, result.FailedItems, result.NumberOfThreads, result.Duration);

            return result;
        }

        private List<WorkTask> CreateWorkTasks(int numberOfItemsToCreate)
        {
            var workList = new List<WorkTask>();
            NumberOfItemsToProcess = numberOfItemsToCreate;

            for (var k = 1; k <= numberOfItemsToCreate; k++)
            {
                workList.Add(new WorkTask(k, HashIndividual));
            }

            return workList;
        }

        protected abstract void HashSampleSet(ref ProcessCounters counters);
    }
}
