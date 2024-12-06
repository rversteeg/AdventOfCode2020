using System;
using System.Collections.Generic;
using AdventOfCode.Util;
using Pos = (int X, int Y);
using Dir = (int X, int Y);

namespace AdventOfCode.Y2024;

public class Day06 : PuzzleWithCharGrid
{
    private static readonly Dir[] Directions = [(0, -1), (1, 0), (0, 1), (-1, 0)];
    private static readonly char[] DirectionsChars = ['^', '>', 'v', '<'];
    
    public override object SolvePart1(char[][] input)
    {
        var grid = new Dictionary<Pos, char>();
        var visited = new Dictionary<Pos, int>();
        Pos curPos = (-1, -1);
        var dirIndex = -1;
        
        foreach (var gridPos in EnumerableExtensions.RangeGrid(input[0].Length, input.Length))
        {
            var curChar = input[gridPos.Y][gridPos.X];
            grid[gridPos] = curChar;
            if (curChar is not '.' and not '#')
            {
                curPos = gridPos;
                dirIndex = Array.IndexOf(DirectionsChars, curChar);
            }
        }
        do
        {
            visited.TryAdd(curPos, dirIndex);
            (curPos, dirIndex) = Move(grid, curPos, dirIndex);
        } while (grid.ContainsKey(curPos));
        
        return visited.Keys.Count;
    }

    private static (Pos pos, int dirIndex) Move(Dictionary<Pos, char> grid, Pos pos, int dirIndex)
    {
        var dir = Directions[dirIndex];
        var nextPos = (pos.X + dir.X, pos.Y + dir.Y);
        if(grid.TryGetValue(nextPos, out var nextChar) && nextChar == '#')
            return Move(grid, pos, (dirIndex + 1) % 4);
        return (nextPos, dirIndex);
    }

    public override object SolvePart2(char[][] input)
    {
        var grid = new Dictionary<Pos, char>();
        var visited = new Dictionary<Pos, HashSet<int>>();
        Pos curPos = (-1, -1);
        var dirIndex = -1;
        
        foreach (var gridPos in EnumerableExtensions.RangeGrid(input[0].Length, input.Length))
        {
            var curChar = input[gridPos.Y][gridPos.X];
            grid[gridPos] = curChar;
            if (curChar is not '.' and not '#')
            {
                curPos = gridPos;
                dirIndex = Array.IndexOf(DirectionsChars, curChar);
            }
        }
        var startPos = curPos;
        var startDirIndex = dirIndex;
        
        do
        {
            visited.TryAdd(curPos, new HashSet<int>());
            visited[curPos].Add(dirIndex);
            (curPos, dirIndex) = Move(grid, curPos, dirIndex);
        } while (grid.ContainsKey(curPos));

        var result = 0;
        
        foreach(var pos in visited.Keys)
        {
            if(pos == startPos)
                continue;
            if (IsCyclical(new Dictionary<Pos, char>(grid) { [pos] = '#' }, startPos, startDirIndex))
                result++;
        }

        return result;
    }

    private static bool IsCyclical(Dictionary<Pos,char> grid, Pos startPos, int startDirIndex)
    {
        var visited = new Dictionary<Pos, HashSet<int>>();
        var curPos = startPos;
        var dirIndex = startDirIndex;
        
        do
        {
            if(visited.TryGetValue(curPos, out var visitedDirs) && visitedDirs.Contains(dirIndex))
                return true;
            
            visited.TryAdd(curPos, []);
            visited[curPos].Add(dirIndex);
            (curPos, dirIndex) = Move(grid, curPos, dirIndex);
        } while (grid.ContainsKey(curPos));

        return false;
    }
}