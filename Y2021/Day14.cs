using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Util;

namespace AdventOfCode.Y2021
{
    public class Day14 : PuzzleSolutionWithParsedInput<(string Start, IEnumerable<(char P1, char P2, char Insert)> Rules)>
    {
        private static readonly Dictionary<char, long> Empty = new();

        public override object SolvePart1((string Start, IEnumerable<(char P1, char P2, char Insert)> Rules) input)
            => Solve(input, 10);
        
        private long Solve((string Start, IEnumerable<(char P1, char P2, char Insert)> Rules) input, int iterations)
        {
            var cache = input.Rules.ToDictionary(x => (x.P1, x.P2, 1),
                x => new Dictionary<char, long>() { { x.Insert, 1 } });

            for (int i = 2; i <= iterations; i++)
            {
                foreach (var rule in input.Rules)
                {
                    cache[(rule.P1, rule.P2, i)] = Add(
                        cache.GetValueOrDefault((rule.P1, rule.P2, 1), null),
                        cache.GetValueOrDefault((rule.P1, rule.Insert, i - 1), null),
                        cache.GetValueOrDefault((rule.Insert, rule.P2, i - 1), null));
                }
            }

            var result =
                input.Start.GroupBy(x => x).ToDictionary(x => x.Key, x => x.LongCount());

            for (int i = 0; i < input.Start.Length - 1; i++)
            {
                result = Add(result, cache.GetValueOrDefault((input.Start[i], input.Start[i + 1], iterations)));
            }

            var counts = result.Select(x => x.Value).OrderBy(x => x).ToList();

            return counts[^1] - counts[0];
        }

        private Dictionary<char,long> Add(params Dictionary<char,long>[] dictionaries)
        {
            return dictionaries.Where(x => x != null).Aggregate(new Dictionary<char, long>(), (state, add) =>
            {
                foreach (var (key, value) in add)
                {
                    state[key] = state.ContainsKey(key) ? state[key] + value : value;
                }

                return state;
            });
        }

        public override object SolvePart2((string Start, IEnumerable<(char P1, char P2, char Insert)> Rules) input)
            => Solve(input, 40);

        protected override (string Start, IEnumerable<(char P1, char P2, char Insert)> Rules) Parse()
        {
            var lines = ReadAllInputLines();
            var regex = new Regex("(.)(.) -> (.)");

            return (lines[0],
                lines.Skip(2).Select(line =>
                    line.Parse(regex,(char p1, char p2, char insert) => (p1, p2, insert))));
        }
    }
}