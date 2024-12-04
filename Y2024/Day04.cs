using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2024;

public class Day04 : PuzzleWithCharGrid
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
        => EnumerableExtensions.RangeGrid(0, 0, input[0].Length, input.Length)
            .Select(pos=>Directions.Count(dir => IsMatch(input, pos.X, pos.Y, dir.xStep, dir.yStep)))
            .Sum();

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
        => EnumerableExtensions.RangeGrid(1, 1, input[0].Length-2, input.Length-2)
            .Count(pos => input[pos.Y][pos.X] == 'A'
                          && (input[pos.Y - 1][pos.X - 1] == 'M' && input[pos.Y + 1][pos.X + 1] == 'S' ||
                              input[pos.Y - 1][pos.X - 1] == 'S' && input[pos.Y + 1][pos.X + 1] == 'M')
                          && (input[pos.Y - 1][pos.X + 1] == 'M' && input[pos.Y + 1][pos.X - 1] == 'S' ||
                              input[pos.Y - 1][pos.X + 1] == 'S' && input[pos.Y + 1][pos.X - 1] == 'M'));
}