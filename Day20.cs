using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Util;
using Microsoft.Diagnostics.Tracing.Parsers.FrameworkEventSource;
using Microsoft.Extensions.Primitives;

namespace AdventOfCode2020
{
    public class Day20 : PuzzleSolutionWithParsedInput<Day20.Image[]>
    {
        public Day20() : base(20)
        {
        }

        public override object SolvePart1(Image[] input)
        {
            return input
                .SelectMany(x => x.GetBorders(), (image, border) => (image, border))
                .GroupBy(x => x.border)
                .Where(x=>x.Count() == 1)
                .GroupBy(x=>x.First().image.Id)
                .Where(x=>x.Count() == 2)
                .Select(x=>x.Key)
                .Aggregate(1L, (seed, val) => seed * val);
        }

        public override object SolvePart2(Image[] input) => null;

        protected override Image[] Parse()
        {
            return 
                ( from imageText in ReadAllInputText().Split($"{Environment.NewLine}{Environment.NewLine}")
                let parts = imageText.Split(Environment.NewLine)
                select new Image(int.Parse(parts[0].Substring(5,4)), ReadGrid(parts.Skip(1).ToArray())) ).ToArray();
        }

        private bool[,] ReadGrid(string[] input)
        {
            var result = new bool[input.Length, input.Length];
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input.Length; x++)
                {
                    result[x, y] = input[y][x] == '#';
                }
            }

            return result;
        }

        public record Image(int Id, bool[,] Pixels)
        {
            public IEnumerable<Border> GetBorders()
            {
                var length = Pixels.GetLength(0);
                yield return Border.ToBorder(Enumerable.Range(0, length).Select(idx => Pixels[0, idx]).ToArray(), 0);
                yield return Border.ToBorder(Enumerable.Range(0, length).Select(idx => Pixels[idx, 0]).ToArray(), 1);
                yield return Border.ToBorder(Enumerable.Range(0, length).Select(idx => Pixels[length-1, idx]).ToArray(), 2);
                yield return Border.ToBorder(Enumerable.Range(0, length).Select(idx => Pixels[idx, length-1]).ToArray(), 3);
            }
        }

        public record Border(int Value, int Side)
        {
            public virtual bool Equals(Border other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Value == other.Value;
            }

            public override int GetHashCode()
            {
                return Value;
            }

            public static Border ToBorder(bool[] Pixels, int Side)
            {
                int value = 0; int valueSwapped = 0;

                for (int bit = 0; bit < Pixels.Length; bit++)
                {
                    if (Pixels[bit])
                    {
                        value += 1 << bit;
                        valueSwapped += 1 << (Pixels.Length - 1 - bit);
                    }
                }

                return new Border(Math.Min(value, valueSwapped), Side);
            }
        }
    }
}
