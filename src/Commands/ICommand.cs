namespace CommandParserApp;

public interface ICommand
{
    void Execute(List<string?> args);
}