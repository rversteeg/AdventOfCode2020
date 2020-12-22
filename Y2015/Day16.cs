using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2015
{
    public class Day16 : PuzzleSolutionWithParsedInput<Dictionary<string,int>[]>
    {
        public Day16() : base(16,2015) { }

        private static readonly Dictionary<string,int> Analysis = @"children: 3
cats: 7
samoyeds: 2
pomeranians: 3
akitas: 0
vizslas: 0
goldfish: 5
trees: 3
cars: 2
perfumes: 1".Split(Environment.NewLine)
            .Select(line => line.Split(": "))
            .Select(x => (x[0], Int32.Parse(x[1])))
            .ToDictionary(x => x.Item1, x => x.Item2);

        public override object SolvePart1(Dictionary<string, int>[] input)
        {
            var result =
                (from sue in input.Select((data, idx) => (data, idx))
                    where sue.data.All(p => Analysis[p.Key] == p.Value)
                    select sue).ToList();

            return result.Single().idx + 1;
        }

        public override object SolvePart2(Dictionary<string, int>[] input)
        {
            var result =
                (from sue in input.Select((data, idx) => (data, idx))
                    where sue.data.All(IsMatch)
                    select sue).ToList();

            return result.Single().idx +1;
        }

        private static bool IsMatch(KeyValuePair<string, int> input)
            => input.Key switch
            {
                "cats" or "trees" => input.Value > Analysis[input.Key],
                "pomeranians" or "goldfish" => input.Value < Analysis[input.Key],
                _ => Analysis[input.Key] == input.Value
            };

        protected override Dictionary<string, int>[] Parse()
        {
            return (from line in ReadAllInputLines()
                let content = line.Substring(line.IndexOf(":", StringComparison.Ordinal) + 2)
                select (from propStr in content.Split(", ")
                    let parts = propStr.Split(": ")
                    select (parts[0], Int32.Parse(parts[1]))).ToDictionary(x => x.Item1, x => x.Item2)).ToArray();
        }
    }
}