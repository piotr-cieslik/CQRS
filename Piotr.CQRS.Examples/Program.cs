using System;

namespace Piotr.CQRS.Examples
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Run {nameof(MainExample)}");
            new MainExample().Run();

            Console.WriteLine($"Run {nameof(ScanAssemblyExample)}");
            new ScanAssemblyExample().Run();

            Console.WriteLine($"Run {nameof(PerformanceExample)}");
            new PerformanceExample().Run();

            Console.ReadKey();
        }
    }
}
