namespace MdServices.Base.Interfaces
{

    public class LoggerFactory : ILoggerFactory
    {
        public ILogger.LogLevel Level { get; set; }

        public void SetLevel(ILogger.LogLevel level)
        {
            Level = level;
        }

        public ILogger CreateLogger<T>()
        {
            return new Logger<T>(this);
        }
    }
}