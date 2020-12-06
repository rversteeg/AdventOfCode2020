namespace AdventOfCode2020
{
    public interface IPuzzleSolution
    {
        public int Day { get; }

        object SolvePart1();
        object SolvePart2();
    }
}