using System.Linq;

namespace AdventOfCode.Util;

public abstract class PuzzleWithCharGrid : PuzzleSolutionWithParsedInput<char[][]>
{
    protected PuzzleWithCharGrid(){}
    protected PuzzleWithCharGrid(int day, int year) : base(day, year) {}

    protected string Seperator { get; set; } = ",";

    protected override char[][] Parse()
        => ReadAllInputLines().Select(line=>line.ToArray()).ToArray();
}