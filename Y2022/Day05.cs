using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Util;

namespace AdventOfCode.Y2022;

public class Day05 : PuzzleSolutionWithParsedInput<(Stack<char>[] Stacks, Day05.CargoCommand[] Commands)>
{
    public override object SolvePart1((Stack<char>[] Stacks, CargoCommand[] Commands) input)
    {
        foreach (var command in input.Commands)
        {
            var fromStack = input.Stacks[command.From-1];
            var toStack = input.Stacks[command.To-1];

            for (int i = 0; i < command.Number; i++)
            {
                toStack.Push(fromStack.Pop());
            }
        }

        return new string(input.Stacks.Select(x => x.Pop()).ToArray());
    }

    public override object SolvePart2((Stack<char>[] Stacks, CargoCommand[] Commands) input)
    {
        foreach (var command in input.Commands)
        {
            var fromStack = input.Stacks[command.From-1];
            var toStack = input.Stacks[command.To-1];

            var moveStack = new Stack<char>();
            for (int i = 0; i < command.Number; i++)
            {
                moveStack.Push(fromStack.Pop());
            }

            while (moveStack.Count > 0)
            {
                toStack.Push(moveStack.Pop());
            }
        }

        return new string(input.Stacks.Select(x => x.Pop()).ToArray());
    }

    protected override (Stack<char>[] Stacks, CargoCommand[] Commands) Parse()
    {
        var allLines = ReadAllInputLines();

        var stackLines = allLines.TakeWhile(x => !Char.IsNumber(x[1])).ToList();
        var commandLines = allLines.SkipWhile(x => !Char.IsNumber(x[1])).Skip(2).ToList();

        var nrStacks = stackLines[0].Length / 4 + 1;

        var stacks = (from stackNum in Enumerable.Range(0, nrStacks)
            select new Stack<char>(stackLines.Select(line => line[stackNum * 4 + 1]).SkipWhile(char.IsWhiteSpace).Reverse())).ToArray();
        
        var commands = commandLines.Select(c =>
            c.Parse(new Regex(@"move (\d+) from (\d+) to (\d+)"), (int num, int from, int to) => new CargoCommand(num, from, to))).ToArray();

        return (stacks, commands);
    }

    public record CargoCommand(int Number, int From, int To);
}