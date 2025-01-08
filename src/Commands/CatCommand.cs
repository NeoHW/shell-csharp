namespace CommandParserApp;

public class CatCommand : ICommand
{
    public string? Execute(List<string?> args)
    {
        string? output = null;
        foreach (var path in args)
        {
            if (File.Exists(path))
            {
                output += File.ReadAllText(path);
            }
            else
            {
                output += $"cat: {path}: No such file or directory";
            }
        }
        
        return output;
    }
}