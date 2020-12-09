using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2020.Util;

namespace AdventOfCode2020
{
    public class Day09 : PuzzleSolutionWithParsedInput<long[]>
    {

        public Day09() : base(9) {}

        private const int preambleLength = 25;
        public override object SolvePart1(long[] input)
        {
            return input[Enumerable.Range(preambleLength, input.Length - preambleLength).First(x => !IsValid(x, input))];
        }

        private bool IsValid(int index, long[] input)
        {
            var preamble = input.Skip(index - preambleLength).Take(preambleLength).ToImmutableList();
            var set = new SortedSet<long>(preamble);

            return preamble.Any(x=>set.Contains(input[index]-x));
        }

        public override object SolvePart2(long[] input)
        {
            long sum = (long)SolvePart1(input);
            int start = 0, end = 0;
            long rangeSum = input[0];


            while (rangeSum != sum)
            {
                if (rangeSum < sum)
                {
                    rangeSum += input[++end];
                }
                else
                {
                    rangeSum -= input[start++];
                }
            }

            return input[start..end].Min() + input[start..end].Max();
        }

        protected override long[] Parse()
            => ReadAllInputLines().Select(long.Parse).ToArray();

    }
}
