using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _7_async_parallel
{
    class Program
    {
        private static readonly ManualResetEvent Signal = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            Console.WriteLine("Main, Thread id: " + Thread.CurrentThread.ManagedThreadId);

            //CallTasksSynchronously();
            //CallTasksAsync();
            //CallTasksWithReturnValuesAsync();
            //CallTasksWithReturnValuesUsingOldSchoolTpl();

            Console.WriteLine("Main, after CallFoos() returned");

            // Make sure the thread doesn't die until all tasks have finished
            Signal.WaitOne();
        }

        private async static void CallTasksSynchronously()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            DoSomething();
            DoSomething();

            stopwatch.Stop();
            Console.WriteLine("Ellapsed time (ms): " + stopwatch.ElapsedMilliseconds);

            Signal.Set();
        }

        private async static void CallTasksAsync()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var task1 = Task.Factory.StartNew(DoSomething);
            var task2 = Task.Factory.StartNew(DoSomething);

            await task1;
            await task2;

            stopwatch.Stop();
            Console.WriteLine("Ellapsed time (ms): " + stopwatch.ElapsedMilliseconds);

            Signal.Set();
        }
        
        private async static void CallTasksUsingTpl()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var task1 = Task.Factory.StartNew(DoSomething);
            var task2 = Task.Factory.StartNew(DoSomething);

            Task.WaitAll(task1, task2);

            stopwatch.Stop();
            Console.WriteLine("Ellapsed time (ms): " + stopwatch.ElapsedMilliseconds);

            Signal.Set();
        }

        private async static void CallTasksWithReturnValuesAsync()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int result1 = await Task.Factory.StartNew(() => CalculateSomething(1));
            int result2 = await Task.Factory.StartNew(() => CalculateSomething(result1));

            Console.WriteLine("Result: " + result2);

            stopwatch.Stop();
            Console.WriteLine("Ellapsed time (ms): " + stopwatch.ElapsedMilliseconds);

            Signal.Set();
        }

        private static void CallTasksWithReturnValuesUsingOldSchoolTpl()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var chainedTask = Task.Factory.StartNew(() => CalculateSomething(1))
                .ContinueWith(task => CalculateSomething(task.Result));

            Console.WriteLine("Result: " + chainedTask.Result);

            stopwatch.Stop();
            Console.WriteLine("Ellapsed time (ms): " + stopwatch.ElapsedMilliseconds);

            Signal.Set();
        }

        private static void DoSomething()
        {
            Console.WriteLine("DoSomething, Thread id: " + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1000);
        }
        
        private static int CalculateSomething(int i)
        {
            Thread.Sleep(1000);
            return i+1;
        }
    }
}
