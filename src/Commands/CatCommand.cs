using System.Text;

namespace CommandParserApp;

public class CatCommand : ICommand
{
    public string? Execute(List<string?> args)
    {
        var outputBuilder = new StringBuilder();
        foreach (var path in args)
        {
            if (File.Exists(path))
            {
                outputBuilder.Append(File.ReadAllText(path).TrimEnd());
            }
            else
            {
                Console.WriteLine($"cat: {path}: No such file or directory");
            }
        }
        
        return outputBuilder.ToString().TrimEnd();
    }
}