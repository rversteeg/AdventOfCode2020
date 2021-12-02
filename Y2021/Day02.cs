using System;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2021
{
    public class Day02 : PuzzleSolutionWithParsedInput<Day02Input[]>
    {
        public Day02() : base(2, 2021)
        {
        }

        public override object SolvePart1(Day02Input[] input)
        {
            var (x, y) = input.Aggregate<Day02Input,(int x, int y)>((0, 0), (acc, day02Input) => day02Input switch
            {
                (Direction.Forward, var amount) => (acc.x + amount, acc.y),
                (Direction.Up, var amount) => (acc.x, acc.y - amount),
                (Direction.Down, var amount) => (acc.x, acc.y + amount)
            });

            return x * y;
        }

        public override object SolvePart2(Day02Input[] input)
        {
            var (x, y, aim) = input.Aggregate<Day02Input,(int x, int y, int aim)>((0, 0, 0), (acc, day02Input) => day02Input switch
            {
                (Direction.Forward, var amount) => (acc.x + amount, acc.y + acc.aim*amount, acc.aim),
                (Direction.Up, var amount) => (acc.x, acc.y, acc.aim-amount),
                (Direction.Down, var amount) => (acc.x, acc.y, acc.aim+amount)
            });

            return x * y;
        }

        protected override Day02Input[] Parse()
        {
            return ReadAllInputLines().Select(line =>
            {
                var parts = line.Split();
                return new Day02Input(parts[0] switch
                {
                    "forward" => Direction.Forward,
                    "up" => Direction.Up,
                    "down" => Direction.Down
                }, Int32.Parse(parts[1]));
            }).ToArray();
        }
    }
    
    public record Day02Input(Direction Direction, int Amount);

    public enum Direction
    {
        Forward,
        Up,
        Down
    }
}