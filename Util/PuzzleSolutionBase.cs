﻿using System.IO;

namespace AdventOfCode2020.Util
{
    public abstract class PuzzleSolutionBase : IPuzzleSolution
    {
        public int Day { get; }

        protected PuzzleSolutionBase(int day)
        {
            Day = day;
        }

        public virtual object SolvePart1()
        {
            return null;
        }

        public virtual object SolvePart2()
        {
            return null;
        }

        public string InputFileName => $@"Input\Day{Day:D2}.txt";

        public string ReadAllInputText()
        {
            return File.ReadAllText(InputFileName);
        }

        public string[] ReadAllInputLines()
        {
            return File.ReadAllLines(InputFileName);
        }
    }
}