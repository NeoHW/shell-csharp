namespace CommandParserApp;

public class CatCommand : ICommand
{
    public void Execute(List<string?> args)
    {
        foreach (var path in args)
        {
            if (File.Exists(path))
            {
                Console.Write(File.ReadAllText(path));
            }
            else
            {
                Console.WriteLine($"cat: {path}: No such file or directory");
            }
        }
    }
}