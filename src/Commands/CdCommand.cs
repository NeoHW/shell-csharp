namespace CommandParserApp;

public class CdCommand : ICommand
{
    public void Execute(List<string> args)
    {
        try
        {
            string directoryPath = args[0];
            string targetDirectory = ResolveTargetDirectory(directoryPath);
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

    private static string ResolveTargetDirectory(string directoryPath)
    {
        if (string.IsNullOrEmpty(directoryPath))
        {
            throw new ArgumentException("cd: missing arguments");
        }

        if (directoryPath.StartsWith('~'))
        {
            return Environment.GetEnvironmentVariable("HOME");
        }
        
        return IsAbsolutePath(directoryPath) 
            ? directoryPath 
            : Path.GetFullPath(directoryPath, Directory.GetCurrentDirectory());
    }
    
    private static bool IsAbsolutePath(string path)
    {
        return Path.IsPathRooted(path);
    } 
}