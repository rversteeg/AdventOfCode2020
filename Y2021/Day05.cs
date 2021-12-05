using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Util;

namespace AdventOfCode.Y2021
{
    public class Day05 : PuzzleSolutionWithParsedInput<IEnumerable<LineSegment>>
    {
        public Day05() : base(5, 2021){}

        public override object SolvePart1(IEnumerable<LineSegment> input)
            => input.Where(line => line.IsHorizontal || line.IsVertical)
                .SelectMany(line => line.ToPoints())
                .GroupBy(x => x)
                .Count(x => x.Count() > 1);

        public override object SolvePart2(IEnumerable<LineSegment> input)
            => input
                .SelectMany(line => line.ToPoints())
                .GroupBy(x => x)
                .Count(x => x.Count() > 1);

        protected override IEnumerable<LineSegment> Parse()
            => ReadAllInputLines().Select(LineSegment.Parse);
    }

    public record Point(long X, long Y);

    public record LineSegment(Point P1, Point P2)
    {
        public bool IsHorizontal => P1.Y == P2.Y;
        public bool IsVertical => P1.X == P2.X;

        public IEnumerable<Point> ToPoints()
        {
            var xDir = IsVertical ? 0 : P1.X < P2.X ? 1 : -1;
            var yDir = IsHorizontal ? 0 : P1.Y < P2.Y ? 1 : -1;

            var curPoint = P1;

            while (curPoint != P2)
            {
                yield return curPoint;
                curPoint = new Point(curPoint.X + xDir, curPoint.Y + yDir);
            }

            yield return curPoint;
        }

        private static readonly Regex ParseRegex = new(@"(\d+),(\d+) -> (\d+),(\d+)", RegexOptions.Compiled);
        public static LineSegment Parse(string line)
            => line.Parse( ParseRegex,
                (long x1, long y1, long x2, long y2) => new LineSegment(new Point(x1, y1), new Point(x2, y2)));
    }
}