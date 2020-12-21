using System.IO;

namespace AdventOfCode.Util
{
    public abstract class PuzzleSolutionBase : IPuzzleSolution
    {
        public int Day { get; }
        public int Year { get; }

        protected PuzzleSolutionBase(int day, int year)
        {
            Day = day;
            Year = year;
        }

        public virtual object SolvePart1()
        {
            return null;
        }

        public virtual object SolvePart2()
        {
            return null;
        }
        private string InputFileName => $@"Input\{Year}\Day{Day:D2}.txt";

        protected string ReadAllInputText()
        {
            return File.ReadAllText(InputFileName);
        }

        protected string[] ReadAllInputLines()
        {
            return File.ReadAllLines(InputFileName);
        }
    }
}