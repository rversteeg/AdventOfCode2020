using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
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

        private static IEnumerable<IPuzzleSolution> GetSolutions()
        {
            var solutions = 
                from type in Assembly.GetExecutingAssembly().DefinedTypes
                where type.ImplementedInterfaces.Contains(typeof(IPuzzleSolution)) && !type.IsAbstract
                select (IPuzzleSolution)Activator.CreateInstance(type);

            return solutions.ToList();
        }
    }
}
