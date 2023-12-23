using System;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2023;

public class Day09 : PuzzleSolutionWithParsedInput<int[][]>
{
    public override object SolvePart1(int[][] input)
        => input.Select(GetNext).Sum();
    
    public override object SolvePart2(int[][] input)
        => input.Select(GetPrev).Sum();

    private int GetNext(int[] line)
    {
        var diffs = GetDiffs(line);
        if (diffs.All(x => x == 0))
            return line[0];

        var next = GetNext(diffs);
        return line[^1] + next;
    }
    
    private int GetPrev(int[] line)
    {
        var diffs = GetDiffs(line);
        if (diffs.All(x => x == 0))
            return line[0];

        var prev = GetPrev(diffs);
        return line[0] - prev;
    }

    private int[] GetDiffs(int[] line)
    {
        var result = new int[line.Length - 1];
        for (int i = 0; i < result.Length; i++)
            result[i] = line[i + 1] - line[i];

        return result;
    }

    protected override int[][] Parse()
    {
        return ReadAllInputLines().Select(x => x.Split(' ').Select(Int32.Parse).ToArray()).ToArray();
    }
}