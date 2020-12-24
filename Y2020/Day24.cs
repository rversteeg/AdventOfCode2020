using System;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Util;

namespace AdventOfCode.Y2020
{
    public class Day24 : PuzzleSolutionWithParsedInput<Day24.Direction[][]>
    {
        public enum Direction
        {
            E, Se, Sw, W, Nw, Ne
        }
        
        public Day24() : base(24, 2020) { }

        public override object SolvePart1(Direction[][] input)
        {
            return input.Select(GetOffset).GroupBy(x => x).Count(x => x.Count() % 2 != 0);
        }

        private static (int x, int y) GetOffset(Direction[] instructions)
        {
            (int x, int y) result = (0, 0);
            foreach (var instruction in instructions)
            {
                result = instruction switch
                {
                    Direction.E => (result.x + 1, result.y),
                    Direction.Se => (result.x + 1, result.y - 1),
                    Direction.Sw => (result.x, result.y - 1),
                    Direction.W => (result.x - 1, result.y),
                    Direction.Nw => (result.x - 1, result.y + 1),
                    Direction.Ne => (result.x, result.y + 1),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            return result;
        }

        private static (int x, int y) Move((int x, int y) pos, Direction dir)
        {
            return dir switch
            {
                Direction.E => (pos.x + 1, pos.y),
                Direction.Se => (pos.x + 1, pos.y - 1),
                Direction.Sw => (pos.x, pos.y - 1),
                Direction.W => (pos.x - 1, pos.y),
                Direction.Nw => (pos.x - 1, pos.y + 1),
                Direction.Ne => (pos.x, pos.y + 1),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static readonly (int dX, int dY)[] Directions = {
            Move((0,0), Direction.Nw), Move((0,0), Direction.Ne),
            Move((0,0), Direction.W), Move((0,0), Direction.E),
            Move((0,0), Direction.Sw), Move((0,0), Direction.Se),
        };

        public override object SolvePart2(Direction[][] input)
        {
            var blackTiles = input
                .Select(GetOffset)
                .GroupBy(x => x)
                .Where(x => x.Count() % 2 != 0)
                .Select(x => x.Key)
                .ToHashSet();

            for (int i = 0; i < 100; i++)
            {
                var adjacencyMap =
                    (from tile in blackTiles
                        from direction in Directions
                        let adjacent = (tile.x + direction.dX, tile.y + direction.dY)
                        group adjacent by adjacent
                        into grp
                        select (grp.Key, grp.Count())).ToList();

                blackTiles = 
                    (from adjacent in adjacencyMap
                    where blackTiles.Contains(adjacent.Key) && (adjacent.Item2 == 1 || adjacent.Item2 == 2)
                          || !blackTiles.Contains(adjacent.Key) && adjacent.Item2 == 2
                    select adjacent.Key).ToHashSet();
            }
            
            return blackTiles.Count();
        }

        private static readonly Regex InputRegex = new Regex("^(e|se|sw|w|nw|ne)+$");

        protected override Direction[][] Parse()
        {
            return ReadAllInputLines().Select(x =>
            {
                var match = InputRegex.Match(x).Groups[1].Captures.Select(x =>
                {
                    return x.Value switch
                    {
                        "e" => Direction.E,
                        "se" => Direction.Se,
                        "sw" => Direction.Sw,
                        "w" => Direction.W,
                        "nw" => Direction.Nw,
                        _ => Direction.Ne
                    };
                }).ToArray();
                return match;
            }).ToArray();
        }
    }
}