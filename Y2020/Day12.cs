using System;
using System.Linq;
using System.Numerics;
using AdventOfCode.Util;

namespace AdventOfCode.Y2020
{
    public class Day12 : PuzzleSolutionWithParsedInput<Day12.Command[]>
    {
        public record Command(char Cmd, int Value);

        public record Boat(Vector2 Position, Vector2 Direction);

        private static readonly Vector2 North = new(0, 1);
        private static readonly Vector2 South = new(0, -1);
        private static readonly Vector2 West = new(-1, 0);
        private static readonly Vector2 East = new(1, 0);

        public Day12() : base(12, 2020) {}

        public override object SolvePart1(Command[] input)
        {
            var (position, _) = input.Aggregate(new Boat(Vector2.Zero, East), Execute);
            return Math.Abs(position.X) + Math.Abs(position.Y);
        }

        private static Boat Execute(Boat boat, Command command) =>
            command.Cmd switch
            {
                'N' => new Boat(boat.Position + North * command.Value, boat.Direction),
                'S' => new Boat(boat.Position + South * command.Value, boat.Direction),
                'E' => new Boat(boat.Position + East * command.Value, boat.Direction),
                'W' => new Boat(boat.Position + West * command.Value, boat.Direction),
                'L' => new Boat(boat.Position, Rotate(boat.Direction, -command.Value)),
                'R' => new Boat(boat.Position, Rotate(boat.Direction, command.Value)),
                'F' => new Boat(boat.Position + boat.Direction * command.Value, boat.Direction),
                _ => throw new ArgumentOutOfRangeException()
            };

        public override object SolvePart2(Command[] input)
        {
            var (position, _) = input.Aggregate(new Boat(Vector2.Zero, new Vector2(10, 1)), ExecuteWithDirectionAsWaypoint);
            return Math.Abs(position.X) + Math.Abs(position.Y);
        }

        private static Boat ExecuteWithDirectionAsWaypoint(Boat boat, Command command) =>
            command.Cmd switch
            {
                'N' => new Boat(boat.Position, boat.Direction + North * command.Value),
                'S' => new Boat(boat.Position, boat.Direction + South * command.Value),
                'E' => new Boat(boat.Position, boat.Direction + East * command.Value),
                'W' => new Boat(boat.Position, boat.Direction + West * command.Value),
                'L' => new Boat(boat.Position, Rotate(boat.Direction, -command.Value)),
                'R' => new Boat(boat.Position, Rotate(boat.Direction, command.Value)),
                'F' => new Boat(boat.Position + boat.Direction * command.Value, boat.Direction),
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