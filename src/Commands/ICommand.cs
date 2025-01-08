namespace CommandParserApp;

public interface ICommand
{
    string? Execute(List<string?> args);
}