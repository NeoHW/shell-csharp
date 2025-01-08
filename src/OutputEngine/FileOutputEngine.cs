namespace CommandParserApp;

public class FileOutputEngine : IOutputEngine
{
    private readonly StreamWriter _fileWriter;

    public FileOutputEngine(string filePath)
    {
        _fileWriter = new StreamWriter(filePath) { AutoFlush = true };
    }
 
    public void Write(string message)
    {
        _fileWriter.Write(message);
    }

    public void WriteLine(string message)
    {
        _fileWriter.WriteLine(message);
    } 
}