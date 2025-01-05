namespace CommandParserApp;

public class CdCommand : ICommand
{
    public void Execute(string args)
    {
        try
        {
            Directory.SetCurrentDirectory(args);
        }
        catch (Exception e)
        {
            if (e is DirectoryNotFoundException or FileNotFoundException)
            {
                Console.WriteLine("cd: {args}: No such file or directory");
            }
            else
                throw;
        }
    }
}