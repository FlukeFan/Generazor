using System;
using System.Threading.Tasks;

namespace DapperDao
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static Task MainAsync(string[] args)
        {
            Console.WriteLine($"Hello from DapperDao");
            return Task.CompletedTask;
        }
    }
}
