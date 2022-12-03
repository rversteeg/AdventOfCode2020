using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace AdventOfCode
{
    internal static class Program
    {
        private static void Main()
        {
            if (RunBenchmark)
            {
                BenchmarkRunner.Run(Assembly.GetExecutingAssembly());
            }
            else
            {
                var allDays = GetSolutions(2022).OrderByDescending(x => x.Day);

                foreach (var day in allDays)
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    var part1Answer = day.SolvePart1();
                    var part1Time = sw.Elapsed;
                    sw.Restart();
                    var part2Answer = day.SolvePart2();
                    var part2Time = sw.Elapsed;
                    sw.Stop();
                    Console.WriteLine($@"-- Day {day.Day:D2}
Part1: {part1Answer} ( Took {part1Time} )
Part2: {part2Answer} ( Took {part2Time} )
");
                }
            }
        }

        private static bool RunBenchmark
        {
            get
            {
#if DEBUG
                return false;
#else
                return true;
#endif

            }
        }

        private static IEnumerable<IPuzzleSolution> GetSolutions(int year)
        {
            return 
                from type in Assembly.GetExecutingAssembly().DefinedTypes
                where type.ImplementedInterfaces.Contains(typeof(IPuzzleSolution)) && !type.IsAbstract
                let solution = (IPuzzleSolution) Activator.CreateInstance(type)
                where solution.Year == year
                select solution;
        }

        public static readonly IDictionary<int, IPuzzleSolution> Solutions = GetSolutions(2021).ToDictionary(x => x.Day);
    }

    public class Benchmark
    {
        [Params(6)]
        public int Day { get; set; }

        [Benchmark]
        public void RunPart1()
        {
            Program.Solutions[Day].SolvePart1();
        }

        [Benchmark]
        public void RunPart2()
        {
            Program.Solutions[Day].SolvePart2();
        }
    }
}
