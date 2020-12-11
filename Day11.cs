using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Util;

namespace AdventOfCode2020
{
    public class Day11 : PuzzleSolutionWithParsedInput<char[,]>
    {
        private const char FreeSeat = 'L';
        private const char TakenSeat = '#';

        public Day11() : base(11){}

        public override object SolvePart1(char[,] input)
        {
            var positions = Positions(input).ToArray();

            while (true)
            {
                var swap = positions.Where(pos =>
                    input[pos.x, pos.y] == FreeSeat && AdjacentTaken(input, pos) == 0 ||
                    input[pos.x, pos.y] == TakenSeat && AdjacentTaken(input, pos) >= 4).ToList();

                if (!swap.Any())
                    break;


                swap.ForEach(pos => input[pos.x, pos.y] = input[pos.x, pos.y] == FreeSeat ? TakenSeat : FreeSeat);
            }

            return positions.Count(x => input[x.x, x.y] == TakenSeat);
        }

        public override object SolvePart2(char[,] input)
        {
            var positions = Positions(input).ToArray();

            while (true)
            {
                var swap = positions.Where(pos =>
                    input[pos.x, pos.y] == FreeSeat && VisibleTaken(input, pos) == 0 ||
                    input[pos.x, pos.y] == TakenSeat && VisibleTaken(input, pos) >= 5).ToList();

                if (!swap.Any())
                    break;


                swap.ForEach(pos => input[pos.x, pos.y] = input[pos.x, pos.y] == FreeSeat ? TakenSeat : FreeSeat);
            }

            return positions.Count(x => input[x.x, x.y] == TakenSeat);
        }

        private IEnumerable<(int x, int y)> Positions(char[,] plan) =>
            from x in Enumerable.Range(0, plan.GetLength(0))
            from y in Enumerable.Range(0, plan.GetLength(1))
            select (x, y);

        private static int AdjacentTaken(char[,] plan, (int x, int y) pos)
            => AdjacentLocations(pos).Count(x => IsTaken(plan, x));

        private static IEnumerable<(int x, int y)> AdjacentLocations((int x, int y) pos)
            => Directions.Select(dir => (pos.x + dir.x, pos.y + dir.y));

        private static bool IsTaken(char[,] plan, (int x, int y) pos) => pos.x >= 0 && pos.y >= 0 && pos.x < plan.GetLength(0) &&
                                                                         pos.y < plan.GetLength(1) && plan[pos.x, pos.y] == TakenSeat;

        private static readonly IEnumerable<(int x, int y)> Directions
            = new[]
            {
                (-1, -1), (0, - 1), (+1, -1),
                (-1, 0), /* center */(+1, 0),
                (-1, +1), (0, +1), (+ 1, + 1)
            };

        private static int VisibleTaken(char[,] plan, (int x, int y) pos)
            => Directions.Count(dir => ScanTaken(plan, (pos.x + dir.x, pos.y + dir.y), dir));

        private static bool ScanTaken(char[,] plan, (int x, int y) pos, (int x, int y) direction)
            => pos.x >= 0 && pos.y >= 0
               && pos.x < plan.GetLength(0) && pos.y < plan.GetLength(1)
               && plan[pos.x, pos.y] != FreeSeat 
               && (plan[pos.x, pos.y] == TakenSeat || ScanTaken(plan, (pos.x + direction.x, pos.y + direction.y), direction));

        protected override char[,] Parse()
        {
            var lines = ReadAllInputLines();
            var result = new char[lines[0].Length, lines.Length];

            for(int x = 0; x < lines[0].Length; x++)
            for (int y = 0; y < lines.Length; y++)
                result[x, y] = lines[y][x];

            return result;
        }
    }
}