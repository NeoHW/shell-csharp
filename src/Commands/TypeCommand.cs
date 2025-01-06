using CommandParserApp.Utilities;

namespace CommandParserApp;

public class TypeCommand(CommandRegistry commandRegistry) : ICommand
{
    public void Execute(List<string> args)
    {
        var commandToCheck = args[0];
        
        if (commandRegistry.IsShellBuiltInCommand(commandToCheck))
        {
            Console.WriteLine($"{commandToCheck} is a shell builtin");
        }
        else
        {
            string? executablePath = PathResolver.FindExecutableInPath(commandToCheck);
            if (executablePath != null)
            {
                Console.WriteLine($"{commandToCheck} is {executablePath}");
            }
            else
            {
                Console.WriteLine($"{commandToCheck}: not found");
            }
        }
    }
}