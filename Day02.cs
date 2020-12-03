using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day02 : PuzzleSolutionBase
    {
        public Day02() : base(2)
        {
        }

        public override string SolvePart1()
        {
            var input = ReadInput();

            return input.Count(MatchesCount).ToString();
        }

        private bool MatchesCount(Input input)
        {
            var count = input.Password.Count(chr => chr == input.Char);
            return count >= input.Min && count <= input.Max;
        }

        public override string SolvePart2()
        {
            var input = ReadInput();

            return input.Count(MatchesPosition).ToString();
        }

        private bool MatchesPosition(Input input)
        {
            return input.Password[input.Min - 1] == input.Char ^ input.Password[input.Max - 1] == input.Char;
        }

        public Input[] ReadInput()
        {
            return File.ReadAllLines(@"Input\Day02\Part1.txt").Select(x =>
            {
                var parts = x.Split(new char[] {'-', ' ', ':'}, StringSplitOptions.RemoveEmptyEntries);

                return new Input(Int32.Parse(parts[0]), Int32.Parse(parts[1]), parts[2][0], parts[3]);
            }).ToArray();
        }

        public record Input(int Min, int Max, int Char, string Password);
    }
}