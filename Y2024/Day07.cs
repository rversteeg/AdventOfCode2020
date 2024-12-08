using System;
using System.Linq;
using AdventOfCode.Util;
using Input = (long Result, long[] Items);

namespace AdventOfCode.Y2024;

public class Day07 : PuzzleSolutionWithParsedInput<(long Result, long[] Items)[]>
{
    public override object SolvePart1(Input[] input)
        => input.Where(x => IsPossible(x.Result, x.Items[0], x.Items.AsSpan()[1..])).Sum(x => x.Result);
    
    private static bool IsPossible(long result, long current, ReadOnlySpan<long> numbers)
    {
        if (current > result)
            return false;
        
        if(numbers.Length == 0)
            return current == result;

        var slice = numbers[1..];
        
        return IsPossible(result, current + numbers[0], slice) ||
               IsPossible(result, current * numbers[0], slice);
    }
    
    private static bool IsPossible2(long result, long current, ReadOnlySpan<long> numbers)
    {
        if (current > result)
            return false;
        
        if(numbers.Length == 0)
            return current == result;

        var slice = numbers[1..];
        
        return IsPossible2(result, current + numbers[0], slice) ||
               IsPossible2(result, current * numbers[0], slice) ||
               IsPossible2(result, Int64.Parse($"{current}{numbers[0]}"), slice);
    }

    public override object SolvePart2(Input[] input) 
        => input.Where(x => IsPossible2(x.Result, x.Items[0], x.Items.AsSpan()[1..])).Sum(x => x.Result);

    protected override Input[] Parse()
    {
        return ReadAllInputLines().Select(ParseLine).ToArray();
    }

    private Input ParseLine(string line)
    {
        var parts = line.Split(':');
        var numbers = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(Int64.Parse).ToArray();
        var result = Int64.Parse(parts[0]);
        return (result, numbers);
    }
}