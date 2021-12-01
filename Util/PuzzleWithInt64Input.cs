using System.Linq;

namespace AdventOfCode.Util
{
    public abstract class PuzzleWithInt64Input : PuzzleSolutionWithParsedInput<long[]>
    {
        protected PuzzleWithInt64Input(int day, int year) : base(day, year) {}

        protected override long[] Parse() => ReadAllInputLines().Select(long.Parse).ToArray();
    }
}