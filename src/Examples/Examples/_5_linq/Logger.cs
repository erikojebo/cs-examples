using Examples._3_oop._4_composition;

namespace Examples._5_linq
{
    public class ConsoleLogger : ComposedLogger
    {
        public ConsoleLogger() : base(new ConsoleOutputTarget())
        {
        }
    }
}