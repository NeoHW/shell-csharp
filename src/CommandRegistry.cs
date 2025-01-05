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

    public void ExecuteCommand(string commandWord, string args)
    {
        if (!IsValidCommand(commandWord))
        {
            HandleCommandNotFound(commandWord);
            return;
        }

        var command = _commands[commandWord];
        command.Execute(args);
    }

    public bool IsValidCommand(string commandWord)
    {
        return _commands.ContainsKey(commandWord);
    }

    public void HandleCommandNotFound(string commandWord)
    {
        Console.WriteLine($"{commandWord}: command not found");
    }
}