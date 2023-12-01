using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Util;
using CommandLine;

namespace AdventOfCode.Y2023;

public class Day01 : PuzzleSolutionWithLinesInput
{

    public override object SolvePart1(string[] input)
    {
        var numbers = input.Select(line =>
            line.Where(x => Char.IsDigit(x)));

        return numbers.Select(line => Int32.Parse($"{line.First()}{line.Last()}")).Sum();
    }

    private (string, int)[] values =
    {
        ("1", 1),
        ("2", 2),
        ("3", 3),
        ("4", 4),
        ("5", 5),
        ("6", 6),
        ("7", 7),
        ("8", 8),
        ("9", 9),
        ("one", 1),
        ("two", 2),
        ("three", 3),
        ("four", 4),
        ("five", 5),
        ("six", 6),
        ("seven", 7),
        ("eight", 8),
        ("nine", 9)
    };

    public override object SolvePart2(string[] input)
    {
        return input.Select(line => ValueIndexes(line).OrderBy(x => x.Item1))
            .Select(ind => $"{ind.First().Item2}{ind.Last().Item2}")
            .Select(Int32.Parse)
            .Sum();
    }

    private IEnumerable<(int, int)> ValueIndexes(string input)
    {
        return values.SelectMany(x => Indexes(x.Item1, input).Select(y => (y, x.Item2)));
    }
    
    private IEnumerable<int> Indexes(char needle, string haystack)
    {
        yield return haystack.IndexOf(needle);
        yield return haystack.LastIndexOf(needle);
    }

    private IEnumerable<int> Indexes(string needle, string haystack)
    {
        yield return haystack.IndexOf(needle);
        yield return haystack.LastIndexOf(needle);
    }
}