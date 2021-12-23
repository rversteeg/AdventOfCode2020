using System;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2021
{
    public class Day07 : PuzzleSolutionWithParsedInput<int[]>
    {

        public override object SolvePart1(int[] input)
            => Solve(input, x => x);

        private static long Solve(int[] input, Func<int, long> fuelFunction)
            => (input.Min()..input.Max())
                .Select(i => input.Sum(x => fuelFunction(Math.Abs(x - i))))
                .Min();

        public override object SolvePart2(int[] input)
            => Solve(input, x=>x * (x + 1) / 2);

        protected override int[] Parse()
            => ReadAllInputText().Split(',').Select(int.Parse).ToArray();
    }
}