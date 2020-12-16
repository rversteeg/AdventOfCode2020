using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode2020.Util;

namespace AdventOfCode2020
{
    public class Day16 : PuzzleSolutionWithParsedInput<Day16.Input>
    {
        public record Input(FieldDef[] Fields, int[] MyTicket, int[][] Tickets);

        public record FieldDef(string Name, Rule[] Rules);

        public record Rule(int MinValue, int MaxValue);

        public Day16() : base(16)
        {
        }

        public override object SolvePart1(Input input)
        {
            return input.Tickets.SelectMany(x => x)
                .Where(val => !input.Fields.Any(field => IsValid(field, val)))
                .Sum();
        }

        private static bool IsValid(FieldDef field, int val)
        {
            return field.Rules.Any(rule => val >= rule.MinValue && val <= rule.MaxValue);
        }

        public override object SolvePart2(Input input)
        {
            var validTickets = input.Tickets
                .Where( ticket => ticket.All(value=>input.Fields.Any(field => IsValid(field, value))))
                .ToArray();

            var matches = new Dictionary<string, int>();

            while(matches.Count != input.Fields.Length)
            {
                var matchedFields =
                    (from field in input.Fields.Where(f => !matches.ContainsKey(f.Name))
                        from index in Enumerable.Range(0, input.Fields.Length)
                            .Where(idx => matches.All(m => m.Value != idx))
                        where validTickets.Select(ticket => ticket[index]).All(x => IsValid(field, x))
                        select new {field, index}).GroupBy(x=>x.field).Where(x=>x.Count()==1).ToList();


                matchedFields.ForEach(x => matches[x.Key.Name] = x.First().index);
            }

            var departureFields = input.Fields.Where(x => x.Name.StartsWith("departure")).ToList();
            var myDepartureValues = departureFields.Select(field => input.MyTicket[matches[field.Name]]).ToList();
            return myDepartureValues.Aggregate(1L, (val, field) => val * field);
        }

        protected override Input Parse()
        {
            var parts = ReadAllInputText().Split($"{Environment.NewLine}{Environment.NewLine}");

            var fields = parts[0].Split(Environment.NewLine).Select(ParseFieldDef).ToArray();
            var myTicket = parts[1].Split(Environment.NewLine)[1].Split(",").Select(int.Parse).ToArray();
            var tickets = parts[2].Split(Environment.NewLine).Skip(1)
                .Select(line => line.Split(",").Select(int.Parse).ToArray()).ToArray();

            return new Input(fields, myTicket, tickets);
        }

        private static readonly Regex FieldDefRegex = new Regex(@"^(.*): (\d+)-(\d+) or (\d+)-(\d+)$", RegexOptions.Compiled);
        
        private static FieldDef ParseFieldDef(string line)
        {
            var match = FieldDefRegex.Match(line);
            return new FieldDef(match.Groups[1].Value, new []
            {
                new Rule(int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value)),
                new Rule(int.Parse(match.Groups[4].Value), int.Parse(match.Groups[5].Value))
            });
        }
    }
}
