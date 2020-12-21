using System;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2020
{
    public class Day06 : PuzzleSolutionWithParsedInput<string[][]>
    {
        public Day06() : base(6, 2020) {}

        public override object SolvePart1(string[][] input)
        {
            return input
                .Select(grp => 
                    grp.SelectMany(x => x)
                        .ToLookup(x => x).Count)
                .Sum();
        }

        public override object SolvePart2(string[][] input)
        {
            return input
                .Select(grp => 
                    grp.SelectMany(x => x)
                        .ToLookup(x => x)
                        .Count(x=>x.Count() == grp.Length))
                .Sum();
        }

        protected override string[][] Parse()
            => ReadAllInputText().Split($"{Environment.NewLine}{Environment.NewLine}")
                .Select(group => group.Split($"{Environment.NewLine}")).ToArray();

    }
}