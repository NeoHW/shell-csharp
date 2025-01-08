using CommandParserApp.Utilities;

namespace CommandParserApp;

public class TypeCommand(CommandRegistry commandRegistry) : ICommand
{
    public string? Execute(List<string?> args)
    {
        string? resultToReturn = null;
        var commandToCheck = args[0];
        
        if (commandRegistry.IsShellBuiltInCommand(commandToCheck))
        {
            resultToReturn = $"{commandToCheck} is a shell builtin";
        }
        else
        {
            string? executablePath = PathResolver.FindExecutableInPath(commandToCheck);
            if (executablePath != null)
            {
                resultToReturn = $"{commandToCheck} is {executablePath}";
            }
            else
            {
                resultToReturn = $"{commandToCheck}: not found";
            }
        }
        return resultToReturn; 
    }
}