using MdServices.Base.Interfaces;

namespace MdServices.Base
{
    public static class LocalContext
    {
        public static ILoggerFactory LoggerFactory { get; }

        static LocalContext()
        {
            LoggerFactory = new LoggerFactory();
            LoggerFactory.SetLevel(ILogger.LogLevel.Trace);
        }
    }

    public static class Context<T>
    {
        public static ILogger Logger { get; set; }

        static Context()
        {
            Logger = LocalContext.LoggerFactory.CreateLogger<T>();
            Logger.Info("Logger for type: " + typeof(T).FullName);
        }
    }
}
