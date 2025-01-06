using System.Text.RegularExpressions;

namespace CommandParserApp.Utilities;

public static class CommandParserUtils
{
    const char Whitespace = ' ';
    const char SingleQuote = '\'';

    public static (string commandWord, List<string> args) ExtractCommandAndArgs(string userInput)
    {
        ArgumentNullException.ThrowIfNull(userInput);

        var args = ParseArguments(userInput);
        var commandWord = ExtractCommandWordFromArgs(args);

        return (commandWord, args);
    }

    private static List<string> ParseArguments(string userInput)
    {
        List<string> args = new();
        var currentWord = string.Empty;
        var inSingleQuote = false;

        foreach (var c in userInput)
        {
            switch (c)
            {
                case Whitespace:
                    HandleWhitespace(ref currentWord, ref args, ref inSingleQuote);
                    break;

                case SingleQuote:
                    HandleSingleQuote(ref inSingleQuote);
                    break;

                default:
                    currentWord += c;
                    break;
            }
        }

        AddRemainingWord(ref currentWord, ref args);

        return args;
    }

    private static void HandleWhitespace(ref string currentWord, ref List<string> args, ref bool inSingleQuote)
    {
        if (inSingleQuote)
        {
            currentWord += Whitespace; 
        }
        else
        {
            AddRemainingWord(ref currentWord, ref args);
        }
    }

    private static void HandleSingleQuote(ref bool inSingleQuote)
    {
        inSingleQuote = !inSingleQuote; 
    }

    private static void AddRemainingWord(ref string currentWord, ref List<string> args)
    {
        if (string.IsNullOrEmpty(currentWord)) return;
        args.Add(currentWord);
        currentWord = string.Empty;
    }

    private static string ExtractCommandWordFromArgs(List<string> args)
    {
        if (args.Count == 0) return string.Empty;

        var commandWord = args[0];
        args.RemoveAt(0); 
        return commandWord;
    }
}
