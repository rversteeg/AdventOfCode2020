using System;
using System.Linq;
using AdventOfCode.Util;
using Sprache;

namespace AdventOfCode.Y2023;

public class Day05 : PuzzleSolutionWithParser<Day05.Input>
{
    public Day05() : base(Parser)
    {
    }

    protected override object SolvePart1(Input input)
    {
        return 0;
    }

    protected override object SolvePart2(Input input)
    {
        return 0;
    }

    public record Input(long[] Seeds, Map[] Mappings);
    public record Map(string From, string To, MapRange[] Ranges);
    public record MapRange(long Destination, long Source, long Length);

    private static readonly Parser<MapRange> MapRangeParser = from dest in Parse.Number
        from ws1 in Parse.WhiteSpace
        from src in Parse.Number
        from ws2 in Parse.WhiteSpace
        from length in Parse.Number
        select new MapRange(long.Parse(dest), long.Parse(src), long.Parse(length));


    private static readonly Parser<Map> MapParser = from fromCategory in Parse.Letter.Many().Text()
        from _ in Parse.String("-to-")
        from toCategory in Parse.Letter.Many().Text()
        from _2 in Parse.String(" map:")
        from newLine in Parse.LineEnd
        from ranges in MapRangeParser.DelimitedBy(Parse.LineEnd)
        select new Map(fromCategory, toCategory, ranges.ToArray());

    private static readonly Parser<Input> Parser = 
        from prefix in Parse.String("seeds: ")
        from seeds in Parse.Number.DelimitedBy(Parse.Char(' '))
        from ws in Parse.LineEnd.Repeat(2)
        from mappings in MapParser.DelimitedBy(Parse.LineEnd.Repeat(2))
            select new Input(seeds.Select(long.Parse).ToArray(), mappings.ToArray());
}