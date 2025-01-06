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
            HandleUserInput(userInput);
        }
    }

    private static void PrintUserInputLine()
    {
        Console.Write("$ ");
    }

    private void HandleUserInput(string? userInput)
    {
        if (string.IsNullOrWhiteSpace(userInput))
        {
            Console.WriteLine("No command entered. Please try again.");
            return;
        }

        var (commandWord, args) = CommandParserUtils.ExtractCommandAndArgs(userInput);
        if (_commandRegistry.IsShellBuiltInCommand(commandWord))
        {
            _commandRegistry.ExecuteShellBuiltInCommand(commandWord, args);
        }
        else
        {
            bool executionResult = _commandRegistry.ExecuteExternalProgramCommand(userInput);
            HandleExecutionResult(executionResult, userInput);
        }
    }

    private static void HandleExecutionResult(bool executionResult, string userInput)
    {
        if (!executionResult)
        {
            HandleCommandNotFound(userInput);
        }
    }
    
    private static void HandleCommandNotFound(string commandWord)
    {
        Console.WriteLine($"{commandWord}: command not found");
    }
}