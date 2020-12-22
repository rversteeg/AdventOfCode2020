﻿using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2020
{
    public class Day22 : PuzzleSolutionWithParsedInput<Day22.Input>
    {
        public record Input(int[] Player1, int[] Player2);
        public Day22() : base(22, 2020) { }
        public override object SolvePart1(Input input)
        {
            Queue<int> player1 = new(input.Player1);
            Queue<int> player2 = new(input.Player2);

            while (player1.Any() && player2.Any())
            {
                var c1 = player1.Dequeue();
                var c2 = player2.Dequeue();

                if (c1 > c2)
                {
                    player1.Enqueue(c1);
                    player1.Enqueue(c2);
                }
                else
                {
                    player2.Enqueue(c2);
                    player2.Enqueue(c1);
                }
            }

            var winner = (player1.Any() ? player1 : player2).ToArray();
            return winner.Select((card, idx) => (winner.Length - idx) * card).Sum();
        }

        public override object SolvePart2(Input input)
        {
            var result = Round(input.Player1, input.Player2);
            return Math.Max(result.Item1, result.Item2);
        }

        private static (int,int) Round(IEnumerable<int> player1, IEnumerable<int> player2)
        {
            var deck1 = new Queue<int>(player1);
            var deck2 = new Queue<int>(player2);
            
            var turns = new HashSet<(string, string)>();

            while (deck1.Count > 0 && deck2.Count > 0)
            {
                var strDeck1 = String.Join(",", deck1);
                var strDeck2 = String.Join(",", deck2);
                if (turns.Contains((strDeck1, strDeck2)))
                    return (1, 0);
                turns.Add((strDeck1, strDeck2));
                
                var c1 = deck1.Dequeue();
                var c2 = deck2.Dequeue();

                int winner;

                if (c1 <= deck1.Count && c2 <= deck2.Count)
                {
                    var (score1, score2) = Round(deck1.Take(c1), deck2.Take(c2));
                    winner = score1 > score2 ? 0 : 1;
                }
                else
                {
                    winner = c1 > c2 ? 0 : 1;
                }
                if (winner == 0)
                {
                    deck1.Enqueue(c1);
                    deck1.Enqueue(c2);
                }
                else
                {
                    deck2.Enqueue(c2);
                    deck2.Enqueue(c1);
                }
            }

            return (deck1.Select((card, idx) => (deck1.Count - idx) * card).Sum(), deck2.Select((card, idx) => (deck2.Count - idx) * card).Sum());
        }

        protected override Input Parse()
        {
            var parts = ReadAllInputText().Split($"{Environment.NewLine}{Environment.NewLine}");
            return new Input(parts[0].Split($"{Environment.NewLine}").Skip(1).Select(Int32.Parse).ToArray(),
                parts[1].Split($"{Environment.NewLine}").Skip(1).Select(Int32.Parse).ToArray());
        }
    }
}