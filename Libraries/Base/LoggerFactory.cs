namespace MdServices.Base.Interfaces
{

    public class LoggerFactory : ILoggerFactory
    {
        public void AddConsole(object trace)
        {
        }

        public ILogger CreateLogger<T>()
        {
            return new Logger<T>();
        }
    }
}