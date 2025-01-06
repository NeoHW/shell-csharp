using System.Text.RegularExpressions;

namespace CommandParserApp.Utilities;

public static class CommandParserUtils
{
    public static (string commandWord, List<string> args) ExtractCommandAndArgs(string userInput)
    {
        ArgumentNullException.ThrowIfNull(userInput);

        var args = ParseArguments(userInput);
        var commandWord = ExtractCommandWordFromArgs(args);

        return (commandWord, args);
    }

    private static List<string> ParseArguments(string userInput)
    {
        var args = new List<string>();
        var regex = new Regex(@"'([^']+)'|(\S+)", RegexOptions.Compiled);

        foreach (Match match in regex.Matches(userInput))
        {
            AddMatchToArguments(match, args);
        }

        return args;
    }

    private static void AddMatchToArguments(Match match, List<string> args)
    {
        if (IsQuotedMatch(match))
        {
            HandleQuotedMatch(match, args);
        }
        else if (IsUnquotedMatch(match))
        {
            args.Add(match.Groups[2].Value);
        }
    }

    private static void HandleQuotedMatch(Match match, List<string> args)
    {
        var quotedValue = match.Groups[1].Value;

        if (IsAdjacentToPreviousQuoted(args))
        {
            args[^1] = MergeWithPreviousQuoted(args[^1], quotedValue);
        }
        else
        {
            args.Add(quotedValue);
        }
    }

    private static bool IsQuotedMatch(Match match) => match.Groups[1].Success;

    private static bool IsUnquotedMatch(Match match) => match.Groups[2].Success;

    private static bool IsAdjacentToPreviousQuoted(List<string> args) =>
        args.Count > 0 && args[^1].EndsWith("'");

    private static string MergeWithPreviousQuoted(string previous, string current) =>
        previous.TrimEnd('\'') + current;

    private static string ExtractCommandWordFromArgs(List<string> args)
    {
        if (args.Count == 0) return string.Empty;

        var commandWord = args[0];
        args.RemoveAt(0); // Remove the command word from the arguments
        return commandWord;
    }
}