namespace MdServices.Base.Interfaces
{
    public interface ILoggerFactory
    {
        void AddConsole(object trace);
        ILogger CreateLogger<T>();
    }
}