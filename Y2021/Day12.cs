using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2021
{
    public class Day12 : PuzzleSolutionWithParsedInput<IEnumerable<(string @from, string to)>>
    {
        public Day12() : base(12, 2021) { }

        public override object SolvePart1(IEnumerable<(string @from, string to)> input)
        {
            var nodes = BuildNodes(input);

            var routes = FindRoutes(nodes, nodes["start"], new List<Node>(), true);

            return routes.Count();
        }
        
        private IEnumerable<IList<Node>> FindRoutes(Dictionary<string,Node> nodes, Node curNode, IList<Node> route, bool hasVisitedSmallTwice)
        {
            if (curNode == nodes["end"])
            {
                yield return route.Append(curNode).ToList();
                yield break;
            }

            if (curNode.IsSmall && route.Contains(curNode))
            {
                if(hasVisitedSmallTwice || curNode.Name == "start")
                    yield break;

                hasVisitedSmallTwice = true;
            }

            var curRoute = route.Append(curNode).ToList();

            foreach (var neighbor in curNode.Neighbors)
            {
                var routes = FindRoutes(nodes, neighbor, curRoute, hasVisitedSmallTwice);
                foreach (var newRoute in routes)
                    yield return newRoute;
            }
        }

        private static Dictionary<string, Node> BuildNodes(IEnumerable<(string @from, string to)> input)
        {
            var nodes = input.SelectMany(x => new[] { x.@from, x.to }).Distinct()
                .Select(x => new Node(x, new HashSet<Node>())).ToDictionary(x => x.Name, x => x);

            foreach (var (from, to) in input)
            {
                nodes[@from].Neighbors.Add(nodes[to]);
                nodes[to].Neighbors.Add(nodes[@from]);
            }

            return nodes;
        }

        public override object SolvePart2(IEnumerable<(string @from, string to)> input)
        {
            var nodes = BuildNodes(input);

            var routes = FindRoutes(nodes, nodes["start"], new List<Node>(), false);

            return routes.Count();
        }

        protected override IEnumerable<(string from, string to)> Parse()
        {
            return ReadAllInputLines().Select(line =>
            {
                var parts = line.Split("-");
                return (parts[0], parts[1]);
            }).ToArray();
        }
    }

    public record Node(string Name, HashSet<Node> Neighbors)
    {
        public bool IsSmall => Char.IsLower(Name[0]);
    }
}