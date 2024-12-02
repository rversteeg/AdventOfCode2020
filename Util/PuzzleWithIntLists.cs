using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Util;

public abstract class PuzzleWithIntLists : PuzzleSolutionWithParsedInput<List<int[]>>
{
    protected PuzzleWithIntLists(){}
    protected PuzzleWithIntLists(int day, int year) : base(day, year) {}

    protected string Seperator { get; set; } = " ";

    protected override List<int[]> Parse()
        => ReadAllInputLines().Select(line=>line.Split(Seperator, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray()).ToList();
}