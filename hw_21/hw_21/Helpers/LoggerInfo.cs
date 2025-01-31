namespace hw_21.Helpers;

public class LoggerInfo
{
    private string _logsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
    
    public void Log(string message)
    {
        if (!Directory.Exists(_logsDirectory))
        {
            Directory.CreateDirectory(_logsDirectory);
        }

        string logFilePath = Path.Combine(_logsDirectory, "logs.txt");
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            writer.WriteLine($"{DateTime.Now}: {message}");
        }
    }
}