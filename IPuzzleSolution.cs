namespace AdventOfCode2020
{
    public interface IPuzzleSolution
    {
        public int Day { get; }

        string SolvePart1();
        string SolvePart2();
    }
}