using System;
using System.Linq;
using CQRS.Markers;

namespace CQRS.Examples
{
    public sealed class ScanAssemblyExample
    {
        public void Run()
        {
            var commands =
                typeof(ScanAssemblyExample).Assembly
                .GetTypes()
                .Where(x => x.IsClass)
                .Where(x => typeof(ICommand).IsAssignableFrom(x))
                .Count();
            var queries =
                typeof(ScanAssemblyExample).Assembly
                .GetTypes()
                .Where(x => x.IsClass)
                .Where(x => typeof(IQuery).IsAssignableFrom(x))
                .Count();

            Console.WriteLine($"Number of commands: {commands}");
            Console.WriteLine($"Number of queries: {queries}");
        }
    }
}
