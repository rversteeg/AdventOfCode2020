using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2015
{
    public class Day18 : PuzzleSolutionWithParsedInput<bool[,]>
    {
        public Day18() : base(18, 2015) { }

        private static readonly IList<(int dX, int dY)> Directions =
            (from dX in Enumerable.Range(-1, 3)
                from dY in Enumerable.Range(-1, 3)
                where dX != 0 || dY != 0
                select (dX, dY)).ToList();
        
        public override object SolvePart1(bool[,] input)
        {
            for (int turn = 0; turn < 100; turn++)
            {
                var newGrid = new bool[input.GetLength(0), input.GetLength(1)];
                for(int x = 0; x < input.GetLength(0);x++)
                for (int y = 0; y < input.GetLength(1); y++)
                {
                    var adjacentCount = Directions.Count(dir => IsSet(input, (x + dir.dX, y + dir.dY)));
                    newGrid[x, y] = adjacentCount == 3 || input[x, y] && adjacentCount == 2;
                }
                
                input = newGrid;
            }

            var count =
                (from x in Enumerable.Range(0, input.GetLength(0))
                    from y in Enumerable.Range(0, input.GetLength(0))
                    where input[x, y]
                    select (x, y)).Count();

            return count;
        }
        
        private bool IsSet(bool[,] input, (int x, int y) position)
        {
            if (position.x < 0 ||
                position.y < 0 ||
                position.x >= input.GetLength(0) ||
                position.y >= input.GetLength(1))
            {
                return false;
            }

            return input[position.x, position.y];
        }

        public override object SolvePart2(bool[,] input)
        {
            HashSet<(int x, int y)> stuckLights = new HashSet<(int x, int y)>()
            {
                (0, 0),
                (0, input.GetLength(1) - 1),
                (input.GetLength(0) - 1, 0),
                (input.GetLength(0) - 1, input.GetLength(1) - 1)
            };
            foreach (var stuckLight in stuckLights)
            {
                input[stuckLight.x, stuckLight.y] = true;
            }
            for (int turn = 0; turn < 100; turn++)
            {
                var newGrid = new bool[input.GetLength(0), input.GetLength(1)];
                for(int x = 0; x < input.GetLength(0);x++)
                for (int y = 0; y < input.GetLength(1); y++)
                {
                    if (stuckLights.Contains((x, y)))
                    {
                        newGrid[x, y] = true;
                        continue;
                    }
                    
                    var adjacentCount = Directions.Count(dir => IsSet(input, (x + dir.dX, y + dir.dY)));
                    newGrid[x, y] = adjacentCount == 3 || input[x, y] && adjacentCount == 2;
                }
                
                input = newGrid;
            }

            var count =
                (from x in Enumerable.Range(0, input.GetLength(0))
                    from y in Enumerable.Range(0, input.GetLength(0))
                    where input[x, y]
                    select (x, y)).Count();

            return count;
        }

        protected override bool[,] Parse()
        {
            var lines = ReadAllInputLines();
            var result = new bool[lines[0].Length, lines.Length];
            for(int y=0; y < lines.Length; y++)
            for (int x = 0; x < lines[y].Length; x++)
            {
                result[x, y] = lines[y][x] == '#';
            }

            return result;
        }
    }
}