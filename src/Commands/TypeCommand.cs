using CommandParserApp.Utilities;

namespace CommandParserApp;

public class TypeCommand(CommandRegistry commandRegistry) : ICommand
{
    public void Execute(string args)
    {
        if (commandRegistry.IsShellBuiltInCommand(args))
        {
            Console.WriteLine($"{args} is a shell builtin");
        }
        else
        {
            string? executablePath = PathResolver.FindExecutableInPath(args);
            if (executablePath != null)
            {
                Console.WriteLine($"{args} is {executablePath}");
            }
            else
            {
                Console.WriteLine($"{args}: not found");
            }
        }
    }
}