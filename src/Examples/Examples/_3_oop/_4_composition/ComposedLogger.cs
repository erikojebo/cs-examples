using System;
using System.Diagnostics;

namespace Examples._3_oop._4_composition
{
    public class ComposedLogger : ILogger
    {
        private readonly IOutputTarget _outputTarget;

        public ComposedLogger(IOutputTarget outputTarget)
        {
            _outputTarget = outputTarget;
        }

        public void Log(Exception exception)
        {
            var message = string.Format("{0} ERROR: {1}, Stacktrace: {2}", DateTime.Now, exception.Message, exception.StackTrace);
            _outputTarget.WriteLine(message);
        }
    }

    public interface IOutputTarget
    {
        void WriteLine(string message);
    }

    public class ConsoleOutputTarget : IOutputTarget
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
    
    public class TraceOutputTarget : IOutputTarget
    {
        public void WriteLine(string message)
        {
            Trace.WriteLine(message);
        }
    }
}