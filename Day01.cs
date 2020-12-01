using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day01 : PuzzleSolutionBase
    {
        public Day01() : base(1)
        {
        }

        public override string SolvePart1()
        {
            var input = ReadInput();

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    if (input[i] + input[j] == 2020)
                        return (input[i] * input[j]).ToString();
                }
            }

            return "NotFound";
        }

        public override string SolvePart2()
        {
            var input = ReadInput();

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    for(int k = j + 1; k < input.Length; k++)
                    {
                        if (input[i] + input[j] + input[k] == 2020)
                            return (input[i] * input[j] * input[k]).ToString();
                    }
                }
            }

            return "NotFound";
        }

        public int[] ReadInput()
        {
            return File.ReadAllLines(@"Input\Day01\Part1.txt").Select(Int32.Parse).ToArray();
        }
    }
}