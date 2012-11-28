using System;
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
        public void Stuff_goes_here()
        {
            var exception = new InvalidOperationException("You can't do that...");

            var logger = new LogMessageConsoleLogger();
            logger.Log(exception);
        }
    }
}