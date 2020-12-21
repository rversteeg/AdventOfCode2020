using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Util;

namespace AdventOfCode2020
{
    public class Day21 : PuzzleSolutionWithParsedInput<Day21.Input[]>
    {
        public record Input(string[] Ingredients, string[] Allergens);

        public Day21() : base(21){}

        public override object SolvePart1(Input[] input)
        {
            var foundAllergen = FindIngredientsWithAllergens(input);
            var ingredientsWithAllergen = foundAllergen.Select(x => x.Value).ToHashSet();
            return input.SelectMany(x => x.Ingredients)
                .Count(ingredient => !ingredientsWithAllergen.Contains(ingredient));
        }

        private Dictionary<string, string> FindIngredientsWithAllergens(Input[] input)
        {
            var options =
                (from line in input
                    from allergen in line.Allergens
                    group line.Ingredients by allergen
                    into grp
                    select (grp.Key, IntersectAll(grp).ToHashSet())).OrderBy(x => x.Item2.Count())
                .ToList();

            var foundAllergen = new Dictionary<string, string>();

            while (foundAllergen.Count != options.Count)
            {
                var newMatches = from allergenOptions in options
                    where !foundAllergen.ContainsKey(allergenOptions.Key)
                          && allergenOptions.Item2.Count == 1
                    select allergenOptions;

                foreach (var newMatch in newMatches)
                {
                    var ingredient = newMatch.Item2.First();
                    foreach (var option in options)
                    {
                        option.Item2.Remove(ingredient);
                    }
                    foundAllergen.Add(newMatch.Key, ingredient);
                }
            }

            return foundAllergen;
        }

        private IEnumerable<string> IntersectAll(IEnumerable<IEnumerable<string>> input)
            => input.Aggregate((seed, val) => seed.Intersect(val));

        public override object SolvePart2(Input[] input)
            => string.Join(",", FindIngredientsWithAllergens(input).OrderBy(x => x.Key).Select(x => x.Value));

        protected override Input[] Parse()
            => ReadAllInputLines().Select(line =>
            {
                var parts = line.Split(new[] {' ', ',', ')'}, StringSplitOptions.RemoveEmptyEntries);
                var index = Array.IndexOf(parts, "(contains");
                return new Input(parts.Take(index).ToArray(), parts.Skip(index + 1).ToArray());
            }).ToArray();
    }
}