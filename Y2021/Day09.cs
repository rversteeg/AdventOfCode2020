using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2021
{
    public class Day09 : PuzzleSolutionWithParsedInput<char[][]>
    {
        public override object SolvePart1(char[][] input)
        {
            var width = input[0].Length;
            var height = input.Length;
            var lowPoints =
                from x in Enumerable.Range(0, width)
                from y in Enumerable.Range(0, height)
                where IsLowPoint(x, y, input)
                select (GetPoint(x,y, input) - '0')+1;

            return lowPoints.Sum();
        }

        private bool IsLowPoint(int x, int y, char[][] input)
        {
            var value = GetPoint(x,y,input);
            return GetPoint(x - 1, y, input) > value
                   && GetPoint(x + 1, y, input) > value
                   && GetPoint(x, y - 1, input) > value
                   && GetPoint(x, y + 1, input) > value;
        }

        private char GetPoint(int x, int y, char[][] input)
        {
            if (x < 0 || y < 0 || x >= input[0].Length || y >= input.Length)
                return '9';
            return input[y][x];
        }


        public override object SolvePart2(char[][] input)
        {
            var width = input[0].Length;
            var height = input.Length;
            var lowPoints =
                (from x in Enumerable.Range(0, width)
                    from y in Enumerable.Range(0, height)
                    where IsLowPoint(x, y, input)
                    select GetBasinSize(x, y, input)).OrderByDescending(x => x).Take(3).ToArray();

            return lowPoints[0] * lowPoints[1] * lowPoints[2];
        }

        private int GetBasinSize(int x, int y, char[][] input)
        {
            var value = GetPoint(x, y, input);
            if (value == '9' || value == 'x')
                return 0;
            input[y][x] = 'x';

            return 1 + GetBasinSize(x - 1, y, input) + GetBasinSize(x + 1, y, input) + GetBasinSize(x, y - 1, input) +
                   GetBasinSize(x, y + 1, input);
        }

        protected override char[][] Parse()
            => ReadAllInputLines().Select(x => x.ToArray()).ToArray();
    }
}