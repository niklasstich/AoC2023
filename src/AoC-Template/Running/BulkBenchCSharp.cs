using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System.Reflection;

namespace AdventOfCode2022.Running;

[MemoryDiagnoser(false)]
[Orderer(SummaryOrderPolicy.Method, MethodOrderPolicy.Alphabetical)]
public class BulkBenchCSharp
{
    private BaseChallenge _challenge = null!;

    [ParamsSource(nameof(Challenges))]
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    public static Type Challenge { get; set; } = null!;

    [GlobalSetup]
    public void Setup()
    {
        _challenge = (BaseChallenge)Activator.CreateInstance(Challenge)!;
    }

    [Benchmark(Description = "Part 1")]
    public string PartOne() => _challenge.SolvePartOne();

    [Benchmark(Description = "Part 2")]
    public string PartTwo() => _challenge.SolvePartTwo();

    public static IEnumerable<Type> Challenges() =>
        Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(predicate: t => t.IsSubclassOf(typeof(BaseChallenge)));
}
