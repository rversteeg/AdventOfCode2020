using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day04 : PuzzleSolutionBase
    {
        public Day04() : base(4)
        {
        }

        public override string SolvePart1()
        {
            string[] requiredClaims = { ClaimNames.BirthYear, ClaimNames.IssueYear, ClaimNames.ExpirationYear, ClaimNames.Height, ClaimNames.HairColor, ClaimNames.EyeColor, ClaimNames.PassportID };

            var documents = ReadInput();

            return documents.Count(doc => requiredClaims.All(claim => doc.Claims.ContainsKey(claim))).ToString();
        }

        public override string SolvePart2()
        {
            var documents = ReadInput().ToList();

            var validators = new Dictionary<string, Func<string, bool>>()
            {
                {ClaimNames.BirthYear, NumberValidator(1920,2002) },
                {ClaimNames.IssueYear, NumberValidator(2010, 2020)},
                {ClaimNames.ExpirationYear, NumberValidator(2020,2030)},
                {ClaimNames.Height, HeightValidator()},
                {ClaimNames.HairColor, RegexValidator(@"^#[0-9a-f]{6}$")},
                {ClaimNames.EyeColor, EnumValidator(new []{"amb", "blu", "brn", "gry", "grn", "hzl", "oth"})},
                {ClaimNames.PassportID, PidValidator()}
            };

            return documents.Count(doc => validators.All(val => doc.Claims.ContainsKey(val.Key) && val.Value(doc.Claims[val.Key]))).ToString();
        }

        private Func<string, bool> NumberValidator(int min, int max)
            => value => Int32.TryParse(value, out int year) && year >= min && year <= max;

        private Func<string, bool> HeightValidator()
        {
            var cmFunc = NumberValidator(150, 193);
            var inFunc = NumberValidator(59, 76);
            var heigtRegex = new Regex(@"^(?<height>\d+)(?<unit>cm|in)$", RegexOptions.Compiled);

            return input =>
            {
                var match = heigtRegex.Match(input);
                if (!match.Success)
                    return false;

                return match.Groups["unit"].Value switch
                {
                    "cm" => cmFunc(match.Groups["height"].Value),
                    "in" => inFunc(match.Groups["height"].Value),
                    _ => false
                };
            };
        }

        private Func<string, bool> EnumValidator(string[] possibleValues) => input => possibleValues.Contains(input);

        private Func<string, bool> PidValidator() => input => input.Length == 9 && input.All(Char.IsDigit);

        private Func<string, bool> RegexValidator(string regexStr)
        {
            var regex = new Regex(regexStr, RegexOptions.Compiled);

            return input => regex.IsMatch(input);
        }

        public IEnumerable<Document> ReadInput()
        {
            var claims = new Dictionary<string, string>();
            var lines = File.ReadAllLines(@"Input\Day04\Part1.txt");

            foreach (var line in lines)
            {
                if (String.IsNullOrWhiteSpace(line))
                {
                    yield return new Document(claims);
                    claims = new Dictionary<string, string>();
                }
                else
                {
                    AddClaimLine(claims, line);
                }
            }
            yield return new Document(claims);
        }

        private void AddClaimLine(Dictionary<string, string> claims, string line)
        {
            var claimPairs = line.Split();

            foreach (var claim in claimPairs)
            {
                var keyValue = claim.Split(":");
                claims[keyValue[0]] = keyValue[1];
            }
        }

        public record Document(Dictionary<string, string> Claims);

        public class ClaimNames
        {
            public const string BirthYear = "byr";
            public const string IssueYear = "iyr";
            public const string ExpirationYear = "eyr";
            public const string Height = "hgt";
            public const string HairColor = "hcl";
            public const string EyeColor = "ecl";
            public const string PassportID = "pid";
            public const string CountryID = "cid";
        }
    }
}