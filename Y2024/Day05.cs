using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2024;

public class Day05 : PuzzleSolutionWithParsedInput<((int, int)[] Rules, int[][] Updates)>
{
    public override object SolvePart1(((int, int)[] Rules, int[][] Updates) input)
    {
        var total = 0;

        foreach (var update in input.Updates)
        {
            var indexes = update.Select((x, i) => (x,i)).ToDictionary(x=>x.x, x=>x.i);
            if(input.Rules.All(rule => !indexes.ContainsKey(rule.Item1) || !indexes.ContainsKey(rule.Item2) || indexes[rule.Item1] <= indexes[rule.Item2]))
            {
                total += update[(update.Length-1) / 2];
            }
        }
        
        return total;
    }

    public override object SolvePart2(((int, int)[] Rules, int[][] Updates) input)
    {
        var total = 0;
        var comparer = new RuleComparer(input.Rules.ToHashSet());

        foreach (var update in input.Updates)
        {
            var indexes = update.Select((x, i) => (x,i)).ToDictionary(x=>x.x, x=>x.i);
            if(!input.Rules.All(rule => !indexes.ContainsKey(rule.Item1) || !indexes.ContainsKey(rule.Item2) || indexes[rule.Item1] <= indexes[rule.Item2]))
            {
                total += update.Order(comparer).ToArray()[update.Length /2];
            }
        }
        
        return total;
    }
    
    private class RuleComparer(HashSet<(int, int)> rules) : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (rules.Contains((x, y)))
                return 1;
            if (rules.Contains((y, x)))
                return -1;
            return 0;
        }
    }

    protected override ((int, int)[] Rules, int[][] Updates) Parse()
    {
        var lines = ReadAllInputLines();
        var instructions =
            lines.TakeWhile(x => !String.IsNullOrWhiteSpace(x))
                .Select(line => line.Split('|')).Select(parts => (int.Parse(parts[0]), int.Parse(parts[1])))
                .ToArray();

        var pages = lines.SkipWhile(x => !String.IsNullOrWhiteSpace(x)).Skip(1).Select(x => x.Split(',').Select(Int32.Parse).ToArray()).ToArray();
        return (instructions.ToArray(), pages);
    }
}