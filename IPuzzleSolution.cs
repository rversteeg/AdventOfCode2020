namespace AdventOfCode
{
    public interface IPuzzleSolution
    {
        public int Day { get; }
        public int Year { get; }

        object SolvePart1();
        object SolvePart2();
    }
}