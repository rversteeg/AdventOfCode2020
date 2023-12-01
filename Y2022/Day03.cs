using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2022;

public class Day03 : PuzzleSolutionWithLinesInput
{
    private static int GetPriority(char duplicate) =>
        duplicate switch
        {
            >= 'a' and <= 'z' => duplicate - 'a' + 1,
            >= 'A' and <= 'Z' => duplicate - 'A' + 27,
        };
    
    public override object SolvePart1(string[] input)
        => input.Select(x => GetPriority(x[..(x.Length / 2)].Intersect(x[(x.Length / 2)..]).Single())).Sum();
    public override object SolvePart2(string[] input)
        => Enumerable.Range(0, input.Length / 3)
            .Select(x => GetPriority(input[x * 3].Intersect(input[x * 3 + 1]).Intersect(input[x * 3 + 2]).Single()))
            .Sum();
}