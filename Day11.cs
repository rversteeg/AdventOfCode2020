using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode2020.Util;

namespace AdventOfCode2020
{
    public class Day11 : PuzzleSolutionWithParsedInput<char[,]>
    {
        private const char Floor = '.';
        private const char FreeSeat = 'L';
        private const char TakenSeat = '#';

        public Day11() : base(11){}

        public override object SolvePart1(char[,] input)
        {
            return Solve(input, AdjacentSeats, 4);
        }

        public override object SolvePart2(char[,] input)
        {
            return Solve(input, VisibleSeats, 5);
        }

        private readonly int _nrOfCores = Environment.ProcessorCount;

        private int Solve(char[,] input, Func<char[,], (int x, int y), IList<(int x, int y)>> seatSelector, int threshold)
        {
            var seatPositions = Positions(input).Where(pos => input[pos.x, pos.y] != Floor).ToList().AsParallel().WithDegreeOfParallelism(_nrOfCores);
            var seatsToCheck = seatPositions.ToDictionary(x => x, x => seatSelector(input, x));

            //Swap all for first run
            Parallel.ForEach(seatPositions, pos => input[pos.x, pos.y] = TakenSeat);
            
            while (true)
            {
                var swap = seatPositions.Where(pos =>
                    input[pos.x, pos.y] == FreeSeat && seatsToCheck[pos].Count(seat => IsTakenNoCheck(input, seat)) == 0 ||
                    input[pos.x, pos.y] == TakenSeat && seatsToCheck[pos].Count(seat => IsTakenNoCheck(input, seat)) >= threshold).ToList();

                if (!swap.Any())
                    break;

                Parallel.ForEach(swap,
                    pos => input[pos.x, pos.y] = input[pos.x, pos.y] == FreeSeat ? TakenSeat : FreeSeat);
            }

            return seatPositions.Count(x => input[x.x, x.y] == TakenSeat);
        }

        private IEnumerable<(int x, int y)> Positions(char[,] plan) =>
            from x in Enumerable.Range(0, plan.GetLength(0))
            from y in Enumerable.Range(0, plan.GetLength(1))
            select (x, y);

        private static readonly IEnumerable<(int x, int y)> Directions
            = new[]
            {
                (-1, -1), (0, - 1), (+1, -1),
                (-1, 0), /* center */(+1, 0),
                (-1, +1), (0, +1), (+ 1, + 1)
            };

        private static bool IsValidPosition(char[,] plan, (int x, int y) pos)
            => pos.x >= 0 && pos.y >= 0 && pos.x < plan.GetLength(0) && pos.y < plan.GetLength(1);
        
        private static IList<(int x, int y)> AdjacentSeats(char[,] plan, (int x, int y) pos)
            => AdjacentLocations(pos)
                .Where(adjPos => IsValidPosition(plan, adjPos) && plan[adjPos.x, adjPos.y] != Floor).ToList();

        private static IList<(int x, int y)> VisibleSeats(char[,] plan, (int x, int y) pos)
            => Directions.Select(dir => ScanSeat(plan, (pos.x + dir.x, pos.y + dir.y), dir)).Where(x => x != null).Select(x=>x.Value).ToList();

        private static IEnumerable<(int x, int y)> AdjacentLocations((int x, int y) pos)
            => Directions.Select(dir => (pos.x + dir.x, pos.y + dir.y));

        private static bool IsTakenNoCheck(char[,] plan, (int x, int y) pos) => plan[pos.x, pos.y] == TakenSeat;

        private static (int x, int y)? ScanSeat(char[,] plan, (int x, int y) pos, (int x, int y) direction)
        {
            if (!IsValidPosition(plan,pos))
                return null;

            if (plan[pos.x, pos.y] != Floor)
                return pos;

            return ScanSeat(plan, (pos.x + direction.x, pos.y + direction.y), direction);
        }

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