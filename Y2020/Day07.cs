using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Util;

namespace AdventOfCode.Y2020
{
    public class Day07 : PuzzleSolutionWithParsedInput<Day07.Rule[]>
    {
        private readonly Regex _ruleRegex = new(@"^(?<color>[a-z ]+) bags contain ((?<requirementNumber>\d+) (?<requirementColor>[a-z ]+) bags?(, )?)+\.$", RegexOptions.Compiled);
        
        public Day07() : base(7, 2020) { }

        public override object SolvePart1(Rule[] input)
            => input.Count(rule => ContainsColor(rule.Color, "shiny gold", input.ToDictionary(x => x.Color)));

        private bool ContainsColor(string color, string containedColor, Dictionary<string, Rule> rules)
            => rules[color].Requirements.Any(req => req.Color == containedColor || ContainsColor(req.Color, containedColor, rules));

        public override object SolvePart2(Rule[] input) 
            => ContainedBags("shiny gold", input.ToDictionary(x => x.Color));

        private int ContainedBags(string color, Dictionary<string, Rule> rules)
            => rules[color].Requirements.Sum(requirement => requirement.Number + ContainedBags(requirement.Color, rules)*requirement.Number);

        protected override Rule[] Parse()
            => ReadInput().ToArray();

        private IEnumerable<Rule> ReadInput()
        {
            var lines = ReadAllInputLines();

            foreach (var rule in lines.Where(x => x.EndsWith(" bags contain no other bags.")))
            {
                yield return new Rule(rule.Substring(0, rule.IndexOf(" bags contain no other bags.", StringComparison.Ordinal)), Enumerable.Empty<Requirement>());
            }
            
            foreach (var rule in lines.Where(x => !x.EndsWith(" bags contain no other bags.")))
            {
                var match = _ruleRegex.Match(rule);

                var color = match.Groups["color"].Value;

                var requirements = match.Groups["requirementColor"].Captures.Select((capture, idx) =>
                    new Requirement(capture.Value,
                        int.Parse(match.Groups["requirementNumber"].Captures[idx].Value))).ToList();

                yield return new Rule(color, requirements);
            }
        }


        public record Rule(string Color, IEnumerable<Requirement> Requirements);

        public record Requirement(string Color, int Number);

    }
}