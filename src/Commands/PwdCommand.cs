namespace CommandParserApp;

public class PwdCommand : ICommand
{
    public string? Execute(List<string?> args)
    {
        return Directory.GetCurrentDirectory();
    }
}