using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2021
{
    public class Day04 : PuzzleSolutionWithLinesInput
    {
        private const int CardSize = 5;
        public Day04() : base(4, 2021) { }

        public override object SolvePart1(string[] input)
        {
            var bingo = Parse(input);
            foreach (var number in bingo.Numbers)
            {
                foreach (var card in bingo.Cards)
                {
                    if (CheckNumber(card, number))
                    {
                        return number * GetRemainingNumberSum(card);
                    }
                }
            }

            return 0;
        }

        private int GetRemainingNumberSum(int[,] card)
        {
            return (from x in Enumerable.Range(0, CardSize)
                from y in Enumerable.Range(0, CardSize)
                where card[x, y] >= 0
                select card[x, y]).Sum();
        }

        private bool CheckNumber(int[,] card, int number)
        {
            for (int x = 0; x < CardSize; x++)
            {
                for (int y = 0; y < CardSize; y++)
                {
                    if (card[x, y] == number)
                    {
                        card[x, y] = number == 0 ? -99 : -card[x, y];
                        return CheckBingo(card, (x, y));
                    }
                }
            }

            return false;
        }

        private bool CheckBingo(int[,] card, (int x, int y) lastPosition)
        {
            return IsColChecked(card, lastPosition.x) || IsRowChecked(card, lastPosition.y);
        }

        private bool IsRowChecked(int[,] card, int y)
        {
            return Enumerable.Range(0, CardSize).All(x => card[x, y] < 0);
        }

        private bool IsColChecked(int[,] card, int x)
        {
            return Enumerable.Range(0, CardSize).All(y => card[x, y] < 0);
        }

        public override object SolvePart2(string[] input)
        {
            var bingo = Parse(input);
            var alreadyWon = new HashSet<int>();
            
            foreach (var number in bingo.Numbers)
            {
                for (int cardNum = 0; cardNum < bingo.Cards.Count; cardNum++)
                {
                    if (!alreadyWon.Contains(cardNum))
                    {
                        if (CheckNumber(bingo.Cards[cardNum], number))
                        {
                            alreadyWon.Add(cardNum);
                            if(alreadyWon.Count == bingo.Cards.Count)
                                return number * GetRemainingNumberSum(bingo.Cards[cardNum]);
                        }
                    }
                }
            }

            return 0;
        }

        public (int[] Numbers, List<int[,]> Cards) Parse(string[] input)
        {
            var numbers = input[0].Split(',').Select(int.Parse).ToArray();
            var cards = new List<int[,]>();
            var numberOfCards = (input.Length - 1) / (CardSize + 1);

            for (int cardNum = 0; cardNum < numberOfCards; cardNum++)
            {
                var card = new int[CardSize, CardSize];
                for (int line = 0; line < CardSize; line++)
                {
                    var cardNumbers = input[cardNum * (CardSize + 1) + 2 + line].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                    for (int number = 0; number < cardNumbers.Count; number++)
                    {
                        card[line, number] = cardNumbers[number];
                    }
                }

                cards.Add(card);
            }
            
            return (numbers,cards);
        }
    }
}