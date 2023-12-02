﻿using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;
using Sprache;

namespace AdventOfCode.Y2023;

public class Day02 : PuzzleSolutionWithParsedInput<IList<Day02.Game>>
{
    public record Subset(IList<(int Number, string Color)> Cubes);
    public record Game(int Id, IEnumerable<Subset> Subsets);

    public override object SolvePart1(IList<Game> input)
        => (from game in input
                where game.Subsets.All(subset => !subset.Cubes.Any(c =>
                    c is { Color: "red", Number: > 12 } or { Color: "green", Number: > 13 } or
                        { Color: "blue", Number: > 14 }))
                select game).Select(x => x.Id).Sum();

    public override object SolvePart2(IList<Game> input)
    {
        return input.Select(game =>
            game.Subsets.SelectMany(x => x.Cubes).GroupBy(x => x.Color).Select(x => x.MaxBy(y => y.Number))
                .Aggregate(1, (val, item) => val * item.Number)).Sum();
    }

    protected override IList<Game> Parse()
        => ReadAllInputLines().Select(x => Parser.Parse(x)).ToList();
    
    private static readonly Parser<(int, string)> CubeParser =
        from leading in Sprache.Parse.WhiteSpace.Many()
        from number in Sprache.Parse.Number
        from ws in Sprache.Parse.WhiteSpace.Many()
        from color in Sprache.Parse.Letter.Many().Text()
        select (Int32.Parse(number), color);

    private static readonly Parser<Subset> SubsetParser =
        from cubes in CubeParser.DelimitedBy(Sprache.Parse.String(",").Token())
        select new Subset(cubes.ToList());
        
    private static readonly Parser<Game> Parser = from game in Sprache.Parse.String("Game ")
        from number in Sprache.Parse.Number
        from filler in Sprache.Parse.String(":")
        from subsets in SubsetParser.DelimitedBy(Sprache.Parse.String(";").Token())
        select new Game(Int32.Parse(number), subsets);
}