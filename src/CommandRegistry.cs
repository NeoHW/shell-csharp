using CommandParserApp.Utilities;
using System.Diagnostics;

namespace CommandParserApp;

public class CommandRegistry
{
    private const string ExitCommand = "exit";
    private const string EchoCommand = "echo";
    private const string TypeCommand = "type";
    private readonly Dictionary<string, ICommand> _commands;

    public CommandRegistry()
    {
        _commands = new Dictionary<string, ICommand>(StringComparer.OrdinalIgnoreCase)
        {
            { ExitCommand, new ExitCommand() },
            { EchoCommand, new EchoCommand() },
            { TypeCommand, new TypeCommand(this) } // Pass itself (Dependency Injection)
        };
    }

    public void ExecuteShellBuiltInCommand(string commandWord, string args)
    {
            var command = _commands[commandWord];
            command.Execute(args);
    }

    public bool IsShellBuiltInCommand(string commandWord)
    {
        return _commands.ContainsKey(commandWord);
    }

    public bool ExecuteExternalProgramCommand(string userInput)
    {
        string[] parts = userInput.Split(" ", 2);
        var executable = parts[0];
        var args = parts.Length > 1 ? parts[1] : string.Empty;
        
        string? executablePath = PathResolver.FindExecutableInPath(executable);
        if (executablePath == null)
        {
            return false;
        }
        
        using var process = new Process();
        process.StartInfo.FileName = executable;
        process.StartInfo.Arguments = args;
        process.Start();
        return true;
    }
}