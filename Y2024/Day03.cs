
using System;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Util;

namespace AdventOfCode.Y2024;

public partial class Day03 : PuzzleSolutionWithTextInput
{
    public override object SolvePart1(string input) 
        => Part1Regex().Matches(input)
            .Select(x=>Int32.Parse(x.Groups["left"].Value) * Int32.Parse(x.Groups["right"].Value))
            .Sum();

    public override object SolvePart2(string input)
    {
        var matches = Part2Regex().Matches(input);
        var enabled = true;
        var sum = 0;
        foreach (Match match in matches)
        {
            if(match.Value.Equals("do()"))
                enabled = true;
            else if (match.Value.Equals("don't()"))
                enabled = false;
            else if (enabled)
            {
                sum += Int32.Parse(match.Groups["left"].Value) * Int32.Parse(match.Groups["right"].Value);
            }
        }
        return sum;
    }
    
    [GeneratedRegex(@"mul\((?<left>\d+),(?<right>\d+)\)")]
    private static partial Regex Part1Regex();
    
    [GeneratedRegex(@"((mul\((?<left>\d+),(?<right>\d+)\))|(do\(\))|(don't\(\)))")]
    private static partial Regex Part2Regex();
}