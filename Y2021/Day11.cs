using System;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Util;

namespace AdventOfCode.Y2021
{
    public class Day11 : PuzzleSolutionWithParsedInput<int[][]>
    {
        public override object SolvePart1(int[][] input)
        {
            var flashes = 0;
            for (var step = 0; step < 100; step++)
            {
                for (var x = 0; x < 10; x++)
                for (var y = 0; y < 10; y++)
                    Increase(x, y, input);
                
                for (var x = 0; x < 10; x++)
                for (var y = 0; y < 10; y++)
                {
                    if (input[y][x] > 9)
                    {
                        flashes++;
                        input[y][x] = 0;
                    }
                }
            }

            return flashes;
        }

        private void Increase(int x, int y, int[][] input)
        {
            if (x < 0 || y < 0 || x >= 10 || y >= 10 || input[y][x] > 9)
                return;

            ++input[y][x];
            if (input[y][x] > 9)
            {
                Increase(x + 1, y - 1, input);
                Increase(x + 1, y, input);
                Increase(x + 1, y + 1, input);
                
                Increase(x - 1, y  - 1, input);
                Increase(x - 1, y, input);
                Increase(x - 1, y + 1, input);
                
                Increase(x, y - 1, input);
                Increase(x, y + 1, input);
            }
        }


        public override object SolvePart2(int[][] input)
        {
            for (var step = 0; step < Int32.MaxValue; step++)
            {
                for (var x = 0; x < 10; x++)
                for (var y = 0; y < 10; y++)
                    Increase(x, y, input);

                var turnFlashes = 0;
                for (var x = 0; x < 10; x++)
                for (var y = 0; y < 10; y++)
                {
                    if (input[y][x] > 9)
                    {
                        turnFlashes++;
                        input[y][x] = 0;
                    }
                }

                if (turnFlashes == 100)
                    return step + 1;
            }

            return -1;
        }

        protected override int[][] Parse()
            => ReadAllInputLines().Select(x => x.Select(chr=> chr - '0').ToArray()).ToArray();
    }
}