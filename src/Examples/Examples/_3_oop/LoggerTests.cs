using System;
using System.IO;
using Examples._3_oop._3_abstract;
using Examples._3_oop._4_composition;
using Examples._3_oop._5_message_object;
using NUnit.Framework;

namespace Examples._3_oop
{
    [TestFixture]
    public class LoggerTests
    {
        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Exception_is_thrown_when_reading_non_existing_file()
        {
            var logger = new LogMessageConsoleLogger();

            ReadFromFile(logger);
        }

        private void ReadFromFile(ILogger logger)
        {
            try
            {
                // Throws exception
                var fileContents = File.ReadAllText("invalid path");
            }
            catch (Exception e)
            {
                logger.Log(e);
                throw;
            }
        }
    }
}