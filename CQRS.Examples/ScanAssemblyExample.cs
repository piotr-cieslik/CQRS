using System;
using System.Linq;

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
                .Where(x => typeof(ICommand).IsAssignableFrom(x));
            var queries =
                typeof(ScanAssemblyExample).Assembly
                .GetTypes()
                .Where(x => x.IsClass)
                .Where(x => typeof(IQuery).IsAssignableFrom(x));

            Console.WriteLine("Commands found in the assembly:");
            foreach (var command in commands)
            {
                Console.WriteLine($"- {command.Name}");
            }

            Console.WriteLine("Queries found in the assembly:");
            foreach (var query in queries)
            {
                Console.WriteLine($"- {query.Name}");
            }
        }
    }
}
