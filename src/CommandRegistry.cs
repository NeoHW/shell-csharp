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
    private const string CatCommand = "cat"; 
    private readonly Dictionary<string, ICommand> _commands;

    public CommandRegistry()
    {
        _commands = new Dictionary<string, ICommand>(StringComparer.OrdinalIgnoreCase)
        {
            { ExitCommand, new ExitCommand() },
            { EchoCommand, new EchoCommand() },
            { TypeCommand, new TypeCommand(this) }, // Pass itself (Dependency Injection)
            { PwdCommand, new PwdCommand() },
            { CdCommand, new CdCommand() },
            { CatCommand, new CatCommand() },
        };
    }

    public string? ExecuteShellBuiltInCommand(string? commandWord, List<string?> args)
    {
            if (commandWord is null) return null;
            
            var command = _commands[commandWord];
            return command.Execute(args);
    }

    public bool IsInCommandRegistry(string? commandWord)
    {
        return !string.IsNullOrWhiteSpace(commandWord) && _commands.ContainsKey(commandWord);
    }
    
    public bool IsShellBuiltInCommand(string? commandWord)
    {
        return !string.IsNullOrEmpty(commandWord) && commandWord != CatCommand && _commands.ContainsKey(commandWord);
    }
    
    public string? ExecuteExternalProgramCommand(string userInput)
    {
        var (executable, args) = CommandParserUtils.ExtractCommandAndArgs(userInput);
        
        string? executablePath = PathResolver.FindExecutableInPath(executable);
        if (executablePath == null)
        {
            return  $"{userInput}: command not found";
        }
        
        var startInfo = new ProcessStartInfo
        {
            FileName = executable,
            Arguments = string.Join(" ", args),
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        
        using var process = new Process { StartInfo = startInfo };
        process.Start();
        
        var output = process.StandardOutput.ReadToEnd();
        var error = process.StandardError.ReadToEnd();

        process.WaitForExit();

        if (!string.IsNullOrEmpty(output))
            return output;
        if (!string.IsNullOrEmpty(error))
            return error;

        return null;
    }
}