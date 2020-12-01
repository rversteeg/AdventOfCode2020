using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var latestDay = GetSolutions().OrderByDescending(x=>x.Day).FirstOrDefault();

            if (latestDay == null)
            {
                Console.WriteLine("No solutions in project");
                return;
            }

            Console.WriteLine($"Part1: {latestDay.SolvePart1()}");
            Console.WriteLine($"Part2: {latestDay.SolvePart2()}");
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
