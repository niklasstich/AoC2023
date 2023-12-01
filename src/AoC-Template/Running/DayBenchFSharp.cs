using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace AdventOfCode2022.Running;

[MemoryDiagnoser(false)]
[Orderer(SummaryOrderPolicy.Method, MethodOrderPolicy.Alphabetical)]
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