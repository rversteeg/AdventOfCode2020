using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2020
{
    public class Day15 : PuzzleSolutionWithParsedInput<int[]>
    {
        public Day15() : base(15, 2020)
        {
        }
        public override object SolvePart1(int[] input)
        {
            return NthNumber(input, 2020);
        }

        private int NthNumber(int[] input, int iteration)
        {
            var lookup = input.Take(input.Length-1).Select((nr,idx)=>(nr,idx)).ToDictionary(x=>x.nr, x=>x.idx);
            var last = input[^1];

            for(int i = input.Length; i < iteration; i++)
            {                
                var lastTurn = i - 1;                
                var next = lookup.ContainsKey(last)
                            ? lastTurn - lookup[last]
                            : 0;

                lookup[last] = lastTurn;
                last = next;                
            }
            return last;
        }

        public override object SolvePart2(int[] input)
        {
            return NthNumber(input, 30000000);
        }

        protected override int[] Parse() => ReadAllInputText().Split(',').Select(int.Parse).ToArray();
    }
}