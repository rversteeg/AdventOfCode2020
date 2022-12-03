using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2022;

public class Day02 : PuzzleSolutionWithParsedInput<List<(int left, int right)>>
{
    private readonly int[,] _scoresP1 = { { 4, 8, 3}, { 1, 5, 9}, { 7, 2, 6} };
    public override object SolvePart1(List<(int left, int right)> input)
        =>input.Select(x => _scoresP1[x.left, x.right]).Sum();
    
    private readonly int[,] _scoresP2 = { { 3, 4, 8}, { 1, 5, 9}, { 2, 6, 7} };

    public override object SolvePart2(List<(int left, int right)> input)
        => input.Select(x => _scoresP2[x.left, x.right]).Sum();

    protected override List<(int left, int right)> Parse()
        => ReadAllInputLines().Select(x => (x[0] - 'A', x[2] - 'X')).ToList();
}