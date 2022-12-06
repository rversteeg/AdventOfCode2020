using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2022;

public class Day06 : PuzzleSolutionWithTextInput
{
    public override object SolvePart1(string input)
        => FindMarker(input, 4);

    public override object SolvePart2(string input)
        => FindMarker(input, 14);

    private static int FindMarker(string input, int markerLength)
        => Enumerable.Range(0, input.Length - markerLength)
            .First(x => input[x..(x + markerLength)].Distinct().Count() == markerLength) + markerLength;
}