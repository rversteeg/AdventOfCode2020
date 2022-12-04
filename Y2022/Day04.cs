using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Util;

namespace AdventOfCode.Y2022;

public class Day04 : PuzzleSolutionWithParsedInput<IEnumerable<((int Start, int Stop) First, (int Start, int Stop) Second)>>
{
    private static bool RangeFullyContained((int Start, int Stop) first, (int Start, int Stop) second)
        => second.Start >= first.Start && second.Stop <= first.Stop
           || first.Start >= second.Start && first.Stop <= second.Stop;

    public override object SolvePart1(IEnumerable<((int Start, int Stop) First, (int Start, int Stop) Second)> input)
        => input.Count(x => RangeFullyContained(x.First, x.Second));
    
    private static bool AnyOverlap((int Start, int Stop) first, (int Start, int Stop) second)
        => second.Start >= first.Start && second.Start <= first.Stop
           || second.Stop >= first.Stop && second.Stop <= first.Stop
           || first.Start >= second.Start && first.Start <= second.Stop
           || first.Stop >= second.Stop && first.Stop <= second.Stop;

    public override object SolvePart2(IEnumerable<((int Start, int Stop) First, (int Start, int Stop) Second)> input)
        => input.Count(x => AnyOverlap(x.First, x.Second));

    protected override IEnumerable<((int Start, int Stop) First, (int Start, int Stop) Second)> Parse()
        => ReadAllInputLines().Select(x => x.Parse(new Regex(@"(\d*)-(\d*),(\d*)-(\d*)"),
            (int s1, int e1, int s2, int e2) => ((s1, e1), (s2, e2)))).ToList();
}