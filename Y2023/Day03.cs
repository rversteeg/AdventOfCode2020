using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AdventOfCode.Util;
using AdventOfCode.Y2021;
using Xunit;

namespace AdventOfCode.Y2023;

public class Day03 : PuzzleWithCharGrid
{
    public override object SolvePart1(char[][] input)
    {
        var total = 0;
        for (int y = 0; y < input.Length; y++)
        {
            bool isAdjecent = false;
            int value = 0;
            
            for (int x = 0; x < input[0].Length; x++)
            {
                if (!char.IsDigit(input[y][x]))
                {
                    if (value > 0)
                    {
                        if (isAdjecent)
                            total += value;
                        value = 0;
                        isAdjecent = false;
                    }
                    continue;
                }
                value = value * 10 + (input[y][x] - '0');
                isAdjecent = isAdjecent || IsNextToSymbol(x, y, input);
            }
            
            if (value > 0)
            {
                if (isAdjecent)
                    total += value;
            }
        }

        return total;
    }

    private static bool IsNextToSymbol(int x, int y, char[][] input)
    {
        return IsSymbol(x - 1, y - 1, input)
            || IsSymbol(x, y - 1, input)
            || IsSymbol(x + 1, y - 1, input)
            || IsSymbol(x - 1, y, input)
            || IsSymbol(x + 1, y, input)
            || IsSymbol(x - 1, y + 1, input)
            || IsSymbol(x, y + 1, input)
            || IsSymbol(x + 1, y + 1, input);
    }

    private static bool IsSymbol(int x, int y, char[][] input)
    {
        return x > 0 && y > 0 && x < input[0].Length && y < input.Length
               && !char.IsDigit(input[y][x]) && input[y][x] != '.';
    }

    public override object SolvePart2(char[][] input)
    {
        var lookup = GetValues(input).SelectMany(x => x.Edges.Select(y => (y, x.Value))).ToLookup(x => x.y, x => x.Value);
        
        var total = 0;
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                if(input[y][x] != '*')
                    continue;
                var point = new Point(x, y);
                if (lookup.Contains(point) && lookup[point].Count() == 2)
                {
                    total += lookup[point].First() * lookup[point].Last();
                }
            }
        }

        return total;
    }

    private IEnumerable<(int Value, List<Point> Edges)> GetValues(char[][] input)
    {
        for (int y = 0; y < input.Length; y++)
        {
            int value = 0;
            List<Point> edges = new();
            for (int x = 0; x <= input[0].Length; x++)
            {
                if (x == input[0].Length || !char.IsDigit(input[y][x]))
                {
                    if (value > 0)
                    {
                        edges.Add(new Point(x + 1, y -1));
                        edges.Add(new Point(x + 1, y));
                        edges.Add(new Point(x + 1, y + 1));
                        yield return (value, edges);
                        edges = new List<Point>();
                        value = 0;
                    }
                    continue;
                }

                if (value == 0)
                {
                    edges.Add(new Point(x - 1, y -1));
                    edges.Add(new Point(x - 1, y));
                    edges.Add(new Point(x - 1, y + 1));
                }

                edges.Add(new Point(x, y - 1));
                edges.Add(new Point(x, y + 1));
                value = value * 10 + (input[y][x] - '0');
            }
        }
    }

    private IEnumerable<int> GetAdjacentParts(int x, int y, char[][] input)
    {
        yield break;
    }
}