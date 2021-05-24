namespace MdServices.Base.Interfaces
{
    public interface ILogger
    {
        public enum LogLevel { None, Fatal, Error, Info, Debug, Trace };
        void LogInformation(string v);
        void LogError(string v);
        void LogWarning(string v);
    }
}