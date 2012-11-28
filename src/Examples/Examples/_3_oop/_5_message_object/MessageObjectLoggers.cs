using System;
using System.Diagnostics;

namespace Examples._3_oop._5_message_object
{
    public class LogMessage
    {
        private readonly Exception _exception;

        public LogMessage(Exception exception)
        {
            _exception = exception;
        }

        public override string ToString()
        {
            return string.Format("{0} ERROR: {1}, Stacktrace: {2}", DateTime.Now, _exception.Message, _exception.StackTrace);
        }
    }

    public class LogMessageConsoleLogger : ILogger
    {
        public void Log(Exception exception)
        {
            Console.WriteLine(new LogMessage(exception));
        }
    }
    
    public class LogMessageTraceLogger : ILogger
    {
        public void Log(Exception exception)
        {
            Trace.WriteLine(new LogMessage(exception));
        }
    }
}