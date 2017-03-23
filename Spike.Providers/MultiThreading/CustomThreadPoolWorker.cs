
namespace Spike.Providers.MultiThreading
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Common;

    public class CustomThreadPoolWorker
    {
        private ProcessCounters InternalCounter { get; set; }
        public Thread TaskScheduler { get; set; }
        private Queue<WorkTask> WorkQueue { get; } = new Queue<WorkTask>();
        public int MaxThreads { get; }
        private List<Thread> ThreadPool { get; } = new List<Thread>();

        public CustomThreadPoolWorker(int maximumThreads)
        {
            MaxThreads = maximumThreads;
        }

        public void InitializeThreadPool(ref ProcessCounters counter)
        {
            InternalCounter = counter;

            TaskScheduler = new Thread(() =>
            {
                while (WorkQueue.Any())
                {
                    Thread activeThread ;

                    lock (WorkQueue)
                    {
                        activeThread = PrepareNewThread(WorkQueue.Peek());

                        if (activeThread != null)
                        {
                            WorkQueue.Dequeue();
                        }
                    }

                    if (activeThread == null)
                    {
                        Thread.Sleep(100);
                        continue;
                    }

                    activeThread.Start();
                }
            });

            TaskScheduler.Start();
        }

        public void AddWork(WorkTask task)
        {
            WorkQueue.Enqueue(task);
        }

        public void AddWork(IEnumerable<WorkTask> tasks)
        {
            foreach (var task in tasks)
            {
                WorkQueue.Enqueue(task);
            }
        }

        public void WorkWrapper(object obj)
        {
            var workItem = (WorkTask) obj;

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
        }

        public Thread PrepareNewThread(WorkTask task)
        {
            var newWorkerThread = new Thread(() =>
            {
                WorkWrapper(task);
            });

            for (var k = 0; k < MaxThreads; k++)
            {
                if (ThreadPool.Count <= k)
                {
                    lock (ThreadPool)
                    {
                        ThreadPool.Add(newWorkerThread);
                    }

                    lock (InternalCounter)
                    {
                        InternalCounter.ThreadCount++;
                    }

                    return newWorkerThread;
                }

                lock (ThreadPool)
                {
                    var currentThread = ThreadPool[k];
                    if (currentThread != null && !IsThreadAvailable(currentThread.ThreadState)) continue;

                    return ThreadPool[k] = newWorkerThread;
                }
            }

            return null;
        }

        private static bool IsThreadAvailable(ThreadState threadState)
        {
            switch (threadState)
            {
                case ThreadState.Aborted:
                case ThreadState.Suspended:
                case ThreadState.Stopped:
                    return true;

                default: return false;
            }
        }
    }
}
