using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;
using AdventOfCode.Y2021;

namespace AdventOfCode.Y2023;

public class Day08 : PuzzleSolutionWithParsedInput<Day08.Input>
{
    public record Input(string Instructions, (string, string, string)[] Nodes);

    public override object SolvePart1(Input input)
    {
        var lookup = input.Nodes.ToDictionary(x => x.Item1, x => (x.Item2, x.Item3));
        var curNode = lookup["AAA"];
        var steps = 0;
        foreach (var chr in RepeatInfinite(input.Instructions))
        {
            var nextNode = chr == 'L' ? curNode.Item1 : curNode.Item2;
            ++steps;
            if (nextNode == "ZZZ")
                return steps;
            curNode = lookup[nextNode];
        }
        return -1;
    }

    private IEnumerable<char> RepeatInfinite(string input)
    {
        while (true)
        {
            foreach (var t in input)
                yield return t;
        }
    }

    public override object SolvePart2(Input input)
    {
        var directions = input.Nodes.ToDictionary(x => x.Item1, x => (Left: x.Item2, Right: x.Item3));

        var cycles = input.Nodes.Where(x => x.Item1.EndsWith('A')).Select(
            x => EndPositions(x.Item1, directions, input.Instructions).First()).ToArray();

        return LCM(cycles, cycles.Length);
    }
    
    static long LCM(long []arr, int n) 
    { 
        // Find the maximum value in arr[] 
        long max_num = 0; 
        for (int i = 0; i < n; i++)
        { 
            if (max_num < arr[i])
            { 
                max_num = arr[i]; 
            } 
        } 
 
        // Initialize result 
        long res = 1; 
 
        // Find all factors that are present 
        // in two or more array elements. 
        int x = 2; // Current factor. 
        while (x <= max_num)
        { 
            // To store indexes of all array 
            // elements that are divisible by x. 
           var indexes = new List<long>(); 
            for (long j = 0; j < n; j++)
            { 
                if (arr[j] % x == 0)
                { 
                    indexes.Add(j); 
                } 
            } 
 
            // If there are 2 or more array elements 
            // that are divisible by x. 
            if (indexes.Count >= 2) 
            { 
                // Reduce all array elements divisible 
                // by x. 
                for (int j = 0; j < indexes.Count; j++) 
                { 
                    arr[(int)indexes[j]] /= x; 
                } 
 
                res *= x; 
            } else
            { 
                x++; 
            } 
        } 
 
        // Then multiply all reduced 
        // array elements 
        for (int i = 0; i < n; i++) 
        { 
            res = res * arr[i]; 
        } 
 
        return res; 
    } 

    private IEnumerable<long> EndPositions(string startNode, Dictionary<string, (string Left, string Right)> directions,
        string instructions)
    {
        var step = 0L;
        var curNode = startNode;
        
        foreach (var chr in RepeatInfinite(instructions))
        {
            step++;
            curNode = chr == 'L' ? directions[curNode].Left : directions[curNode].Right;
            if (curNode.EndsWith('Z'))
                yield return step;
        }
    }

    protected override Input Parse()
    {
        var lines = ReadAllInputLines();

        return new Input(lines[0], lines.Skip(2).Select(ParseLine).ToArray());
    }

    private (string, string, string) ParseLine(string arg)
    {
        return (arg.Substring(0, 3),arg.Substring(7, 3),arg.Substring(12, 3));
    }
}