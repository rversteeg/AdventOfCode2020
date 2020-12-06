using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode2020.Util;

namespace AdventOfCode2020
{
    public class Day04 : PuzzleSolutionWithParsedInput<Day04.Document[]>
    {
        public Day04() : base(4){}

        public override object SolvePart1(Document[] input)
        {
            string[] requiredClaims = { ClaimNames.BirthYear, ClaimNames.IssueYear, ClaimNames.ExpirationYear, ClaimNames.Height, ClaimNames.HairColor, ClaimNames.EyeColor, ClaimNames.PassportId };

            return input.Count(doc => requiredClaims.All(claim => doc.Claims.ContainsKey(claim)));
        }

        public override object SolvePart2(Document[] input)
        {
            var validators = new Dictionary<string, Func<string, bool>>()
            {
                {ClaimNames.BirthYear, NumberValidator(1920,2002) },
                {ClaimNames.IssueYear, NumberValidator(2010, 2020)},
                {ClaimNames.ExpirationYear, NumberValidator(2020,2030)},
                {ClaimNames.Height, HeightValidator()},
                {ClaimNames.HairColor, RegexValidator(@"^#[0-9a-f]{6}$")},
                {ClaimNames.EyeColor, SetValidator(new []{"amb", "blu", "brn", "gry", "grn", "hzl", "oth"})},
                {ClaimNames.PassportId, PidValidator()}
            };

            return input.Count(doc => validators.All(val => doc.Claims.ContainsKey(val.Key) && val.Value(doc.Claims[val.Key])));
        }

        private static Func<string, bool> NumberValidator(int min, int max)
            => value => int.TryParse(value, out int year) && year >= min && year <= max;

        private Func<string, bool> HeightValidator()
        {
            var cmValidator = NumberValidator(150, 193);
            var inchValidator = NumberValidator(59, 76);
            var heightRegex = new Regex(@"^(?<height>\d+)(?<unit>cm|in)$", RegexOptions.Compiled);

            return input =>
            {
                var match = heightRegex.Match(input);
                if (!match.Success)
                    return false;

                return match.Groups["unit"].Value switch
                {
                    "cm" => cmValidator(match.Groups["height"].Value),
                    "in" => inchValidator(match.Groups["height"].Value),
                    _ => false
                };
            };
        }

        private static Func<string, bool> SetValidator(string[] set) => set.Contains;

        private static Func<string, bool> PidValidator() => input => input.Length == 9 && input.All(char.IsDigit);

        private static Func<string, bool> RegexValidator(string regexStr)
        {
            var regex = new Regex(regexStr, RegexOptions.Compiled);

            return input => regex.IsMatch(input);
        }

        protected override Document[] Parse()
            => (from docLines in ReadAllInputText().Split($"{Environment.NewLine}{Environment.NewLine}")
                select new Document(
                    (from keyValue in docLines.Split(new []{$"{Environment.NewLine}", " "}, StringSplitOptions.RemoveEmptyEntries)
                        select keyValue.Split(":")).ToDictionary(x => x[0], x => x[1]))).ToArray();

        public record Document(Dictionary<string, string> Claims);

        public class ClaimNames
        {
            public const string BirthYear = "byr";
            public const string IssueYear = "iyr";
            public const string ExpirationYear = "eyr";
            public const string Height = "hgt";
            public const string HairColor = "hcl";
            public const string EyeColor = "ecl";
            public const string PassportId = "pid";
            public const string CountryId = "cid";
        }
    }
}