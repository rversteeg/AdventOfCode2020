using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2021
{
    public class Day06 : PuzzleSolutionWithParsedInput<int[]>
    {
        public Day06() : base(6, 2021){}
        public override object SolvePart1(int[] input)
        {
            return Solve(input, 80);
        }

        private static long Solve(int[] input, int days)
        {
            var fish = input.GroupBy(x => x).ToDictionary(x => x.Key, x => x.LongCount());

            for (var d = 0; d < days; d++)
            {
                for (var i = 0; i <= 8; i++)
                {
                    fish[i - 1] = fish.TryGetValue(i, out var oldFishCount) ? oldFishCount : 0;
                }

                fish[6] += fish[-1];
                fish[8] = fish[-1];
            }
            
            fish.Remove(-1);
            return fish.Values.Sum();
        }

        public override object SolvePart2(int[] input)
        {
            return Solve(input, 256);
        }

        protected override int[] Parse()
            => ReadAllInputText().Split(",").Select(int.Parse).ToArray();
    }
}