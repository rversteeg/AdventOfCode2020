namespace AdventOfCode2020.Util
{
    public abstract class PuzzleSolutionWithTextInput : PuzzleSolutionWithParsedInput<string>
    {
        protected PuzzleSolutionWithTextInput(int day) : base(day) { }

        protected override string Parse() => ReadAllInputText();
    }
}