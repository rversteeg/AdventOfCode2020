using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AdventOfCode.Util;
// ReSharper disable PossibleMultipleEnumeration

namespace AdventOfCode.Y2020
{
    public class Day10 : PuzzleSolutionWithParsedInput<int[]>
    {

        public Day10() : base(10, 2020) {}

        public override object SolvePart1(int[] input)
        {
            int d1 = 0, d3 = 1, prev = 0;

            foreach (var value in input.OrderBy(x => x))
            {
                if (value - prev == 1)
                    ++d1;
                else
                    ++d3;

                prev = value;
            }

            return d1 * d3;
        }

        public override object SolvePart2(int[] input)
        {
            return Combinations(0, input.OrderBy(x => x).ToImmutableList());
        }


        private readonly Dictionary<int, long> _values = new();

        //Input is sorted
        private long Combinations(int curValue, IEnumerable<int> input)
        {
            if (!input.Any())
                return 1;
            if (_values.ContainsKey(curValue))
                return _values[curValue];
            _values[curValue] = input
                .TakeWhile(x => x > curValue && x <= curValue + 3).Select((x, idx) => Combinations(x, input.Skip(idx + 1))).Sum();
            return _values[curValue];
        }

        protected override int[] Parse()
            => ReadAllInputLines().Select(int.Parse).ToArray();
    }
}