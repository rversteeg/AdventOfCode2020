using System;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2023;

public class Day01 : PuzzleSolutionWithLinesInput
{

    public override object SolvePart1(string[] input)
    {
        var numbers = input.Select(line =>
            line.Where(x => Char.IsDigit(x)));

        return numbers.Select(line => Int32.Parse($"{line.First()}{line.Last()}")).Sum();
    }

    private readonly (string, int)[] _values =
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
        return input.Select(line => FirstNumber(line) * 10 + LastNumber(line)).Sum();
    }

    private int FirstNumber(ReadOnlySpan<char> input)
    {
        while (input.Length > 0)
        {
            foreach (var val in _values)
            {
                if (input.StartsWith(val.Item1))
                    return val.Item2;
            }

            input = input[1..];
        }

        throw new Exception("Invalid");
    }
    
    private int LastNumber(ReadOnlySpan<char> line)
    {
        for (int i = 1; i <= line.Length; i++)
        {
            var input = line.Slice(line.Length - i);
            foreach (var val in _values)
            {
                if (input.StartsWith(val.Item1))
                    return val.Item2;
            }
        }

        throw new Exception("Invalid");
    }
}