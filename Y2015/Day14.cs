using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Util;

namespace AdventOfCode.Y2015
{
    public class Day14 : PuzzleSolutionWithParsedInput<(string name, int speed, int duration, int restPeriod)[]>
    {
        public Day14() : base(14, 2015) {}

        public override object SolvePart1((string name, int speed, int duration, int restPeriod)[] input)
        {
            var distances = input.Select(deer =>
            {
                var fullCycle = 2503 / (deer.duration + deer.restPeriod);
                var remainder = Math.Min(2503 % (deer.duration + deer.restPeriod), deer.duration);
                return fullCycle * deer.duration * deer.speed + remainder * deer.speed;
            }).ToArray();

            return distances.Max();
        }

        public override object SolvePart2((string name, int speed, int duration, int restPeriod)[] input)
        {
            var distance = new long[input.Length];
            var scores = new long[input.Length];

            for (int i = 0; i < 2503; i++)
            {
                for (int deer = 0; deer < input.Length; deer++)
                {
                    if (i % (input[deer].duration + input[deer].restPeriod) < input[deer].duration)
                        distance[deer] += input[deer].speed;
                }

                foreach (var deerAhead in distance.Select((dist, idx) => (dist, idx)).GroupBy(x => x.dist)
                    .OrderByDescending(x => x.Key).First())
                {
                    scores[deerAhead.idx]++;
                }
            }

            return scores.Max();
        }
        
        private static readonly Regex InputRegex =new Regex("^(?<Name>[a-zA-Z]+) can fly (?<Speed>\\d+) km/s for (?<Duration>\\d+) seconds, but then must rest for (?<RestPeriod>\\d+) seconds.$");

        protected override (string name, int speed, int duration, int restPeriod)[] Parse()
            => ReadAllInputLines().Select(line =>
            {
                var match = InputRegex.Match(line);
                return (match.Groups["Name"].Value, Int32.Parse(match.Groups["Speed"].Value),Int32.Parse(match.Groups["Duration"].Value),Int32.Parse(match.Groups["RestPeriod"].Value));
            }).ToArray();
    }
}