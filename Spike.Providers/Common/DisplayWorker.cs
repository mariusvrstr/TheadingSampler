using System;

namespace Spike.Providers.Common
{
    public class DisplayWorker
    {
        public static void WriteSampleToScreen(int position, string hashValue)
        {
            if (!SampleWorker.IsSample(position)) return;

            Console.WriteLine($"Sample [{position}] >> Hashed to [{hashValue}]");
        }

        public static void DisplayResultSummary(int success, int failures, int threads, string time)
        {
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine($"Batch Completed. Success [{success}] Failed [{failures}] using [{threads}] Threads. Duration [{time}]");
        }
    }
}
