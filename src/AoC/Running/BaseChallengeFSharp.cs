using System.Reflection;
using System.Text.RegularExpressions;

namespace AoC.Running;

public partial class BaseChallengeFSharp
{
    public BaseChallengeFSharp(MemberInfo t, IEnumerable<MethodInfo> methods)
    {
        DayIdentifier = DayRegex().Match(t.Name).Value;
        var methodInfos = methods.ToList();
        Part1 = methodInfos.First(m => m.Name == "part1");
        Part2 = methodInfos.First(m => m.Name == "part2");
        Input = File.ReadAllLines(Path.Combine(InputDir, $"{DayIdentifier}{InputFileType}"));
    }

    public override string ToString() => $"Day {DayIdentifier}";

    [GeneratedRegex(@"\d+")]
    private static partial Regex DayRegex();

    private const string InputDir = "Inputs";
    private const string InputFileType = ".txt";
    
    private string[] Input { get; }

    public string DayIdentifier { get; }
    private MethodInfo Part1 { get; }
    private MethodInfo Part2 { get; }

    public string SolvePartOne() => Part1.Invoke(null, new object?[] { Input }) as string ?? string.Empty;
    public string SolvePartTwo() => Part2.Invoke(null, new object?[] { Input }) as string ?? string.Empty;
}