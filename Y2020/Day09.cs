using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2020
{
    public class Day09 : PuzzleSolutionWithParsedInput<long[]>
    {

        public Day09() : base(9, 2020) {}

        private const int PreambleLength = 25;
        public override object SolvePart1(long[] input)
        {
            return input[Enumerable.Range(PreambleLength, input.Length - PreambleLength).First(x => !IsValid(x, input))];
        }

        private bool IsValid(int index, long[] input)
        {
            var preamble = new HashSet<long>(input[(index - PreambleLength)..index]);
            return preamble.Any(x=> preamble.Contains(input[index]-x));
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
