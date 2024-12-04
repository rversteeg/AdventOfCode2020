using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2024;

public class Day02 : PuzzleWithIntLists
{
    public override object SolvePart1(List<int[]> input) => input.Where(IsValid).Count();

    private bool IsValid(int[] line)
    {
        var sign = line[0] > line[1] ? 1 : -1;
        for (int i = 0; i < line.Length - 1; i++)
        {
            var diff = (line[i] - line[i + 1]) * sign;
            if (diff is < 1 or > 3)
            {
                return false;
            }
        }

        return true;
    }

    public override object SolvePart2(List<int[]> input) => input.Where(IsValid2).Count();

    private bool IsValid2(int[] line)
    {
        var sign = line[0] > line[1] ? 1 : -1;
        for (int i = 0; i < line.Length - 1; i++)
        {
            var diff = (line[i] - line[i + 1]) * sign;
            if (diff is < 1 or > 3)
            {
                return IsValid(Remove(line, i)) || IsValid(Remove(line, i + 1)) || IsValid(Remove(line, i - 1));
            }
        }
        return true;
    }

    private int[] Remove(int[] input, int itemToRemove)
        => input.Take(itemToRemove).Concat(input.Skip(itemToRemove + 1)).ToArray();
}