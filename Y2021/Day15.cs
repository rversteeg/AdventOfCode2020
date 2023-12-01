using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2021
{
    public class Day15 : PuzzleWithInt32Grid
    {
        record Node((int, int) Position, (int, int)? Via, long Cost)
        {
            public long Cost { get; set; } = Cost;
            public (int, int)? Via { get; set; } = Via;
        }

        public int Height { get; set; }

        public int Width { get; set; }
        
        public override object SolvePart1(int[][] input)
        {
            Width = input[0].Length;
            Height = input.Length;

            int CostFunc(int x, int y) => input[y][x];
            return Solve(CostFunc);
        }

        private object Solve(Func<int, int, int> costFunc)
        {
            HashSet<Node> visited = new();
            var nodes =
                (from x in Enumerable.Range(0, Width)
                    from y in Enumerable.Range(0, Height)
                    let cost = x == 0 && y == 0 ? 0 : long.MaxValue
                    select new Node((x, y), null, cost)).ToDictionary(x => x.Position, x => x);

            var toVisit = new PriorityQueue<Node, long>();
            toVisit.Enqueue(nodes[(0, 0)], 0);

            var targetNode = nodes[(Width - 1, Height - 1)];

            while (!visited.Contains(targetNode))
            {
                Node curNode;
                do
                {
                    curNode = toVisit.Dequeue();
                } while (visited.Contains(curNode));

                foreach (var p in GetNeighbours(curNode.Position))
                {
                    var cost = curNode.Cost + costFunc(p.x, p.y);
                    if (nodes[p].Cost > cost)
                    {
                        nodes[p].Cost = cost;
                        nodes[p].Via = curNode.Position;
                        toVisit.Enqueue(nodes[p], cost);
                    }
                }

                visited.Add(curNode);
            }

            return targetNode.Cost;
        }

        public override object SolvePart2(int[][] input)
        {
            var inputWidth = input[0].Length;
            var inputHeight = input.Length;
            Width = inputWidth*5;
            Height = inputHeight*5;
            
            int CostFunc(int x, int y) => (input[y % inputHeight][x % inputWidth] + y / inputHeight + x / inputWidth - 1)%9 + 1;
            return Solve(CostFunc);
        }

        private IEnumerable<(int x,int y)> GetNeighbours((int x, int y) position)
        {
            if (position.x > 0)
                yield return (position.x - 1, position.y);
            
            if(position.y > 0)
                yield return (position.x, position.y - 1);
            
            if(position.x < Width - 1)
                yield return (position.x + 1, position.y);
            
            if(position.y < Height - 1)
                yield return (position.x, position.y + 1);
        }
    }
}