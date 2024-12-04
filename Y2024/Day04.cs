using System;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2024;

public partial class Day04 : PuzzleWithCharGrid
{
    private static readonly (int xStep, int yStep)[] Directions =
    [
        (0, 1), // down
        (-1, 1), // left down
        (-1, 0), // left
        (-1, -1), // left up
        (0, -1), // up
        (1, -1), // right up
        (1, 0), // right
        (1, 1) // right down
    ];

    public override object SolvePart1(char[][] input)
        => (from x in Enumerable.Range(0, input[0].Length)
            from y in Enumerable.Range(0, input.Length)
            select Directions.Count(dir => IsMatch(input, x, y, dir.xStep, dir.yStep))).Sum();

    private const string TextToFind = "XMAS";

    private bool IsMatch(char[][] grid, int x, int y, int xStep, int yStep)
    {
        var endX = x + 3 * xStep;
        var endY = y + 3 * yStep; 
        if ( endX >= grid[0].Length || endX < 0 || endY >= grid.Length || endY < 0)
            return false;
        return !TextToFind.Where((t, i) => grid[y + i * yStep][x + i * xStep] != t).Any();
    }


    public override object SolvePart2(char[][] input)
        => (from x in Enumerable.Range(1, input[0].Length - 2)
            from y in Enumerable.Range(1, input.Length - 2)
            where input[y][x] == 'A'
                  && (input[y - 1][x - 1] == 'M' && input[y + 1][x + 1] == 'S' ||
                      input[y - 1][x - 1] == 'S' && input[y + 1][x + 1] == 'M')
                  && (input[y - 1][x + 1] == 'M' && input[y + 1][x - 1] == 'S' ||
                      input[y - 1][x + 1] == 'S' && input[y + 1][x - 1] == 'M')
            select 1).Count();
}