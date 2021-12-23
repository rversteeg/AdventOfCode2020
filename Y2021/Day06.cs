using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2021
{
    public class Day06 : PuzzleSolutionWithParsedInput<int[]>
    {
        public override object SolvePart1(int[] input)
        {
            Cache = Enumerable.Range(0, 9).ToDictionary(x => (x, 1), x => x == 0 ? 2L : 1L);
            return Solve2(input, 80);
        }

        private long Solve2(int[] input, int days)
            => input.Sum(x => NrReproduction((x, days)));

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

        private Dictionary<(int stage, int days), long> Cache =
            Enumerable.Range(0, 9).ToDictionary(x => (x, 1), x => x == 0 ? 2L : 1L);

        private long NrReproduction((int stage, int days) input)
        {
            if (Cache.ContainsKey(input))
                return Cache[input];

            long value =
                input.stage == 0
                    ? NrReproduction((6, input.days - 1)) + NrReproduction((8, input.days - 1))
                    : NrReproduction((input.stage - 1, input.days - 1));
            Cache[input] = value;
            return value;
        }

        public override object SolvePart2(int[] input)
        {
            Cache = Enumerable.Range(0, 9).ToDictionary(x => (x, 1), x => x == 0 ? 2L : 1L);
            return Solve2(input, 256);
        }

        protected override int[] Parse()
            => ReadAllInputText().Split(",").Select(int.Parse).ToArray();
    }
}