using System;
using AdventOfCode.Util;

namespace AdventOfCode.Y2020
{
    public class Day25 : PuzzleSolutionWithParsedInput<(int, int)>
    {
        public Day25() : base(25, 2020)
        {
        }

        public override object SolvePart1((int, int) input)
        {
            long value = 1;
            long subject = 7;
            long loop = 0;
            while (true)
            {
                value = (value*subject)%20201227;
                loop++;
                if (value == input.Item1 || value == input.Item2)
                    break;
            }

            subject = value == input.Item1 ? input.Item2 : input.Item1;
            value = 1;

            for (long i = 0; i < loop; i++)
            {
                value = (value*subject)%20201227;
            }

            return value;
        }

        public override object SolvePart2((int, int) input)
        {
            return "Merry Christmas!";
        }

        protected override (int, int) Parse()
        {
            return (9033205, 9281649);
        }
    }
}