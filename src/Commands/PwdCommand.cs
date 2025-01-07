namespace CommandParserApp;

public class PwdCommand : ICommand
{
    public void Execute(List<string?> args)
    {
        Console.WriteLine(Directory.GetCurrentDirectory());
    }
}