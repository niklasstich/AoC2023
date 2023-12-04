using System.CommandLine;
using AoC.Running;
using AoC.Utilities;
using BenchmarkDotNet.Running;

namespace AoC;

internal class Program
{
    public static IEnumerable<Type> DayInputTypes = null!;
    public static IEnumerable<BaseChallengeFSharp> DayInputFSharp = null!;

    private static void Main(string[] args)
    {
        var langOption = new Option<string>("--lang", () => "c#", "The language to run in.");
        var daysOption = new Option<IEnumerable<int>>("--days", "The days to run.")
            { AllowMultipleArgumentsPerToken = true };
        var daysOptionInRun = new Option<IEnumerable<int>>("--days", "The days to run.")
            { AllowMultipleArgumentsPerToken = true, IsRequired = true };
        var iterationsOption = new Option<int>("--iterations", () => 1, "The number of iterations to run.");

        var rootCommand = new RootCommand
        {
            langOption,
            daysOption,
        };
        rootCommand.SetHandler(RunWithBench, langOption, daysOption);

        var runCommand = new Command("run", "Run without benchmark.");
        runCommand.AddOption(langOption);
        runCommand.AddOption(daysOptionInRun);
        runCommand.AddOption(iterationsOption);
        runCommand.SetHandler(RunWithoutBench, langOption, daysOptionInRun, iterationsOption);

        var benchCommand = new Command("bench", "Run with benchmark.");
        benchCommand.AddOption(langOption);
        benchCommand.AddOption(daysOption);
        benchCommand.SetHandler(RunWithBench, langOption, daysOption);

        rootCommand.AddCommand(runCommand);
        rootCommand.AddCommand(benchCommand);

        rootCommand.Invoke(args);
    }

    private static void RunWithBench(string lang, IEnumerable<int> days)
    {
        var daysList = days.ToList();
        switch (lang, daysList.Count)
        {
            case ("c#", 0):
                BenchmarkRunner.Run<BulkBenchCSharp>();
                break;
            case ("f#", 0):
                BenchmarkRunner.Run<BulkBenchFSharp>();
                break;
            case ("c#", _):
                DayInputTypes = GetTypes(daysList);
                BenchmarkRunner.Run<DayBenchCSharp>();
                break;
            case ("f#", _):
                DayInputFSharp = GetFSharpChallenges(daysList);
                BenchmarkRunner.Run<DayBenchFSharp>();
                break;
        }

        return;

        IEnumerable<Type> GetTypes(IEnumerable<int> days)
        {
            foreach (var day in days)
            {
                var type = ReflectionUtilities.GetChallengeType(day);
                if (type is null)
                    Console.WriteLine($"No challenge found for day {day}.");
                else
                    yield return type;
            }
        }

        IEnumerable<BaseChallengeFSharp> GetFSharpChallenges(IEnumerable<int> days)
        {
            List<(BaseChallengeFSharp challenge, int)> challenges =
                BulkBenchFSharp.Challenges().Select(c => (c, int.Parse(c.DayIdentifier))).ToList();
            foreach (var day in days)
            {
                var challenge = challenges.FirstOrDefault(c => c.Item2 == day).challenge;
                if (challenge is null)
                    Console.WriteLine($"No challenge found for day {day}.");
                else
                    yield return challenge;
            }
        }
    }

    private static void RunWithoutBench(string lang, IEnumerable<int> days, int iterations)
    {
        switch (lang)
        {
            case "c#":
                RunCSharpWithoutBench(days, iterations);
                break;
            case "f#":
                RunFSharpWithoutBench(days, iterations);
                break;
        }
    }

    private static void RunFSharpWithoutBench(IEnumerable<int> days, int iterations)
    {
        for (var i = 0; i < iterations; i++)
        {
            foreach (var day in days)
            {
                var challenge = BulkBenchFSharp.Challenges().FirstOrDefault(c => int.Parse(c.DayIdentifier) == day);
                if (challenge is null)
                    Console.WriteLine($"No challenge found for day {day}.");
                else
                {
                    var solvePartOne = challenge.SolvePartOne();
                    var solvePartTwo = challenge.SolvePartTwo();
                    if (i != 0) continue;
                    Console.WriteLine($"Day {day} part 1: {solvePartOne}");
                    Console.WriteLine($"Day {day} part 2: {solvePartTwo}");
                }
            }
        }
    }

    private static void RunCSharpWithoutBench(IEnumerable<int> days, int iterations)
    {
        for (var i = 0; i < iterations; i++)
        {
            foreach (var day in days)
            {
                var type = ReflectionUtilities.GetChallengeType(day);
                if (type is null)
                    Console.WriteLine($"No challenge found for day {day}.");
                else
                {
                    var challenge = (BaseChallenge)Activator.CreateInstance(type)!;
                    if (i != 0) continue;
                    Console.WriteLine($"Day {day} part 1: {challenge.SolvePartOne()}");
                    Console.WriteLine($"Day {day} part 2: {challenge.SolvePartTwo()}");
                }
            }
        }
    }
}