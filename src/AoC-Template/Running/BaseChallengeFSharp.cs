using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Running;

public partial class BaseChallengeFSharp
{
    public BaseChallengeFSharp(MemberInfo t, IEnumerable<MethodInfo> methods)
    {
        DayIdentifier = DayRegex().Match(t.Name).Value;
        var methodInfos = methods.ToList();
        Part1 = methodInfos.First(m => m.Name == "part1");
        Part2 = methodInfos.First(m => m.Name == "part2");
    }

    public override string ToString() => $"Day {DayIdentifier}";

    [GeneratedRegex(@"\d+")]
    private static partial Regex DayRegex();

    private const string InputDir = "Inputs";
    private const string InputFileType = ".txt";

    private string GetInput() => File.ReadAllText(Path.Combine(InputDir, $"{DayIdentifier}{InputFileType}"));

    public string DayIdentifier { get; }
    private MethodInfo Part1 { get; }
    private MethodInfo Part2 { get; }

    public string SolvePartOne() => Part1.Invoke(null, new object?[] { GetInput() }) as string ?? string.Empty;
    public string SolvePartTwo() => Part2.Invoke(null, new object?[] { GetInput() }) as string ?? string.Empty;
}