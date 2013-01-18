using System;
using System.IO;

namespace _6_code_smells.Models
{
    public class Logger
    {
        public static void WriteLogMessage(Exception e)
        {
            var logFilePath = GetLogFilePath();

            using (var stream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write))
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine(String.Format("{0}: {1}", DateTime.Now, e.Message));
            }
        }

        private static string GetLogFilePath()
        {
            return Path.Combine(Path.GetTempPath(), "_6_code_smells.log");
        }
    }
}