using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2021
{
    public class Day08 : PuzzleSolutionWithParsedInput<IEnumerable<Day08.Input>>
    {
        public record Input(IEnumerable<string> Signals, IEnumerable<string> Output)
        {
            public static Input ParseLine(string line)
            {
                var parts = line.Split(" | ");
                return new Input(parts[0].Split().Select(SortString), parts[1].Split().Select(SortString));
            }

            private static string SortString(string input)
                => String.Join("", input.OrderBy(x => x));
        }

        private readonly int[] _uniqueLengths = new[] { 2, 3, 4, 7 };
        public override object SolvePart1(IEnumerable<Input> input)
            => input.SelectMany(x=>x.Output).Count(x => _uniqueLengths.Contains(x.Length));

        public override object SolvePart2(IEnumerable<Input> input)
            => input.Select(SolveInput).Sum();

        private int SolveInput(Input input)
        {
            var knownChars = new Dictionary<char, string>()
            {
                { '1', input.Signals.First(x => x.Length == 2) },
                { '4', input.Signals.First(x => x.Length == 4) },
                { '7', input.Signals.First(x => x.Length == 3) },
                { '8', input.Signals.First(x => x.Length == 7) }
            };

            knownChars['9'] = input
                .Signals.First(x => x.Length == 6 
                                    && x.Intersect(knownChars['4']).Count() == 4);
            
            knownChars['0'] = input
                .Signals.First(x => x.Length == 6 
                                    && x.Intersect(knownChars['4']).Count() == 3 
                                    && x.Intersect(knownChars['1']).Count() == 2 );
            
            knownChars['6'] = input
                .Signals.First(x => x.Length == 6 
                                    && x.Intersect(knownChars['4']).Count() == 3 
                                    && x.Intersect(knownChars['1']).Count() == 1 );
            
            knownChars['5'] = input
                .Signals.First(x => x.Length == 5 
                                    && x.Intersect(knownChars['6']).Count() == 5 );
            
            knownChars['3'] = input
                .Signals.First(x => x.Length == 5 
                                    && x.Intersect(knownChars['7']).Count() == 3 );
            
            knownChars['2'] = input
                .Signals.First(x => x.Length == 5 
                                    && x.Intersect(knownChars['4']).Count() == 2 );

            return Int32.Parse(String.Join("", input.Output.Select(x => FindNumber(knownChars, x))));
        }

        private char FindNumber(Dictionary<char, string> knownChars, string outputSignal)
            => knownChars.Single(x => x.Value == outputSignal ).Key;

        protected override IEnumerable<Input> Parse()
            => ReadAllInputLines().Select(Input.ParseLine);
    }
}