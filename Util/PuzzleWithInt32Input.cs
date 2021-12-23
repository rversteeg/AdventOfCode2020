using System.Linq;

namespace AdventOfCode.Util
{
    public abstract class PuzzleWithInt32Input : PuzzleSolutionWithParsedInput<int[]>
    {
        protected PuzzleWithInt32Input(){}
        protected PuzzleWithInt32Input(int day, int year) : base(day, year) {}

        protected override int[] Parse() => ReadAllInputLines().Select(int.Parse).ToArray();
    }
}