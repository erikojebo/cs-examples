using System;

namespace Examples._3_oop._1_duplicated
{
    public class DuplicatedConsoleLogger : ILogger
    {
        public void Log(Exception exception)
        {
            var message = string.Format("{0} ERROR: {1}, Stacktrace: {2}", DateTime.Now, exception.Message, exception.StackTrace);

            Console.WriteLine(message);
        }
    }
}