using CommandParserApp.Utilities;

namespace CommandParserApp;

public class CommandParser
{
    private readonly CommandRegistry _commandRegistry = new();

    public void Run()
    {
        while (true)
        {
            PrintUserInputLine();
            var userInput = Console.ReadLine();
            
            var result = HandleUserInput(userInput);
            if (result != null) Console.WriteLine(result);
        }
    }

    private static void PrintUserInputLine()
    {
        Console.Write("$ ");
    }

    private string? HandleUserInput(string? userInput)
    {
        if (string.IsNullOrWhiteSpace(userInput))
        {
            return "No command entered. Please try again.";
        }

        var (commandWord, args) = CommandParserUtils.ExtractCommandAndArgs(userInput);
        if (_commandRegistry.IsInCommandRegistry(commandWord))
        {
            return _commandRegistry.ExecuteShellBuiltInCommand(commandWord, args);
        }
        
        string? executionResult = _commandRegistry.ExecuteExternalProgramCommand(userInput);
        return executionResult;
    }
}