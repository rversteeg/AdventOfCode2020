using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2023;

public class Day07 : PuzzleSolutionWithParsedInput<IEnumerable<Day07.Input>>
{
    
    public override object SolvePart1(IEnumerable<Input> input)
    {
        return input
            .OrderByDescending(x => GetRank(x.Hand))
            .ThenBy(x => x.Hand, LabelComparer.Part1Instance)
            .Select((x, i) => x.Score * (i + 1))
            .Sum();
    }

    private static Rank GetRank(string hand)
    {
        var result = hand.GroupBy(x => x)
                .Select(x => (Card: x.Key, Count: x.Count()))
                .OrderByDescending(x => x.Count).Take(2).ToList() switch
            {
                [(_, 5), ..] => Rank.FiveOfAKind,
                [(_, 4), ..] => Rank.FourOfAKind,
                [(_, 3), (_, 2)] => Rank.FullHouse,
                [(_, 3), ..] => Rank.ThreeOfAKind,
                [(_, 2), (_, 2)] => Rank.TwoPair,
                [(_, 2), ..] => Rank.OnePair,
                _ => Rank.HighCard
            };

        return result;
    }
    
    private static Rank GetRankWithJoker(string hand)
    {
        var jokers = hand.Count(x => x == 'J');

        var result = hand.Where(x => x != 'J').GroupBy(x => x)
                .Select(x => (Card: x.Key, Count: x.Count()))
                .OrderByDescending(x => x.Count).Take(2).ToList() switch
            {
                [(_, 5), ..] => Rank.FiveOfAKind,
                [(_, 4), ..] => jokers == 1 ? Rank.FiveOfAKind : Rank.FourOfAKind,
                [(_, 3), (_, 2)] => Rank.FullHouse,
                [(_, 3), ..] => jokers switch
                {
                    2 => Rank.FiveOfAKind,
                    1 => Rank.FourOfAKind,
                    _ => Rank.ThreeOfAKind
                },
                [(_, 2), (_, 2)] => jokers == 1 ? Rank.FullHouse : Rank.TwoPair,
                [(_, 2), ..] => jokers switch
                {
                    3 => Rank.FiveOfAKind,
                    2 => Rank.FourOfAKind,
                    1 => Rank.ThreeOfAKind,
                    _ => Rank.OnePair
                },
                _ => jokers switch
                {
                    5 => Rank.FiveOfAKind,
                    4 => Rank.FiveOfAKind,
                    3 => Rank.FourOfAKind,
                    2 => Rank.ThreeOfAKind,
                    1 => Rank.OnePair,
                    _ => Rank.HighCard
                }
            };

        return result;
    }
    
    public override object SolvePart2(IEnumerable<Input> input)
    {
        return input
            .OrderByDescending(x => GetRankWithJoker(x.Hand))
            .ThenBy(x => x.Hand, LabelComparer.Part2Instance)
            .Select((x, i) => x.Score * (i + 1))
            .Sum();
    }

    protected override IEnumerable<Input> Parse()
        => ReadAllInputLines().Select(x => x.Split(' ')).Select(x=>new Input(x[0], Int32.Parse(x[1])));
    
    public record Input(string Hand, int Score);

    private enum Rank
    {
        FiveOfAKind,
        FourOfAKind,
        FullHouse,
        ThreeOfAKind,
        TwoPair,
        OnePair,
        HighCard
    }

    private class LabelComparer : IComparer<string>
    {
        private readonly Dictionary<char, int> _values;
        
        public static readonly LabelComparer Part1Instance = new(new()
        {
            { 'A', 14 },
            { 'K', 13 },
            { 'Q', 12 },
            { 'J', 11 },
            { 'T', 10 },
            { '9', 9 },
            { '8', 8 },
            { '7', 7 },
            { '6', 6 },
            { '5', 5 },
            { '4', 4 },
            { '3', 3 },
            { '2', 2 },
        });
            
        public static readonly LabelComparer Part2Instance = new(new()
        {
            { 'A', 14 },
            { 'K', 13 },
            { 'Q', 12 },
            { 'J', 1 },
            { 'T', 10 },
            { '9', 9 },
            { '8', 8 },
            { '7', 7 },
            { '6', 6 },
            { '5', 5 },
            { '4', 4 },
            { '3', 3 },
            { '2', 2 },
        });

        private LabelComparer(Dictionary<char, int> values)
        {
            _values = values;
        }


        public int Compare(string x, string y)
        {
            Debug.Assert(x != null, nameof(x) + " != null");
            Debug.Assert(y != null, nameof(y) + " != null");
            for (var i = 0; i < x.Length; i++)
            {
                if(x[i] == y[i]) continue;

                return _values[x[i]].CompareTo(_values[y[i]]);
            }
            return 0;
        }
    }
}