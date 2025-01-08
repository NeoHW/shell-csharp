using CommandParserApp.Utilities;

namespace CommandParserApp;

public class CommandParser
{
    private readonly CommandRegistry _commandRegistry = new();
    private readonly IOutputEngine _defaultOutputEngine;
    private IOutputEngine _currentOutputEngine;
    private IOutputEngine _currentOutputErrorEngine;

    public CommandParser(IOutputEngine defaultOutputEngine)
    {
        _defaultOutputEngine = defaultOutputEngine;
        _currentOutputEngine = _defaultOutputEngine;
        _currentOutputErrorEngine = _defaultOutputEngine;
    }

    public void Run()
    {
        while (true)
        {
            ResetOutputEngine();
            PrintUserInputLine();
            var userInput = Console.ReadLine();
            
            var (result, error)  = HandleUserInput(userInput);
            if (result != null) _currentOutputEngine.WriteLine(result);
            if (error != null) _currentOutputErrorEngine.WriteLine(error);
        }
    }

    private void ResetOutputEngine()
    {
        _currentOutputEngine = _defaultOutputEngine;
    }
    
    private void PrintUserInputLine()
    {
        _currentOutputEngine.Write("$ ");
    }


    private (string? output, string? error) HandleUserInput(string? userInput)
    {
        if (string.IsNullOrWhiteSpace(userInput))
        {
            return (null, "No command entered. Please try again.");
        }

        var (commandWord, args) = CommandParserUtils.ExtractCommandAndArgs(userInput);
        
        HandleRedirectOperatorIfPresent(args);
        
        return ExecuteCommand(commandWord, args);
    }

    private void HandleRedirectOperatorIfPresent(List<string?> args)
    {
        for (int i = 0; i < args.Count; i++)
        {
            if (args[i] == "1>" || args[i] == ">")
            {
                if (i + 1 < args.Count && args[i + 1] != null)
                {
                    var targetFile = args[i + 1]!;
                    try
                    {
                        _currentOutputEngine = new FileOutputEngine(targetFile);
                    }
                    catch (Exception e)
                    {
                        _currentOutputEngine.WriteLine($"Error: Unable to redirect output to file '{targetFile}': {e.Message}");
                    }
                    
                    // remove operator and file name from args
                    args.RemoveAt(i + 1);
                    args.RemoveAt(i);
                    break;
                } 
                
                _currentOutputEngine.WriteLine("Error: Missing target file for output redirection.");
            }
        }

    }

    private (string? output, string? error) ExecuteCommand(string commandWord, List<string?> args)
    {
        if (_commandRegistry.IsInCommandRegistry(commandWord))
        {
            return _commandRegistry.ExecuteShellBuiltInCommand(commandWord, args);
        }
        
        return  _commandRegistry.ExecuteExternalProgramCommand(commandWord, args);
    }
}