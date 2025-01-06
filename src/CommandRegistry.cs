using CommandParserApp.Utilities;
using System.Diagnostics;

namespace CommandParserApp;

public class CommandRegistry
{
    private const string ExitCommand = "exit";
    private const string EchoCommand = "echo";
    private const string TypeCommand = "type";
    private const string PwdCommand = "pwd"; 
    private const string CdCommand = "cd"; 
    private readonly Dictionary<string, ICommand> _commands;

    public CommandRegistry()
    {
        _commands = new Dictionary<string, ICommand>(StringComparer.OrdinalIgnoreCase)
        {
            { ExitCommand, new ExitCommand() },
            { EchoCommand, new EchoCommand() },
            { TypeCommand, new TypeCommand(this) }, // Pass itself (Dependency Injection)
            { PwdCommand, new PwdCommand() },
            { CdCommand, new CdCommand() }
        };
    }

    public void ExecuteShellBuiltInCommand(string commandWord, List<string> args)
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
        var (executable, args) = CommandParserUtils.ExtractCommandAndArgs(userInput);
        
        string? executablePath = PathResolver.FindExecutableInPath(executable);
        if (executablePath == null)
        {
            return false;
        }
        
        using var process = new Process();
        process.StartInfo.FileName = executable;
        process.StartInfo.Arguments = string.Join(" ", args);
        process.Start();
        return true;
    }
}