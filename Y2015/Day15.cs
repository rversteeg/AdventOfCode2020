using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2015
{
    public class Day15 : PuzzleSolutionWithParsedInput<Day15.Ingredient[]>
    {
        public record Ingredient(string Name, Property[] Properties);
        public record Property(string Name, int Value);

        public Day15() : base(15, 2015) {}

        public override object SolvePart1(Ingredient[] input)
        {
            var result = Permutations(input, 100)
                .Select(p => GetCombinedProperties(input, p.ToArray())
                    .Where(x => x.Key != "calories")
                    .Aggregate(1L, (seed, val) => seed * val.Value))
                .Max();
            return result;
        }

        private static Dictionary<string, long> GetCombinedProperties(Ingredient[] input, int[] amounts)
        {
            return (from idx in Enumerable.Range(0, amounts.Length)
                let ingredient = input[idx]
                from property in ingredient.Properties
                let amount = amounts[idx] * property.Value
                group (property.Name, amount) by property.Name
                into grp
                let propertyName = grp.Key
                let totalAmount = Math.Max(grp.Sum(x => x.amount), 0L)
                select (propertyName, totalAmount)).ToDictionary(x => x.propertyName, x => x.totalAmount);
        }

        public IEnumerable<IEnumerable<int>> Permutations(ArraySegment<Ingredient> input, int maxSpoons)
        {
            if (input.Count == 1)
                return new[] {new []{maxSpoons}};
            if (maxSpoons == 0)
                return new[] {new int[input.Count()]};

            return Enumerable.Range(0, maxSpoons)
                .SelectMany(numSpoons =>
                    Permutations(input.Slice(1), maxSpoons - numSpoons).Select(p => new[] {numSpoons}.Concat(p)));
        }

        public override object SolvePart2(Ingredient[] input)
        {
            var result = Permutations(input, 100)
                .Select(p => GetCombinedProperties(input, p.ToArray()))
                .Where(x=>x["calories"] == 500)
                .Select(p=> p.Where(x => x.Key != "calories")
                    .Aggregate(1L, (seed, val) => seed * val.Value))
                .Max();
            return result;
        }
        
        protected override Ingredient[] Parse()
            => (from line in ReadAllInputLines()
                let parts = line.Split(": ")
                let props = parts[1].Split(", ")
                let name = parts[0]
                let properties = (from prop in props
                    let propParts = prop.Split(" ")
                    select new Property(propParts[0], Int32.Parse(propParts[1]))).ToArray()
                select new Ingredient(name, properties)).ToArray();
    }
}