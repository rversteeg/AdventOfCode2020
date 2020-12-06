namespace AdventOfCode2020.Util
{
    public abstract class PuzzleSolutionWithLinesInput : PuzzleSolutionWithParsedInput<string[]>
    {
        protected PuzzleSolutionWithLinesInput(int day) : base(day) {}

        protected override string[] Parse() => ReadAllInputLines();
    }
}