using System;

namespace AdventOfCode.Util
{
    public abstract class PuzzleSolutionWithLinesInput : PuzzleSolutionWithParsedInput<string[]>
    {
        protected PuzzleSolutionWithLinesInput(int day, int year) : base(day, year) {}

        protected override string[] Parse() => ReadAllInputLines();
    }
}