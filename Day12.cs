using System;
using System.Linq;
using System.Numerics;
using AdventOfCode2020.Util;

namespace AdventOfCode2020
{
    public class Day12 : PuzzleSolutionWithParsedInput<Day12.Command[]>
    {
        public record Command(char command, int value);

        public record Boat(Vector2 position, Vector2 direction);

        private static readonly Vector2 North = new Vector2(0, 1);
        private static readonly Vector2 South = new Vector2(0, -1);
        private static readonly Vector2 West = new Vector2(-1, 0);
        private static readonly Vector2 East = new Vector2(1, 0);

        public Day12() : base(12) {}

        public override object SolvePart1(Command[] input)
        {
            var (position, _) = input.Aggregate(new Boat(Vector2.Zero, East), Execute);
            return Math.Abs(position.X) + Math.Abs(position.Y);
        }

        private static Boat Execute(Boat boat, Command command) =>
            command.command switch
            {
                'N' => new Boat(boat.position + North * command.value, boat.direction),
                'S' => new Boat(boat.position + South * command.value, boat.direction),
                'E' => new Boat(boat.position + East * command.value, boat.direction),
                'W' => new Boat(boat.position + West * command.value, boat.direction),
                'L' => new Boat(boat.position, Rotate(boat.direction, -command.value)),
                'R' => new Boat(boat.position, Rotate(boat.direction, command.value)),
                'F' => new Boat(boat.position + boat.direction * command.value, boat.direction),
                _ => throw new ArgumentOutOfRangeException()
            };

        public override object SolvePart2(Command[] input)
        {
            var (position, _) = input.Aggregate(new Boat(Vector2.Zero, new Vector2(10, 1)), ExecuteWithDirectionAsWaypoint);
            return Math.Abs(position.X) + Math.Abs(position.Y);
        }

        private static Boat ExecuteWithDirectionAsWaypoint(Boat boat, Command command) =>
            command.command switch
            {
                'N' => new Boat(boat.position, boat.direction + North * command.value),
                'S' => new Boat(boat.position, boat.direction + South * command.value),
                'E' => new Boat(boat.position, boat.direction + East * command.value),
                'W' => new Boat(boat.position, boat.direction + West * command.value),
                'L' => new Boat(boat.position, Rotate(boat.direction, -command.value)),
                'R' => new Boat(boat.position, Rotate(boat.direction, command.value)),
                'F' => new Boat(boat.position + boat.direction * command.value, boat.direction),
                _ => throw new ArgumentOutOfRangeException()
            };

        private static Vector2 Rotate(Vector2 vector, int degrees)
            => (degrees < 0 ? degrees + 360 : degrees) switch
            {
                90 => new Vector2(vector.Y, -vector.X),
                180 => new Vector2(-vector.X, -vector.Y),
                270 => new Vector2(-vector.Y, vector.X),
                _ => throw new ArgumentOutOfRangeException(nameof(degrees), degrees, "degrees must be a multiple of 90 and between -360 and 360")
            };

        protected override Command[] Parse()
            => ReadAllInputLines().Select(line => new Command(line[0], int.Parse(line.Substring(1)))).ToArray();
    }
}