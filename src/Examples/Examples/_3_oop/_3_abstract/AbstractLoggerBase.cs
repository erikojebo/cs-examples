using System;
using System.Diagnostics;

namespace Examples._3_oop._3_abstract
{
    public abstract class AbstractLoggerBase : ILogger
    {
        public void Log(Exception exception)
        {
            var message = string.Format("{0} ERROR: {1}, Stacktrace: {2}", DateTime.Now, exception.Message, exception.StackTrace);

            WriteMessage(message);
        }

        protected abstract void WriteMessage(string message);
    }

    public class OverrideConsoleLogger : AbstractLoggerBase
    {
        protected override void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
    
    public class OverrideTraceLogger : AbstractLoggerBase
    {
        protected override void WriteMessage(string message)
        {
            Trace.WriteLine(message);
        }
    }
}