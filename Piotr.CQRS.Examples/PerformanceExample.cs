using System;
using System.Diagnostics;

namespace Piotr.CQRS.Examples
{
    public sealed partial class PerformanceExample
    {
        public void Run()
        {
            var initialization = Stopwatch.StartNew();
            var handlersLookup = new HandlersLookup();
            var disaptcher = new Dispatcher(handlersLookup);
            initialization.Stop();

            var dispatch = Stopwatch.StartNew();
            var result = disaptcher.Dispatch(new Command1000());
            dispatch.Stop();

            Console.WriteLine($"Initialization time: {initialization.ElapsedMilliseconds}ms.");
            Console.WriteLine($"Dispatch time: {dispatch.ElapsedMilliseconds}ms.");
        }
    }
}
