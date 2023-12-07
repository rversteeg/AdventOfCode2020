using System.Linq;
using AdventOfCode.Util;

namespace AdventOfCode.Y2023;

public class Day06 : PuzzleSolutionWithParsedInput<(int Duration, int Distance)[]>
{
    public override object SolvePart1((int Duration, int Distance)[] input)
    {
        return input.Select(x =>
        {
            var total = 0;
            for (int i = 1; i <= x.Duration; i++)
            {
                if (i * (x.Duration - i) > x.Distance)
                    total++;
            }
            return total;
        }).Aggregate(1, (x, y) => x * y);
    }

    public override object SolvePart2((int Duration, int Distance)[] input)
    {
        var duration = 40817772L;
        var distance = 219101213651089L;
        var total = 0;
        for (long i = 1; i <= 40817772; i++)
        {
            if (i * (duration - i) > distance)
                total++;
        }
        return total;
    }

    protected override (int Duration, int Distance)[] Parse()
    {
        return new[]
        {
            (40, 219), (81, 1012), (77, 1365), (72, 1089)
        };
    }
}