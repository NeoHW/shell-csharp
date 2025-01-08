namespace CommandParserApp;

public class EchoCommand : ICommand
{
    public string? Execute(List<string?> args)
    {
        return string.Join(" ", args);
    }
}