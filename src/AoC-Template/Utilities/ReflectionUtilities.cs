using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.RegularExpressions;
using AdventOfCode2022.Running;

namespace AdventOfCode2022.Utilities;

public static partial class ReflectionUtilities
{
    [GeneratedRegex(@"\d+")]
    private static partial Regex DayRegex();
    public static bool TryGetChallengeType(string challengeType, [NotNullWhen(true)] out Type? challenge)
    {
        challenge = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(predicate: t => t.Name == challengeType);

        return challenge is not null;
    }

    public static Type? GetChallengeType(int day) => Assembly
        .GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.BaseType == typeof(BaseChallenge))
        .FirstOrDefault(predicate: t => int.Parse(DayRegex().Match(t.Name).Value) == day);
}