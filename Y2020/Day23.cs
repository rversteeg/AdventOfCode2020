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
            public int Value { get; init; }
            public Cup Next { get; set; }

            public IEnumerable<Cup> Take(int nr)
            {
                var curCup = this;
                for (int i = 0; i < nr; i++)
                {
                    yield return curCup;
                    curCup = curCup.Next;
                }
            }
        }

        public override object SolvePart1(int[] input)
        {
            var lookup = BuildCupList(input);
            var curCup = lookup[input[0]];
            
            for (int i = 0; i < 100; i++)
            {
                var cupsToRemove = curCup.Next.Take(3).ToList();
                
                //Remove
                curCup.Next = cupsToRemove[2].Next;
                
                var destVal = FindDestVal(curCup.Value, 9, cupsToRemove.Select(x => x.Value).ToList());
                var destCup = lookup[destVal];

                //Insert
                cupsToRemove[2].Next = destCup.Next;
                destCup.Next = cupsToRemove[0];

                curCup = curCup.Next;
            }

            return string.Join("", lookup[1].Next.Take(8).Select(x => x.Value.ToString()));
        }

        private static Dictionary<int, Cup> BuildCupList(IReadOnlyList<int> input)
        {
            if (input.Count == 0)
                return new Dictionary<int, Cup>();
            
            Dictionary<int, Cup> lookup = new();
            Cup prev = null;

            foreach (var number in input)
            {
                var cup = new Cup() {Value = number};
                if (prev != null)
                    prev.Next = cup;
                prev = cup;
                lookup[number] = cup;
            }

            prev!.Next = lookup[input[0]];
            return lookup;
        }

        private static int FindDestVal(int curValue, int maxVal, IList<int> exclude)
        {
            while(true)
            {
                curValue = curValue == 1 ? maxVal : curValue - 1;
                if (!exclude.Contains(curValue))
                    return curValue;
            }
        }

        public override object SolvePart2(int[] input)
        {
            const int numItems = 1_000_000;
            var lookup = BuildCupList(input.Concat(Enumerable.Range(input.Max() + 1, numItems - input.Length)).ToList());
            var curCup = lookup[input[0]];
            
            for (int i = 0; i < 10_000_000; i++)
            {
                var cupsToRemove = curCup.Next.Take(3).ToList();
                
                //Remove
                curCup.Next = cupsToRemove[2].Next;
                
                var destVal = FindDestVal(curCup.Value, numItems, cupsToRemove.Select(x => x.Value).ToList());
                var destCup = lookup[destVal];

                //Insert
                cupsToRemove[2].Next = destCup.Next;
                destCup.Next = cupsToRemove[0];

                curCup = curCup.Next;
            }
            
            return 1L * lookup[1].Next.Value * lookup[1].Next.Next.Value;
        }

        protected override int[] Parse()
        {
            return new[] { 8,7,1,3,6,9,4,5,2};
            //return new int[] {3,8,9,1,2,5,4,6,7};
        }
    }
}