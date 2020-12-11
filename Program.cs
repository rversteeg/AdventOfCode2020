using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "benchmark")
            {
                BenchmarkRunner.Run(Assembly.GetExecutingAssembly());
            }
            else
            {
                var allDays = GetSolutions().OrderByDescending(x => x.Day);

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

        private static IEnumerable<IPuzzleSolution> GetSolutions()
        {
            var solutions = 
                from type in Assembly.GetExecutingAssembly().DefinedTypes
                where type.ImplementedInterfaces.Contains(typeof(IPuzzleSolution)) && !type.IsAbstract
                select (IPuzzleSolution)Activator.CreateInstance(type);

            return solutions.ToList();
        }

        public static IDictionary<int, IPuzzleSolution> Solutions = GetSolutions().ToDictionary(x => x.Day);
    }

    public class Benchmark
    {
        //[Params(1,2,3,4,5,6,7,8,9,10,11)]
        [Params(11)]
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
