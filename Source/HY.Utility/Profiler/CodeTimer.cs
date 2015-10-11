namespace HY.Utitily.Profiler
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public static class CodeTimer
    {
        public class TimerResult
        {
            public string MethodName { get; set; }

            public long TimeElapsed { get; set; }

            public ulong CPUCycles { get; set; }

            public int[] GCCounts { get; set; }

            public void TraceLog()
            {
                ConsoleColor currentForeColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(MethodName);

                Console.ForegroundColor = currentForeColor;
                Console.WriteLine("\tTime Elapsed:\t" + TimeElapsed.ToString("N0") + "ms");
                Console.WriteLine("\tCPU Cycles:\t" + CPUCycles.ToString("N0"));

                for (int i = 0; i < GCCounts.Length; i++)
                {
                    int count = GC.CollectionCount(i) - GCCounts[i];
                    Console.WriteLine("\tGen " + i + ": \t\t" + count);
                }

                Console.WriteLine();
            }
        }

        public static void Initialize()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            Time(string.Empty, 1, () => { });
        }

        public static TimerResult Time(string name, int iteration, Action action)
        {
            // 1.
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

            var gcCounts = new int[GC.MaxGeneration + 1];
            for (var i = 0; i <= GC.MaxGeneration; i++)
            {
                gcCounts[i] = GC.CollectionCount(i);
            }

            // 2.
            var watch = new Stopwatch();
            watch.Start();
            var clycleCount = GetCycleCount();
            for (var i = 0; i < iteration; i++)
            {
                action();
            }

            var cpuCycles = GetCycleCount() - clycleCount;
            watch.Stop();

            for (var i = 0; i <= GC.MaxGeneration; i++)
            {
                gcCounts[i] = GC.CollectionCount(i) - gcCounts[i];
            }

            return new TimerResult
            {
                MethodName = name,
                TimeElapsed = watch.ElapsedMilliseconds,
                CPUCycles = cpuCycles,
                GCCounts = gcCounts
            };
        }

        private static ulong GetCycleCount()
        {
            ulong cycleCount = 0;
            QueryThreadCycleTime(GetCurrentThread(), ref cycleCount);
            return cycleCount;
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool QueryThreadCycleTime(IntPtr threadHandle, ref ulong cycleTime);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentThread();
    }
}
