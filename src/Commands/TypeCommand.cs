namespace CommandParserApp;

public class TypeCommand(CommandRegistry commandRegistry) : ICommand
{
    public void Execute(string args)
    {
        if (commandRegistry.IsValidCommand(args))
        {
            Console.WriteLine($"{args} is a shell builtin");
        }
        else
        {
            SearchCommandInPath(args);
        }
    }

    private static void SearchCommandInPath(string args)
    {
        // Search for command in directories listed in Shell Path
        string? pathEnv = Environment.GetEnvironmentVariable("PATH");
        if (string.IsNullOrEmpty(pathEnv))
        {
            Console.WriteLine("PATH environment variable is not set");
            return;
        }
            
        // https://unix.stackexchange.com/questions/332948/how-does-lookup-in-path-work-under-the-hood
        string[] pathDirectories = pathEnv.Split(":");
        foreach (string directory in pathDirectories)
        {
            string fullPath = Path.Combine(directory, args);
            if (!File.Exists(fullPath)) continue;
            Console.WriteLine($"{args} is {fullPath}");
            return;
        }
            
        Console.WriteLine($"{args}: not found");
    }
}