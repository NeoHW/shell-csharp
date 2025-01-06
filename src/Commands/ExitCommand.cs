namespace CommandParserApp;

public class ExitCommand : ICommand
{
    private const int ExitCode = 0;

    public void Execute(List<string> args)
    {
        Environment.Exit(ExitCode);
    }
}