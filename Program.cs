#if DEBUG
using AdventOfCode;

var runner = new PuzzleRunner(2021);
runner.RunAllDays();
#else
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark<AdventOfCode.Y2024.Day01>>();
#endif
