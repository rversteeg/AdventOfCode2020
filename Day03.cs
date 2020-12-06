using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day03 : PuzzleSolutionBase
    {
        public Day03() : base(3)
        {
        }

        public override string SolvePart1()
        {
            return NrOfTrees(ReadAllInputLines(), (3,1)).ToString();
        }

        public override string SolvePart2()
        {
            var input = ReadAllInputLines();

            var slopes = new [] {(1,1), (3, 1), (5, 1), (7, 1), (1, 2)};

            var result = slopes.Select(slope => NrOfTrees(input, slope))
                .Aggregate(1L, (x, y) => x * y);
            return result.ToString();
        }

        private int NrOfTrees(string[] input, (int dX, int dY) slope)
        {
            return input.Where((line,index)=>index % slope.dY == 0).Where((line, index) => line[index * slope.dX % line.Length] == '#').Count();
        }
    }
}