using System;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day05 : PuzzleSolutionBase
    {
        public Day05() : base(5)
        {
        }

        public override string SolvePart1()
        {
            return ReadAllInputLines().Select(ParseSeatId).Max().ToString();
        }

        public override string SolvePart2()
        {
            var seats = ReadAllInputLines().Select(ParseSeatId).OrderBy(x=>x).ToArray();

            for (int i = 0; i < seats.Length - 1;i++)
                if (seats[i + 1] - seats[i] > 1)
                    return (seats[i] + 1).ToString();

            return "Seat Not Found";
        }
        
        private int ParseSeatId(string seat) => Convert.ToInt32(ToBinary(seat), 2);

        private string ToBinary(string seat) =>
            new(seat.Select(chr => chr switch { 'B' or 'R' => '1', _ => '0' }).ToArray());
    }
}