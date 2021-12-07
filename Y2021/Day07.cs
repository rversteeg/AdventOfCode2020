using System;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2021
{
    public class Day07 : PuzzleSolutionWithParsedInput<int[]>
    {
        public Day07() : base(7, 2021){}

        public override object SolvePart1(int[] input)
        {
            return Solve(input, x => x);
        }

        private static long Solve(int[] input, Func<int, long> fuelFunction)
        {
            var min = input.Min();
            var max = input.Max();

            var result = long.MaxValue;

            foreach (var i in min..max)
            {
                var fuel = input.Sum(x => fuelFunction(Math.Abs(x - i)));
                if (fuel < result)
                    result = fuel;
            }

            return result;
        }

        public override object SolvePart2(int[] input)
            => Solve(input, x=>x * (x + 1) / 2);

        protected override int[] Parse()
            => ReadAllInputText().Split(',').Select(int.Parse).ToArray();
    }
}