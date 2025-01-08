namespace CommandParserApp;

public class ConsoleOutputEngine : IOutputEngine
{
    public void Write(string message)
    {
        Console.Write(message);
    }

    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    } 
}