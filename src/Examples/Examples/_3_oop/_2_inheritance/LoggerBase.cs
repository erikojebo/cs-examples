using System;
using System.Diagnostics;

namespace Examples._3_oop._2_inheritance
{
    public class LoggerBase
    {
        protected string CreateLogMessage(Exception exception)
        {
            return string.Format("{0} ERROR: {1}, Stacktrace: {2}", DateTime.Now, exception.Message, exception.StackTrace);
        }
    }

    public class InheritedTraceLogger : LoggerBase, ILogger
    {
        public void Log(Exception exception)
        {
            var message = CreateLogMessage(exception);
            Trace.WriteLine(message);
        }
    }

    public class InheritedConsoleLogger : LoggerBase, ILogger
    {
        public void Log(Exception exception)
        {
            var message = CreateLogMessage(exception);
            Console.WriteLine(message);
        }
    }
}