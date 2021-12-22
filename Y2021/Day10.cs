using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2021
{
    public class Day10 : PuzzleSolutionWithLinesInput
    {
        public Day10() : base(10, 2021)
        {
        }

        public override object SolvePart1(string[] input)
        {
            return input.Select(CheckLine).Where(x => x.corrupt).Select(x=>CharScore(x.firstInvalidChar)).Sum();
        }

        private long CharScore(char chr)
            => chr switch
            {
                ')' => 3,
                ']' => 57,
                '}' => 1197,
                '>' => 25137,
                _ => 0
            };

        private (bool corrupt, char firstInvalidChar, Stack<char> remaining) CheckLine(string line)
        {
            Stack<char> openChars = new Stack<char>();

            foreach (var chr in line)
            {
                switch (chr)
                {
                    case '(':
                    case '[':
                    case '{':
                    case '<':
                        openChars.Push(chr);
                        break;
                    case ')':
                        if (openChars.Pop() != '(')
                            return (true, chr, openChars);
                        break;
                    case ']':
                        if (openChars.Pop() != '[')
                            return (true, chr, openChars);
                        break;
                    case '}':
                        if (openChars.Pop() != '{')
                            return (true, chr, openChars);
                        break;
                    case '>':
                        if (openChars.Pop() != '<')
                            return (true, chr, openChars);
                        break;
                }
            }
            return (false, ' ', openChars);
        }

        public override object SolvePart2(string[] input)
        {
            var scores = input.Select(CheckLine).Where(x => !x.corrupt).Select(x=>ScoreClosing(x.remaining)).OrderBy(x=>x).ToArray();
            return scores[scores.Length / 2];
        }

        private long ScoreClosing(Stack<char> openingChars)
        {
            var score = 0L;
            while (openingChars.TryPop(out var opening))
            {
                score = score * 5 + opening switch
                {
                    '(' => 1,
                    '[' => 2,
                    '{' => 3,
                    '<' => 4,
                    _ => throw new ArgumentOutOfRangeException(nameof(openingChars))
                };
            }
            return score;
        }
    }
}