using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2021;

public class Day01 : PuzzleWithInt32Input
{
    public override object SolvePart1(int[] input)
    {
        var result = 0;
        for (int i = 0; i < input.Length - 1; i++)
        {
            if (input[i] < input[i + 1])
                ++result;
        }
            
        return result;
    }

    public override object SolvePart2(int[] input)
    {
        var result = 0;
        for (int i = 0; i < input.Length - 3; i++)
        {
            if (input[i..(i + 3)].Sum() < input[(i+1)..(i + 4)].Sum())
                ++result;
        }

        return result;
    }
}