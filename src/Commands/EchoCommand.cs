namespace CommandParserApp;

public class EchoCommand : ICommand
{
    public void Execute(string args)
    {
        Console.WriteLine(args);
    }
}