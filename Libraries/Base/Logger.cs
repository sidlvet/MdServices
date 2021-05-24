using MdServices.Base.Interfaces;
using System;

namespace MdServices.Base
{
    public class Logger<T>: ILogger
    {
        private ILoggerFactory _factory;

        public Logger(ILoggerFactory factory)
        {
            _factory = factory;
        }      

        private static string TimeSpecification { get { return DateTime.Now.ToString("HH:mm:ss.fff"); } }

        void ILogger.Error(string v)
        {
            Console.WriteLine( TimeSpecification + " [ERROR] " + v);
        }

        void ILogger.Info(string v)
        {
            Console.WriteLine(TimeSpecification + " [INFO] " + v);
        }

        void ILogger.Warning(string v)
        {
            Console.WriteLine(TimeSpecification + " [WARNING] " + v);
        }

        void ILogger.Debug(string v)
        {
            Console.WriteLine(TimeSpecification + " [DEBUG] " + v);
        }

        void ILogger.Trace(string v)
        {
            Console.WriteLine(TimeSpecification + " [TRACE] " + v);
        }
    }
}