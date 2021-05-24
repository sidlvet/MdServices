namespace MdServices.Base.Interfaces
{
    public interface ILogger
    {
        public enum LogLevel { None, Fatal, Error, Info, Debug, Trace };

        void Info(string v);
        void Error(string v);
        void Warning(string v);
        void Debug(string v);
        void Trace(string v);
    }
}