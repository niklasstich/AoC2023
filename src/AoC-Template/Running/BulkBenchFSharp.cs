using System.Reflection;
using AoCFSharp.Days;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace AdventOfCode2022.Running;

[MemoryDiagnoser(false)]
[Orderer(SummaryOrderPolicy.Method, MethodOrderPolicy.Alphabetical)]
public class BulkBenchFSharp
{
    [ParamsSource(nameof(Challenges))]
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    public static BaseChallengeFSharp Challenge { get; set; } = null!;

    [Benchmark(Description = "Part 1")]
    public string PartOne() => Challenge.SolvePartOne();

    [Benchmark(Description = "Part 2")]
    public string PartTwo() => Challenge.SolvePartTwo();
    public static IEnumerable<BaseChallengeFSharp> Challenges()
    {
        return Assembly
                   .GetAssembly(typeof(Day01))?
                   .GetTypes()
                   .Select(t => (t, t.GetMethods()))
                   .Where(tup => tup.Item2.Any(m => m.Name == "part1") &&
                                 tup.Item2.Any(m => m.Name == "part2"))
                   .Select(tup => (tup.t, tup.Item2.Where(m => m.Name is "part1" or "part2")))
                   .Select(tup => new BaseChallengeFSharp(tup.t, tup.Item2))
               ?? Enumerable.Empty<BaseChallengeFSharp>();
    }
}