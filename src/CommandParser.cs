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

        var (commandWord, args) = ExtractCommandAndArgs(userInput);
        _commandRegistry.ExecuteCommand(commandWord, args);
    }

    private (string commandWord, string args) ExtractCommandAndArgs(string command)
    {
        var parts = command.Split(" ", 2);
        var commandWord = parts.Length > 0 ? parts[0] : string.Empty;
        var args = parts.Length > 1 ? parts[1] : string.Empty;
        return (commandWord, args);
    }
}