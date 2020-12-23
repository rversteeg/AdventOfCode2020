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

        [DebuggerDisplay("{Value} ( Next = {Next.Value} )")]
        public class Cup
        {
            public int Value;
            public Cup Next;

            public IEnumerable<Cup> Enumerate()
            {
                var cur = this;
                while (true)
                {
                    yield return cur;
                    cur = cur.Next;
                }
            }
        }

        public override object SolvePart1(int[] input)
        {
            var lookup = BuildCupList(input);
            Run(lookup, input[0], input.Max(), 100);
            
            return string.Join("", lookup[1].Next.Enumerate().Take(8).Select(x => x.Value.ToString()));
        }

        private void Run(Cup[] lookup, int firstValue, int maxValue, int iterations)
        {
            var curCup = lookup[firstValue];
            
            for (int i = 0; i < iterations; i++)
            {
                var firstCup = curCup.Next;
                var middleCup = firstCup.Next;
                var lastCup = middleCup.Next;
                var destVal = FindDestVal(curCup.Value, maxValue, firstCup.Value, middleCup.Value, lastCup.Value );
                var destCup = lookup[destVal];
                
                //Remove
                curCup.Next = lastCup.Next;
                //Insert
                lastCup.Next = destCup.Next;
                destCup.Next = firstCup;

                curCup = curCup.Next;
            }
        }

        private static Cup[] BuildCupList(IReadOnlyList<int> input, bool extendToMillion = false)
        {
            //Index 0 is not used
            var lookup = new Cup[extendToMillion ? 1_000_001 : input.Count + 1];
            Cup prev = null;

            foreach (var number in input)
            {
                var cup = new Cup() {Value = number};
                if (prev != null)
                    prev.Next = cup;
                prev = cup;
                lookup[number] = cup;
            }

            if (extendToMillion)
            {
                for (int i = 10; i <= 1_000_000; i++)
                {
                    var cup = new Cup() {Value = i};
                    prev.Next = cup;
                    prev = cup;
                    lookup[i] = cup;
                }
            }

            prev!.Next = lookup[input[0]];
            return lookup;
        }

        private static int FindDestVal(int curValue, int maxVal, int exclude1, int exclude2, int exclude3)
        {
            while(true)
            {
                curValue = curValue == 1 ? maxVal : curValue - 1;
                if (exclude1 != curValue && exclude2 != curValue && exclude3 != curValue)
                    return curValue;
            }
        }

        public override object SolvePart2(int[] input)
        {
            const int numItems = 1_000_000;
            var lookup = BuildCupList(input, true);
            Run(lookup, input[0], numItems, 10_000_000);
            return 1L * lookup[1].Next.Value * lookup[1].Next.Next.Value;
        }

        protected override int[] Parse()
        {
            return new[] { 8,7,1,3,6,9,4,5,2};
        }
    }
}