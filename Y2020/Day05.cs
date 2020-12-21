using System;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2020
{
    public class Day05 : PuzzleSolutionWithLinesInput
    {
        public Day05() : base(5, 2020){}

        public override object SolvePart1(string[] input)
        {
            return input.Select(ParseSeatId).Max();
        }

        public override object SolvePart2(string[] input)
        {
            var seats = input.Select(ParseSeatId).OrderBy(x=>x).ToArray();

            for (int i = 0; i < seats.Length - 1;i++)
                if (seats[i + 1] - seats[i] > 1)
                    return (seats[i] + 1);

            return "Seat Not Found";
        }
        
        private static int ParseSeatId(string seat) => Convert.ToInt32(ToBinary(seat), 2);

        private static string ToBinary(string seat) =>
            new(seat.Select(chr => chr switch { 'B' or 'R' => '1', _ => '0' }).ToArray());
    }
}