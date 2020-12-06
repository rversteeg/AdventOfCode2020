using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day06 : PuzzleSolutionBase
    {
        public Day06() : base(6) {}

        public override string SolvePart1()
        {
            return GetInput()
                .Select(grp => 
                    grp.SelectMany(x => x)
                        .ToLookup(x => x).Count)
                .Sum().ToString();
        }

        public override string SolvePart2()
        {
            return GetInput()
                .Select(grp => 
                    grp.SelectMany(x => x)
                        .ToLookup(x => x)
                        .Count(x=>x.Count() == grp.Length))
                .Sum().ToString();
        }

        private string[][] GetInput()
        {
            return ReadAllInputText().Split($"{Environment.NewLine}{Environment.NewLine}")
                .Select(group => group.Split($"{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries)).ToArray();
        }

    }
}