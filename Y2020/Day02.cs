using System;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2020
{
    public class Day02 : PuzzleSolutionWithParsedInput<Day02.Day2Record[]>
    {
        public Day02() : base(2, 2020) {}

        public override object SolvePart1(Day2Record[] input) => input.Count(MatchesCount);

        private bool MatchesCount(Day2Record input)
        {
            var count = input.Password.Count(chr => chr == input.Char);
            return count >= input.Min && count <= input.Max;
        }

        public override object SolvePart2(Day2Record[] input)
                => input.Count(MatchesPosition);

        private bool MatchesPosition(Day2Record input) 
            => input.Password[input.Min - 1] == input.Char ^ input.Password[input.Max - 1] == input.Char;

        protected override Day2Record[] Parse()
            => (from line in ReadAllInputLines()
                let parts = line.Split(new[] {'-', ' ', ':'}, StringSplitOptions.RemoveEmptyEntries)
                select new Day2Record(int.Parse(parts[0]), int.Parse(parts[1]), parts[2][0], parts[3])).ToArray();

        public record Day2Record(int Min, int Max, int Char, string Password);
    }
}