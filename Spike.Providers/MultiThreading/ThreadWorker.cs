
namespace Spike.Providers.MultiThreading
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Common;

    public class ThreadWorker
    {
        private ProcessCounters InternalCounter { get; set; }

        public List<WorkTask> WorkQueue { get; set; }
        private List<int> ThreadsUsed { get; set; }
       
        public void WorkWrapper(object obj)
        {
            var workItem = (WorkTask)obj;

            try
            {
                workItem.Execute();

                lock (InternalCounter)
                {
                    InternalCounter.SuccessCount++;
                }
            }
            catch (Exception ex)
            {
                lock (InternalCounter)
                {
                    InternalCounter.FailureCount--;
                }
                    
                Console.WriteLine($"Failed to excecute work: {ex.Message}", InternalCounter.FailureCount);
            }

            if (!ThreadsUsed.Contains(Thread.CurrentThread.ManagedThreadId))
            {
                InternalCounter.ThreadCount++;
                ThreadsUsed.Add(Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("New thread is joining the party. Thread {0} consumes {1}", Thread.CurrentThread.GetHashCode(), ((WorkTask)obj).Id);
            }
        }
        
        public void ProcessWorkItem(WorkTask workItem)
        {
            ThreadPool.QueueUserWorkItem(WorkWrapper, workItem);
            WorkQueue.Remove(workItem);
        }

        public void ProcessAllWorkItems(ref ProcessCounters counter)
        {
            InternalCounter = counter;
            ThreadsUsed = new List<int>();

            while (WorkQueue.Any())
            {
                lock (WorkQueue)
                {
                    var workItem = WorkQueue.FirstOrDefault();
                    ProcessWorkItem(workItem);
                }
            }
        }
    }
}
