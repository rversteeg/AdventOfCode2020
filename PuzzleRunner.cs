using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace AdventOfCode;

public class PuzzleRunner(int Year)
{
    public void RunAllDays()
    {
        var allDays = GetSolutions(Year).OrderByDescending(x => x.Day);

        foreach (var day in allDays)
        {
            RunDay(day);
        }
    }

    private static void RunDay(IPuzzleSolution day)
    {
        var start = Stopwatch.GetTimestamp();
        var part1Answer = day.SolvePart1();
        var finishPart1 = Stopwatch.GetTimestamp();
        var part2Answer = day.SolvePart2();
        var finishPart2 = Stopwatch.GetTimestamp();
        Console.WriteLine($@"
-- Day {day.Day:D2}
Part1: {part1Answer} ( Took {Stopwatch.GetElapsedTime(start, finishPart1)} )
Part2: {part2Answer} ( Took {Stopwatch.GetElapsedTime(finishPart1, finishPart2)} )");
    }

    public void RunLast()
    {
        var lastDay = GetSolutions(Year).OrderByDescending(x => x.Day).FirstOrDefault();
        if (lastDay is not null)
        {
            RunDay(lastDay);
        }
    }

    private static IEnumerable<IPuzzleSolution> GetSolutions(int year)
    {
        return
            from type in Assembly.GetExecutingAssembly().DefinedTypes
            where type.ImplementedInterfaces.Contains(typeof(IPuzzleSolution)) && !type.IsAbstract
            let solution = (IPuzzleSolution)Activator.CreateInstance(type)
            where solution.Year == year
            select solution;
    }
}