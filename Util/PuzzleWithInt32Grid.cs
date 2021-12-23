using System.Linq;

namespace AdventOfCode.Util
{
    public abstract class PuzzleWithInt32Grid : PuzzleSolutionWithParsedInput<int[][]>
    {
        protected PuzzleWithInt32Grid(){}
        protected PuzzleWithInt32Grid(int day, int year) : base(day, year) {}

        protected string Seperator { get; set; } = ",";

        protected override int[][] Parse()
            => ReadAllInputLines().Select(line=>line.Select(c=> c - '0').ToArray()).ToArray();
    }
}