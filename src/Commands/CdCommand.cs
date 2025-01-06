namespace CommandParserApp;

public class CdCommand : ICommand
{
    public void Execute(string args)
    {
        try
        {
            string targetDirectory = ResolveTargetDirectory(args);
            Directory.SetCurrentDirectory(targetDirectory);
        }
        catch (Exception e)
        {
            if (e is DirectoryNotFoundException or FileNotFoundException)
            {
                Console.WriteLine($"cd: {args}: No such file or directory");
            }
            else
                throw;
        }  
    }

    private static string ResolveTargetDirectory(string args)
    {
        if (string.IsNullOrEmpty(args))
        {
            throw new ArgumentException("cd: missing arguments");
        }
        
        return IsAbsolutePath(args) 
            ? args 
            : Path.GetFullPath(args, Directory.GetCurrentDirectory());
    }
    
    private static bool IsAbsolutePath(string path)
    {
        return Path.IsPathRooted(path);
    } 
}