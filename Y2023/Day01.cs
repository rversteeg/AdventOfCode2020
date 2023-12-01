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
        return input.Select(line => FirstNumber(line) * 10 + LastNumber(line)).Sum();
    }

    private int FirstNumber(ReadOnlySpan<char> input)
    {
        while (input.Length > 0)
        {
            if (input.StartsWith("1") || input.StartsWith("one"))
                return 1;
            if (input.StartsWith("2") || input.StartsWith("two"))
                return 2;
            if (input.StartsWith("3") || input.StartsWith("three"))
                return 3;
            if (input.StartsWith("4") || input.StartsWith("four"))
                return 4;
            if (input.StartsWith("5") || input.StartsWith("five"))
                return 5;
            if (input.StartsWith("6") || input.StartsWith("six"))
                return 6;
            if (input.StartsWith("7") || input.StartsWith("seven"))
                return 7;
            if (input.StartsWith("8") || input.StartsWith("eight"))
                return 8;
            if (input.StartsWith("9") || input.StartsWith("nine"))
                return 9;

            input = input[1..];
        }

        throw new Exception("Invalid");
    }
    
    private int LastNumber(ReadOnlySpan<char> line)
    {
        for (int i = 1; i <= line.Length; i++)
        {
            var input = line.Slice(line.Length - i);
            if (input.StartsWith("1") || input.StartsWith("one"))
                return 1;
            if (input.StartsWith("2") || input.StartsWith("two"))
                return 2;
            if (input.StartsWith("3") || input.StartsWith("three"))
                return 3;
            if (input.StartsWith("4") || input.StartsWith("four"))
                return 4;
            if (input.StartsWith("5") || input.StartsWith("five"))
                return 5;
            if (input.StartsWith("6") || input.StartsWith("six"))
                return 6;
            if (input.StartsWith("7") || input.StartsWith("seven"))
                return 7;
            if (input.StartsWith("8") || input.StartsWith("eight"))
                return 8;
            if (input.StartsWith("9") || input.StartsWith("nine"))
                return 9;
        }

        throw new Exception("Invalid");
    }
}