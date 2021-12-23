using System;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2021
{
    public class Day03 : PuzzleSolutionWithLinesInput
    {
        public override object SolvePart1(string[] input)
        {
            var half = input.Length / 2;
            var gammaStr = new String(Enumerable.Range(0, input[0].Length)
                .Select(x => input.Count(line => line[x] == '1') > half ? '1' : '0').ToArray());

            var epsilonStr = new String(gammaStr.Select(x => x == '1' ? '0' : '1').ToArray());

            var gammaRate = Convert.ToInt32(gammaStr, 2);
            var epsilonRate = Convert.ToInt32(epsilonStr, 2);

            return gammaRate * epsilonRate;
        }

        public override object SolvePart2(string[] input)
        {
            var oxygen = Convert.ToInt32(Find(input, true),2);
            var co2 = Convert.ToInt32(Find(input, false),2);
            return oxygen * co2;
        }

        public string Find(string[] input, bool mostCommon)
        {
            for (int pos = 0; pos < input[0].Length; pos++)
            {
                input = Reduce(input, pos, mostCommon);
                if (input.Length == 1)
                    return input[0];
                if (input.Length == 0)
                    return null;
            }

            return null;
        }

        public string[] Reduce(string[] input, int position, bool mostCommon)
        {
            var search = mostCommon ? '1' : '0';
            var count = input.Count(line => line[position] == search) * 2;
            var keep =
                count == input.Length ? search : 
                count > input.Length ? '1' : '0';

            return input.Where(line => line[position] == keep).ToArray();
        }
    }
}