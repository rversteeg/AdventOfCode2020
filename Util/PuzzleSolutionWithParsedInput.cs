namespace AdventOfCode.Util
{
    public abstract class PuzzleSolutionWithParsedInput<TInput> : PuzzleSolutionBase
    {
        protected PuzzleSolutionWithParsedInput() {}
        protected PuzzleSolutionWithParsedInput(int day, int year) : base(day, year) {}

        public abstract object SolvePart1(TInput input);
        public abstract object SolvePart2(TInput input);

        protected abstract TInput Parse();

        public override object SolvePart1()
        {
            return SolvePart1(Parse());
        }

        public override object SolvePart2()
        {
            return SolvePart2(Parse());
        }
    }
}