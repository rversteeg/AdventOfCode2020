using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2024;

public class Day01 : PuzzleSolutionWithParsedInput<(List<int> left, List<int> right) >
{
    public override object SolvePart1((List<int> left, List<int> right) input)
        => input.left.Order().Zip(input.right.Order(), (l, r) => Math.Abs(l - r)).Sum();

    public override object SolvePart2((List<int> left, List<int> right)  input)
    {
        var lookup = input.right.GroupBy(x=>x).Select(x=>(x.Key, x.Count() * x.Key)).ToDictionary(x=>x.Key, x=>x.Item2);
        return input.left.Select(x=>lookup.GetValueOrDefault(x, 0)).Sum();
    }

    protected override (List<int>, List<int>) Parse()
    {
        var result = (new List<int>(), new List<int>());
        
        foreach (var line in ReadAllInputLines())
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            result.Item1.Add(int.Parse(parts[0]));
            result.Item2.Add(int.Parse(parts[1]));
        }

        return result;
    }
}