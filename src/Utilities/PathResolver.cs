namespace CommandParserApp.Utilities;

public static class PathResolver
{
    public static string? FindExecutableInPath(string executableName)
    {
        // Search for command in directories listed in Shell Path
        string? pathEnv = Environment.GetEnvironmentVariable("PATH");
        if (string.IsNullOrEmpty(pathEnv))
        {
            Console.WriteLine("PATH environment variable is not set");
            return null;
        }
            
        // https://unix.stackexchange.com/questions/332948/how-does-lookup-in-path-work-under-the-hood
        string[] pathDirectories = pathEnv.Split(Path.PathSeparator);
        foreach (string directory in pathDirectories)
        {
            string fullPath = Path.Combine(directory, executableName);
            if (File.Exists(fullPath))
            {
                return fullPath;
            }
        }
        return null;
    }
}