using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2015
{
    public class Day19 : PuzzleSolutionWithParsedInput<((string from, string to)[] replacements, string inputString)>
    {
        public Day19() : base(19,2015) {}

        public override object SolvePart1(((string from, string to)[] replacements, string inputString) input)
        {
            var result = input.replacements.SelectMany(x => Replace(input.inputString, x)).Distinct().Count();
            return result;
        }

        private IEnumerable<string> Replace(string input, (string from, string to) replacement)
        {
            var start = -1;
            while ((start = input.IndexOf(replacement.Item1, start+1, StringComparison.Ordinal)) >= 0)
            {
                if (start == 0)
                {
                    yield return replacement.to + input.Substring(replacement.from.Length);
                }
                else
                {
                    yield return input.Substring(0, start) + replacement.to +
                                 input.Substring(replacement.from.Length + start);
                }
            }
        }

        public override object SolvePart2(((string from, string to)[] replacements, string inputString) input)
        {
            // var step = 0;
            // var checkedStrings = new HashSet<string>();
            // var newStrings = new List<string>() {input.inputString};
            //
            // do
            // {
            //     newStrings.ForEach(x =>checkedStrings.Add(x));
            //     newStrings = (from newString in newStrings
            //         from replacement in input.replacements
            //         let rep = (replacement.@from, replacement.to)
            //         from replaced in Replace(newString, rep)
            //         where !checkedStrings.Contains(replaced)
            //         select replaced).Distinct().ToList();
            //     step++;
            // } while (!newStrings.Contains("e"));

            return 0;
        }

        protected override ((string, string)[], string) Parse()
        {
            var parts = ReadAllInputText().Split($"{Environment.NewLine}{Environment.NewLine}");

            var replacements =
                (from line in parts[0].Split(Environment.NewLine)
                    let split = line.Split(" => ")
                    select (split[0], split[1])).ToArray();
            return (replacements, parts[1]);
        }
    }
}