using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Util;
using Microsoft.Extensions.Primitives;

namespace AdventOfCode2020
{
    public class Day19 : PuzzleSolutionWithParsedInput<Day19.Input>
    {
        public Day19() : base(19) { }

        public interface IRule
        {
            IEnumerable<StringSegment> Match(StringSegment input, Dictionary<int, IRule> rules);
        }

        public record Input(Dictionary<int, IRule> Rules, string[] Messages);

        public record LeafRule(char Required) : IRule
        {
            public IEnumerable<StringSegment> Match(StringSegment input, Dictionary<int, IRule> rules) 
                => input.Length > 0 && input[0] == Required ? new[] {input.Subsegment(1)} : Enumerable.Empty<StringSegment>();
        }

        public record SingleRule(List<int> RuleList) : IRule
        {
            public IEnumerable<StringSegment> Match(StringSegment input, Dictionary<int, IRule> rules)
                => RuleList.Aggregate(
                    (IEnumerable<StringSegment>)new[] {input},
                    (options, rule) => options.SelectMany(option => rules[rule].Match(option, rules)));
        }

        public record MultiRule(List<SingleRule> RuleLists) : IRule
        {
            public IEnumerable<StringSegment> Match(StringSegment input, Dictionary<int, IRule> rules)
                => RuleLists.SelectMany(x => x.Match(input, rules));
        }

        public override object SolvePart1(Input input)
        {
            var ruleZero = input.Rules[0];
            return input.Messages.Count(message => ruleZero.Match(message, input.Rules).Any(match=>match.Length == 0));
        }

        public override object SolvePart2(Input input)
        {
            var ruleZero = input.Rules[0];

            input.Rules[8] = ParseMultiRule("42 | 42 8");
            input.Rules[11] = ParseMultiRule("42 31 | 42 11 31");

            return input.Messages.Count(message => ruleZero.Match(message, input.Rules).Any(match => match.Length == 0));
        }

        protected override Input Parse()
        {
            var parts = ReadAllInputText().Split($"{Environment.NewLine}{Environment.NewLine}");
            return new Input(
                (from line in parts[0].Split(Environment.NewLine)
                    let split = line.Split(":")
                    let number = int.Parse(split[0])
                    let rule = split[1].Contains('"')
                        ? ParseLeafRule(split[1])
                        : split[1].Contains('|')
                            ? ParseMultiRule(split[1])
                            : ParseSingleRule(split[1])
                    select (number, rule)).ToDictionary(x => x.number, x => x.rule),
                parts[1].Split(Environment.NewLine));
        }

        private IRule ParseLeafRule(string input)
        {
            return new LeafRule(input[2]);
        }

        private IRule ParseMultiRule(string input)
        {
            var subRules = input.Split("|").Select(ParseSingleRule).Cast<SingleRule>().ToList();
            return new MultiRule(subRules);
        }

        private IRule ParseSingleRule(string input)
        {
            return new SingleRule(input.Trim().Split(" ").Select(Int32.Parse).ToList());
        }
    }
}