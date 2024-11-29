using AdventOfCode;
using BenchmarkDotNet.Attributes;

public class Benchmark<TSolution> where TSolution : IPuzzleSolution, new()
{
    private readonly TSolution _solution = new();

    [Benchmark]
    public void RunPart1()
    {
        _solution.SolvePart1();
    }

    [Benchmark]
    public void RunPart2()
    {
        _solution.SolvePart2();
    }
}