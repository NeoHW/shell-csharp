using System.Text;

namespace CommandParserApp;

public class CatCommand : ICommand
{
    public (string? output, string? error) Execute(List<string?> args)
    {
        var outputBuilder = new StringBuilder();
        var errorBuilder = new StringBuilder();
        foreach (var path in args)
        {
            if (File.Exists(path))
            {
                outputBuilder.Append(File.ReadAllText(path).TrimEnd());
            }
            else
            {
                errorBuilder.Append($"cat: {path}: No such file or directory");
            }
        }
        
        return (outputBuilder.ToString().TrimEnd(), errorBuilder.ToString().TrimEnd());
    }
}