using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2023;

public class Day04 : PuzzleSolutionWithParsedInput<IList<Day04.Input>>
{
    public override object SolvePart1(IList<Input> input)
        => input.Select(x => x.WinningNumbers.Intersect(x.MyNumbers).Count())
            .Where(x => x > 0)
            .Select(x => Math.Pow(2, x) / 2)
            .Sum();
    
    public override object SolvePart2(IList<Input> input)
    {
        var counts = Enumerable.Range(1, input.Count).ToDictionary(x => x, x => 1);

        foreach (var card in input)
        {
            var match = card.WinningNumbers.Intersect(card.MyNumbers).Count();

            for (int i = card.CardNum + 1; i <= card.CardNum + match; i++)
            {
                counts[i] += counts[card.CardNum];
            }
        }

        return counts.Select(x => x.Value).Sum();
    }

    protected override IList<Input> Parse() => ReadAllInputLines().Select((x, y) =>
    {
        var parts = x.Split(":")[1].Split("|");
        var winningNumbers = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray();
        var myNumbers = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray();
        return new Input(y + 1, winningNumbers, myNumbers);
    }).ToList();
    
    public record Input(int CardNum, int[] WinningNumbers, int[] MyNumbers);

}