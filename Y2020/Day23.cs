using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2020
{
    public class Day23 : PuzzleSolutionWithParsedInput<int[]>
    {
        public Day23() : base(23, 2020) { }

        private struct Cup
        {
            public int Value;
            public int NextValue;
        }

        public override object SolvePart1(int[] input)
        {
            var lookup = BuildCupList(input);
            Run(lookup, input[0], input.Max(), 100);
            
            return string.Join("", Enumerate(lookup, lookup[1].NextValue).Take(8).Select(x => x.ToString()));
        }

        private static IEnumerable<int> Enumerate(Cup[] lookup, int firstValue)
        {
            Cup cur = lookup[firstValue];
            while (true)
            {
                yield return cur.Value;
                cur = lookup[cur.NextValue];
            }
        }

        private void Run(Cup[] lookup, int firstValue, int maxValue, int iterations)
        {
            var curValue = firstValue;
            
            for (int i = 0; i < iterations; i++)
            {
                var valuesToRemove = (lookup[curValue].NextValue,
                    lookup[lookup[curValue].NextValue].NextValue, 
                    lookup[lookup[lookup[curValue].NextValue].NextValue].NextValue);
                
                var destVal = FindDestVal(curValue, maxValue, valuesToRemove);

                //Remove
                lookup[curValue].NextValue = lookup[valuesToRemove.Item3].NextValue;
                //Insert
                lookup[valuesToRemove.Item3].NextValue = lookup[destVal].NextValue;
                lookup[destVal].NextValue = valuesToRemove.Item1;

                curValue = lookup[curValue].NextValue;
            }
        }

        private static Cup[] BuildCupList(IReadOnlyList<int> input, bool extendToMillion = false)
        {
            //Index 0 is not used
            var lookup = new Cup[extendToMillion ? 1_000_001 : input.Count + 1];
            var prevNumber = -1;

            foreach (var number in input)
            {
                lookup[number] = new Cup() {Value = number};
                if (prevNumber != -1)
                    lookup[prevNumber].NextValue = number;
                prevNumber = number;
            }

            if (extendToMillion)
            {
                for (int i = input.Max()+1; i <= 1_000_000; i++)
                {
                    lookup[i] = new Cup() {Value = i};
                    lookup[prevNumber].NextValue = i;
                    prevNumber = i;
                }
            }

            lookup[prevNumber].NextValue = lookup[input[0]].Value;
            return lookup;
        }

        private static int FindDestVal(int curValue, int maxVal, (int, int, int) exclude)
        {
            while(true)
            {
                curValue = curValue == 1 ? maxVal : curValue - 1;
                if (exclude.Item1 != curValue && exclude.Item2 != curValue && exclude.Item3 != curValue)
                    return curValue;
            }
        }

        public override object SolvePart2(int[] input)
        {
            const int numItems = 1_000_000;
            var lookup = BuildCupList(input, true);
            Run(lookup, input[0], numItems, 10_000_000);
            return 1L * lookup[1].NextValue * lookup[lookup[1].NextValue].NextValue;
        }

        protected override int[] Parse()
        {
            return new[] { 8,7,1,3,6,9,4,5,2};
        }
    }
}