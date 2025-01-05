namespace CommandParserApp;

internal class Program
{
    private static void Main(string[] args)
    {
        var parser = new CommandParser();
        parser.Run();
    }
}