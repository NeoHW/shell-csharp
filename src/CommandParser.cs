using CommandParserApp.Utilities;

namespace CommandParserApp;

public class CommandParser
{
    private readonly CommandRegistry _commandRegistry = new();
    private readonly IOutputEngine _outputEngine;

    public CommandParser(IOutputEngine outputEngine)
    {
        _outputEngine = outputEngine;
    }

    public void Run()
    {
        while (true)
        {
            PrintUserInputLine();
            var userInput = Console.ReadLine();
            
            var result = HandleUserInput(userInput);
            if (result != null) _outputEngine.WriteLine(result);
        }
    }

    private  void PrintUserInputLine()
    {
        _outputEngine.Write("$ ");
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