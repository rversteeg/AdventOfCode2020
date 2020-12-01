namespace AdventOfCode2020
{
    public abstract class PuzzleSolutionBase : IPuzzleSolution
    {
        public int Day { get; }

        protected PuzzleSolutionBase(int day)
        {
            Day = day;
        }

        public virtual string SolvePart1()
        {
            return null;
        }

        public virtual string SolvePart2()
        {
            return null;
        }
    }
}