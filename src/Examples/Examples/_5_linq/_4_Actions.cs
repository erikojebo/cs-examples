using System;
using System.IO;
using NUnit.Framework;

namespace Examples._5_linq
{
    [TestFixture]
    public class _4_Actions
    {
        private ConsoleLogger _logger;

        [SetUp]
        public void SetUp()
        {
            _logger = new ConsoleLogger();            
        }

        [Test]
        public void Some_kinds_of_duplication_are_hard_to_remove()
        {
            ParseInvalidInt();
            ReadInvalidFile();
        }

        private void ReadInvalidFile()
        {
            try
            {
                var fileContents = File.ReadAllText("this is a path that will throw an exception");
                Console.WriteLine(fileContents);
            }
            catch (Exception exception)
            {
                _logger.Log(exception);
            }

            // Could be written as:

            //WithErrorLogging(() =>
            //{
            //    var fileContents = File.ReadAllText("this is a path that will throw an exception");
            //    Console.WriteLine(fileContents);
            //});
        }

        private void ParseInvalidInt()
        {
            try
            {
                var value = int.Parse("this will throw an exception when parsed");
                Console.WriteLine(value);
            }
            catch (Exception exception)
            {
                _logger.Log(exception);
            }
        }

        private void WithErrorLogging(Action action)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                _logger.Log(exception);
            }
        }
    }
}