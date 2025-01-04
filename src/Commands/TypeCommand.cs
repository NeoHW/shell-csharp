namespace CommandParserApp;

public class TypeCommand(CommandRegistry commandRegistry) : ICommand {
    private readonly CommandRegistry _commandRegistry = commandRegistry;

    public void Execute(string args) {
        if (_commandRegistry.IsValidCommand(args)) {
            Console.WriteLine($"{args} is a shell builtin");
        } else {
            Console.WriteLine("invalid_command: not found");
        }
    }
}