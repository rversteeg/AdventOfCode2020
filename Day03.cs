using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Util;

namespace AdventOfCode2020
{
    public class Day03 : PuzzleSolutionWithLinesInput
    {
        public Day03() : base(3) {}

        public override object SolvePart1(string[] input)
            => NrOfTrees(input, (3,1));

        public override object SolvePart2(string[] input)
        {
            var slopes = new [] {(1,1), (3, 1), (5, 1), (7, 1), (1, 2)};

            return slopes.Select(slope => NrOfTrees(input, slope))
                .Aggregate(1L, (x, y) => x * y);
        }

        private int NrOfTrees(IEnumerable<string> input, (int dX, int dY) slope)
            => input.Where((_,index)=>index % slope.dY == 0).Where((line, index) => line[index * slope.dX % line.Length] == '#').Count();
    }
}