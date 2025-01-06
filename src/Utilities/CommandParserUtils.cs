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
        var regex = new Regex(@"'([^']*)'|(\S+)", RegexOptions.Compiled);

        var matches = regex.Matches(userInput);
        return matches
            .Select(match => match.Groups[1].Success ? match.Groups[1].Value
                                                     : match.Groups[2].Value)
            .ToList();
    }

    private static string ExtractCommandWordFromArgs(List<string> args)
    {
        if (args.Count == 0) return string.Empty;

        var commandWord = args[0];
        args.RemoveAt(0); // Remove the command word from the arguments
        return commandWord;
    }
}