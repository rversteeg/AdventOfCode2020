using System.Linq;
using AdventOfCode2020.Util;

namespace AdventOfCode2020
{
    public class Day01 : PuzzleSolutionWithParsedInput<int[]>
    {
        public Day01() : base(1) {}

        public override object SolvePart1(int[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    if (input[i] + input[j] == 2020)
                        return (input[i] * input[j]).ToString();
                }
            }

            return null;
        }

        public override object SolvePart2(int[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    for(int k = j + 1; k < input.Length; k++)
                    {
                        if (input[i] + input[j] + input[k] == 2020)
                            return (input[i] * input[j] * input[k]);
                    }
                }
            }

            return null;
        }

        protected override int[] Parse() => ReadAllInputLines().Select(int.Parse).ToArray();
    }
}