using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode.Util
{
    public abstract class PuzzleSolutionBase : IPuzzleSolution
    {
        public int Day { get; }
        public int Year { get; }

        protected PuzzleSolutionBase()
        {
            (Day, Year) = GetDayAndYearFromName();
        }

        private (int Day, int Year) GetDayAndYearFromName()
        {
            var type = GetType();
            var dayMatch = Regex.Match(type.Name, @"^Day(?<day>\d{2})$");
            var yearMatch = Regex.Match(type.Namespace, @"^AdventOfCode.Y(?<year>\d{4})$");

            if (!dayMatch.Success || !yearMatch.Success)
                throw new Exception("Invalid naming convention");

            return (Int32.Parse(dayMatch.Groups["day"].Value.TrimStart('0')),
                Int32.Parse(yearMatch.Groups["year"].Value));
        }

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