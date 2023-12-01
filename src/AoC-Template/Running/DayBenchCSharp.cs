using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace AdventOfCode2022.Running;

[MemoryDiagnoser(false)]
[Orderer(SummaryOrderPolicy.Method, MethodOrderPolicy.Alphabetical)]
public class DayBenchCSharp
{

    private BaseChallenge _challenge = null!;
    public static IEnumerable<Type> Challenges => Program.DayInputTypes;
    
    [ParamsSource(nameof(Challenges))]
    // ReSharper disable once FieldCanBeMadeReadOnly.Local
    public Type Challenge = null!;

    [GlobalSetup] public void Setup()
    {
        _challenge = (BaseChallenge)Activator.CreateInstance(Challenge)!;
    }

    [Benchmark(Description = "Part 1")] public string PartOne()
        => _challenge.SolvePartOne();

    [Benchmark(Description = "Part 2")] public string PartTwo()
        => _challenge.SolvePartTwo();
}