namespace MdServices.Base.Interfaces
{
    public interface ILoggerFactory
    {
        void SetLevel(ILogger.LogLevel trace);
        ILogger CreateLogger<T>();
    }
}