using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2020
{
    public class Day17 : PuzzleSolutionWithParsedInput<HashSet<(int x, int y, int z)>>
    {
        public Day17() : base(17, 2020) { }

        public override object SolvePart1(HashSet<(int x, int y, int z)> input)
        {            
            return Turn(input, _directions3D, Add3D, 6).Count();
        }

        private static (int x, int y, int z) Add3D((int x, int y, int z) left, (int x, int y, int z) right)
            => (left.x + right.x, left.y + right.y, left.z + right.z);

        private readonly IList<(int x, int y, int z)> _directions3D = (from x in Enumerable.Range(-1, 3)
                                                                from y in Enumerable.Range(-1, 3)
                                                                from z in Enumerable.Range(-1, 3)
                                                                where x != 0 || y != 0 || z != 0
                                                                select (x,y,z)).ToList();       

        private HashSet<TVector> Turn<TVector>(HashSet<TVector> input, IList<TVector> directions, Func<TVector, TVector, TVector> add, int nrOfTurns)
        {
            if (nrOfTurns == 0)
                return input;

            var adjacencyMap = input.SelectMany(item => directions.Select(direction => add(item, direction))).GroupBy(x => x);

            var result = (from item in adjacencyMap
                         let count = item.Count()
                         where count == 3 || input.Contains(item.Key) && count == 2
                         select item.Key).ToHashSet();

            return Turn(result, directions, add, nrOfTurns - 1);
        }        

        public override object SolvePart2(HashSet<(int x, int y, int z)> input)
        {
            return Turn(input.Select(item => (item.x, item.y, item.z, 0)).ToHashSet(), _directions4D, Add4D, 6).Count();
        }

        private static (int x, int y, int z, int w) Add4D((int x, int y, int z, int w) left, (int x, int y, int z, int w) right)
            => (left.x + right.x, left.y + right.y, left.z + right.z, left.w + right.w);

        private readonly IList<(int x, int y, int z, int w)> _directions4D = (from x in Enumerable.Range(-1, 3)
                                                                    from y in Enumerable.Range(-1, 3)
                                                                    from z in Enumerable.Range(-1, 3)
                                                                    from w in Enumerable.Range(-1, 3)
                                                                    where x != 0 || y != 0 || z != 0 || w != 0
                                                                    select (x, y, z, w)).ToList();

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
                        yield return (x, y, 0);
                }
            }
        }
    }
}
