namespace CommandParserApp;

public class EchoCommand : ICommand
{
    public void Execute(List<string> args)
    {
        Console.WriteLine(string.Join(" ", args));
    }
}