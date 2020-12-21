namespace AdventOfCode.Util
{
    public abstract class PuzzleSolutionWithTextInput : PuzzleSolutionWithParsedInput<string>
    {
        protected PuzzleSolutionWithTextInput(int day, int year) : base(day, year) { }

        protected override string Parse() => ReadAllInputText();
    }
}