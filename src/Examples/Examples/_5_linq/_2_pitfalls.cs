using System;
using System.Linq;
using System.Threading;
using NUnit.Framework;

namespace Examples._5_linq
{
    public class _2_pitfalls
    {
        [Test]
        public void Without_the_proper_System_Linq_using_statement_you_will_not_see_the_linq_methods_in_intellisense()
        {
            // ..
        }

        [Test]
        public void Enumerables_might_be_evaluated_multiple_times()
        {
            var numbers = Enumerable.Range(1, 5);

            var strings = numbers.Select(x => string.Format("{0}. {1}", x, DateTime.Now));

            foreach (var s in strings)
            {
                Console.WriteLine(s);
            }
            
            // Thread.Sleep(1000);

            foreach (var s in strings)
            {
                Console.WriteLine(s);
            }
        } 
    }
}