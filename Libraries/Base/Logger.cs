using MdServices.Base.Interfaces;
using System;

namespace MdServices.Base
{
    public class Logger<T>: ILogger
    {
        public Logger() { }

        private static string TimeSpecification { get { return DateTime.Now.ToString("HH:mm:ss.fff"); } }

        void ILogger.LogError(string v)
        {
            Console.WriteLine( TimeSpecification + " [ERROR] " + v);
        }

        void ILogger.LogInformation(string v)
        {
            Console.WriteLine(TimeSpecification + " [INFO] " + v);
        }

        void ILogger.LogWarning(string v)
        {
            Console.WriteLine(TimeSpecification + " [WARNING] " + v);
        }
    }
}