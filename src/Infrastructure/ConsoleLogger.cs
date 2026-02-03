namespace CSharpSyntheticRepo.Infrastructure;

public enum LogLevel
{
    Trace = 0,
    Debug = 1,
    Info = 2,
    Warn = 3,
    Error = 4,
}


public sealed class ConsoleLogger
{
    private readonly object _lock = new();
    private readonly LogLevel _minLevel;

    public ConsoleLogger(LogLevel minLevel) => _minLevel = minLevel;

    // Overload set #1
    public void Log(string message) => Log(LogLevel.Info, message);

    // Overload set #1
    public void Log(LogLevel level, string message)
    {
        if (level < _minLevel) return;
        lock (_lock)
        {
            Console.WriteLine($"{DateTime.UtcNow:O} [{level}] {message}");
        }
    }

    // Overload set #1
    public void Log(Exception ex, string message = "Exception")
        => Log(LogLevel.Error, $"{message}: {ex.GetType().Name}: {ex.Message}");
}


