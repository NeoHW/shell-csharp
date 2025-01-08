namespace CommandParserApp;

public class ExitCommand : ICommand
{
    private const int ExitCode = 0;

    public string? Execute(List<string?> args)
    {
        Environment.Exit(ExitCode);
        return null;
    }
}