using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Examples._7_async_and_parallel
{
    public class _1_async
    {
        // Se till att använda .NET 4.5, annars blir det ett kryptiskt felmeddelande vid kompilering

        [Test]
        public async void Fact()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await Foo();
            await Foo();

            stopwatch.Stop();
            Console.WriteLine("Ellapsed time (ms): " + stopwatch.ElapsedMilliseconds);
        }

        private async void CallStuff()
        {
            
        }

        private Task Foo()
        {
            return Task.Factory.StartNew(() => Thread.Sleep(1000));
        }
    }
}