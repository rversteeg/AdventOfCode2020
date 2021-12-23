using System;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2021
{
    public class Day02 : PuzzleSolutionWithParsedInput<Day02Input[]>
    {

        public override object SolvePart1(Day02Input[] input)
        {
            var (x, y) = input.Aggregate<Day02Input,(int x, int y)>((0, 0), (acc, day02Input) => day02Input switch
            {
                (Direction.Forward, var units) => (acc.x + units, acc.y),
                (Direction.Up, var units) => (acc.x, acc.y - units),
                (Direction.Down, var units) => (acc.x, acc.y + units)
            });

            return x * y;
        }

        public override object SolvePart2(Day02Input[] input)
        {
            var (x, y, aim) = input.Aggregate<Day02Input,(int x, int y, int aim)>((0, 0, 0), (acc, day02Input) => day02Input switch
            {
                (Direction.Forward, var units) => (acc.x + units, acc.y + acc.aim*units, acc.aim),
                (Direction.Up, var units) => (acc.x, acc.y, acc.aim-units),
                (Direction.Down, var units) => (acc.x, acc.y, acc.aim+units)
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
    
    public record Day02Input(Direction Direction, int Units);

    public enum Direction
    {
        Forward,
        Up,
        Down
    }
}