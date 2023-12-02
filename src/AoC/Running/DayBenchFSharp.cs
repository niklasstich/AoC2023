﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace AoC.Running;

[MemoryDiagnoser(false)]
[Orderer(SummaryOrderPolicy.Method, MethodOrderPolicy.Alphabetical)]
[InProcess]
public class DayBenchFSharp
{

    public static IEnumerable<BaseChallengeFSharp> Challenges => Program.DayInputFSharp;
    
    [ParamsSource(nameof(Challenges))]
    // ReSharper disable once FieldCanBeMadeReadOnly.Local
    public BaseChallengeFSharp Challenge = null!;

    [Benchmark(Description = "Part 1")] public string PartOne()
        => Challenge.SolvePartOne();

    [Benchmark(Description = "Part 2")] public string PartTwo()
        => Challenge.SolvePartTwo();
}