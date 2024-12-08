using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2024;

public class Day08 : PuzzleWithCharGrid
{
    public override object SolvePart1(char[][] input)
    {
        var antennas = EnumerableExtensions.RangeGrid(input[0].Length, input.Length)
            .Where(pos => input[pos.Y][pos.X] != '.')
            .Select(pos => (Pos: pos, Type: input[pos.Y][pos.X]))
            .ToList();

        var types = antennas.ToLookup(x => x.Type, x => x.Pos);
        var antinodes = new HashSet<(int X, int Y)>();
        
        foreach(var group in types)
        {
            foreach (var antenna in group)
            {
                foreach (var other in group)
                {
                    if(other == antenna)
                        continue;
                    
                    var diff = (X: other.X - antenna.X, Y: other.Y - antenna.Y);
                    var antinode = (X: other.X + diff.X, Y: other.Y + diff.Y);
                    if (antinode.X >= 0 && antinode.X < input[0].Length && antinode.Y >= 0 &&
                        antinode.Y < input.Length)
                    {
                        antinodes.Add(antinode);
                    }
                }
            }
        }
        
        return antinodes.Count;
    }

    public override object SolvePart2(char[][] input)
    {
        return 0;
    }
}