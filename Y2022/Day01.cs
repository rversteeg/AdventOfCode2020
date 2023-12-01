using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2022;

public class Day01 : PuzzleSolutionWithLinesInput
{
    private IEnumerable<int> Groups(string[] input)
    {
        var current = 0;
        foreach (var line in input)
        {
            if (String.IsNullOrWhiteSpace(line))
            {
                yield return current;
                current = 0;
                continue;
            }

            current += int.Parse(line);
        }
    }

    public override object SolvePart1(string[] input)
        => Groups(input).Max();

    public override object SolvePart2(string[] input)
        => Groups(input).OrderByDescending(x=>x).Take(3).Sum();
}