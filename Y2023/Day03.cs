using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AdventOfCode.Util;
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
        var total = 0;
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                if(input[y][x] != '*')
                    continue;

                var adjacentParts = GetAdjacentParts(x, y, input).ToList();

                if (adjacentParts.Count == 2)
                    total += adjacentParts[0] * adjacentParts[1];
            }
        }

        return total;
    }

    private IEnumerable<int> GetAdjacentParts(int x, int y, char[][] input)
    {
        yield break;
    }
}