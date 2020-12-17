using System;
using System.Collections.Generic;
using AdventOfCode2020.Util;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day17 : PuzzleSolutionWithParsedInput<HashSet<(int x, int y, int z)>>
    {
        public Day17() : base(17) { }

        public override object SolvePart1(HashSet<(int x, int y, int z)> input)
        {
            var result = Turn(input, 6);

            return result.Count();
        }        

        private IList<(int x, int y, int z)> Directions = (from x in Enumerable.Range(-1, 3)
                                                                from y in Enumerable.Range(-1, 3)
                                                                from z in Enumerable.Range(-1, 3)
                                                                where x != 0 || y != 0 || z != 0
                                                                select (x,y,z)).ToList();

        private HashSet<(int x, int y, int z)> Turn(HashSet<(int x, int y, int z)> input, int nrOfTurns)
        {
            if (nrOfTurns == 0)
                return input;

            var adjecencyMap = input.SelectMany(item => Directions.Select(direction => (item.x + direction.x, item.y + direction.y, item.z + direction.z))).GroupBy(x => x);

            var result = (from item in adjecencyMap
                         let count = item.Count()
                         where count == 3 || input.Contains(item.Key) && count == 2
                         select item.Key).ToHashSet();

            return Turn(result, nrOfTurns - 1);
        }        

        public override object SolvePart2(HashSet<(int x, int y, int z)> input)
        {
            var input4D = input.Select(item => (item.x, item.y, item.z, 0)).ToHashSet();
            var result = Turn(input4D, 6);
            return result.Count();
        }

        private IList<(int x, int y, int z, int w)> Directions4D = (from x in Enumerable.Range(-1, 3)
                                                                    from y in Enumerable.Range(-1, 3)
                                                                    from z in Enumerable.Range(-1, 3)
                                                                    from w in Enumerable.Range(-1, 3)
                                                                    where x != 0 || y != 0 || z != 0 || w != 0
                                                                    select (x, y, z, w)).ToList();


        private HashSet<(int x, int y, int z, int w)> Turn(HashSet<(int x, int y, int z, int w)> input, int nrOfTurns)
        {
            if (nrOfTurns == 0)
                return input;

            var adjecencyMap = input.SelectMany(item => Directions4D.Select(direction => (item.x + direction.x, item.y + direction.y, item.z + direction.z, item.w + direction.w))).GroupBy(x => x);

            var result = (from item in adjecencyMap
                          let count = item.Count()
                          where count == 3 || input.Contains(item.Key) && count == 2
                          select item.Key).ToHashSet();

            return Turn(result, nrOfTurns - 1);
        }

        protected override HashSet<(int x, int y, int z)> Parse()
            => ReadInput().ToHashSet();

        private IEnumerable<(int x, int y, int z)> ReadInput()
        {
            var lines = ReadAllInputLines();
            for(int y = 0; y < lines.Length; y++)
            {
                for(int x = 0; x < lines[0].Length; x++)
                {
                    if (lines[y][x] == '#')
                        yield return new(x, y, 0);
                }
            }
        }
    }
}
