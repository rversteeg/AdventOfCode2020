using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2015
{
    public class Day17 : PuzzleSolutionWithParsedInput<int[]>
    {
        public Day17() : base(17, 2015) { }

        public override object SolvePart1(int[] input)
        {
            var solution = Solutions(input, 150).ToList();

            return solution.Count();
        }

        public IEnumerable<IEnumerable<int>> Solutions(ArraySegment<int> bins, int remaining)
        {
            if (bins.Count == 0 || remaining <= 0)
                yield break;

            var bin0 = new[] {bins[0]};

            if (bins[0] == remaining)
                yield return bin0;

            foreach (var item in Solutions(bins.Slice(1), remaining - bins[0]))
            {
                yield return bin0.Concat(item);
            }
            
            foreach (var item in Solutions(bins.Slice(1), remaining))
            {
                yield return item;
            }
        }

        public override object SolvePart2(int[] input)
        {
            var solution = Solutions(input, 150).ToList();

            var minContainers = solution.Select(x => x.Count()).OrderBy(x => x).ToList();

            var numOptions = minContainers.Count(x => x == minContainers.First());


            return numOptions;
        }

        protected override int[] Parse()
            => ReadAllInputLines().Select(Int32.Parse).OrderByDescending(x=>x).ToArray();
    }
}