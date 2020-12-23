using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2020
{
    public class Day23 : PuzzleSolutionWithParsedInput<int[]>
    {
        public Day23() : base(23, 2020) { }

        public override object SolvePart1(int[] input)
        {
            var lookup = BuildCupList(input);
            Run(lookup, input[0], input.Max(), 100);
            
            return string.Join("", Enumerate(lookup, lookup[1]).Take(8).Select(x => x.ToString()));
        }

        private static IEnumerable<int> Enumerate(int[] next, int firstValue)
        {
            while (true)
            {
                yield return firstValue;
                firstValue = next[firstValue];
            }
        }

        private static int Mod(int val, int mod)
        {
            var result = val % mod;
            return result < 0 ? result + mod : result;
        }

        private void Run(int[] next, int firstValue, int maxValue, int iterations)
        {
            var curValue = firstValue;
            
            for (int i = 0; i < iterations; i++)
            {
                var i1 = next[curValue];
                var i2 = next[i1];
                var i3 = next[i2];
                
                var destVal = curValue == 1 ? maxValue : curValue - 1;
                while(destVal == i1 || destVal == i2 || destVal == i3)
                    destVal = destVal == 1 ? maxValue : destVal - 1;

                //Remove
                next[curValue] = next[i3];
                //Insert
                next[i3] = next[destVal];
                next[destVal] = i1;

                curValue = next[curValue];
            }
        }

        private static int[] BuildCupList(IReadOnlyList<int> input, bool extendToMillion = false)
        {
            //Index 0 is not used
            var lookup = new int[extendToMillion ? 1_000_001 : input.Count + 1];
            var prevNumber = -1;

            foreach (var number in input)
            {
                if (prevNumber != -1)
                    lookup[prevNumber] = number;
                prevNumber = number;
            }

            if (extendToMillion)
            {
                for (int i = input.Max()+1; i <= 1_000_000; i++)
                {
                    lookup[prevNumber] = i;
                    prevNumber = i;
                }
            }

            lookup[prevNumber] = input[0];
            return lookup;
        }

        public override object SolvePart2(int[] input)
        {
            const int numItems = 1_000_000;
            var lookup = BuildCupList(input, true);
            Run(lookup, input[0], numItems, 10_000_000);
            return 1L * lookup[1] * lookup[lookup[1]];
        }

        protected override int[] Parse()
        {
            return new[] { 8,7,1,3,6,9,4,5,2};
        }
    }
}