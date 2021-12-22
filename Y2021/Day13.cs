using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Util;

namespace AdventOfCode.Y2021
{
    public class Day13 : PuzzleSolutionWithParsedInput<Day13.Input>
    {
        public record Input(HashSet<(int X, int Y)> Points, IEnumerable<(char Dir, int Num)> Instructions);

        public Day13() : base(13, 2021) {}

        public override object SolvePart1(Input input)
        {
            return Fold(input.Points, input.Instructions.First()).Count;
        }

        public override object SolvePart2(Input input)
        {
            var result = input.Instructions.Aggregate(input.Points, Fold);

            var maxX = result.Max(x => x.X);
            var maxY = result.Max(x => x.Y);

            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    Console.Write(result.Contains((x, y)) ? "#" : " ");
                }

                Console.WriteLine();
            }

            return 0;
        }

        private HashSet<(int X, int Y)> Fold(HashSet<(int X, int Y)> points, (char Dir, int Num) instruction)
        {
            return points.Select(
                point=>
                    instruction.Dir switch
                    {
                        'x' => FoldX(point, instruction.Num),
                        'y' => FoldY(point, instruction.Num)
                    }
            ).ToHashSet();
        }

        private (int X, int Y) FoldX((int X, int Y) point, int number)
        {
            if (number > point.X)
                return point;

            return (number - (point.X - number), point.Y);
        }
        
        private (int X, int Y) FoldY((int X, int Y) point, int number)
        {
            if (number > point.Y)
                return point;

            return (point.X, number - (point.Y - number));
        }

        protected override Input Parse()
        {
            var lines = ReadAllInputLines();
            var pointLines = lines.TakeWhile(x => !String.IsNullOrWhiteSpace(x)).ToArray();
            var instructionLines = lines.SkipWhile(x => !String.IsNullOrWhiteSpace(x)).Skip(1).ToArray();
            var pointRegex = new Regex(@"(\d+),(\d+)");
            var instructionRegex = new Regex(@"fold along (.+)=(\d+)");
            return new Input(pointLines
                    .Select(line => line.Parse(pointRegex, (int x, int y) => (x, y))).ToHashSet(),
                instructionLines
                    .Select(line => line.Parse<(char,int), string, int>(instructionRegex, (axis, number)=>(axis[0], number)))
                    .ToList());
        }
    }
}