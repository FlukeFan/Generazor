using System;
using System.Threading.Tasks;
using Generazor;

namespace HelloWorld
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var model = new HelloWorldModel { Message = "Hello from generated template!" };
            var gen = new Generator();
            var output = await gen.GenerateStringAsync("/HelloWorld.cshtml", model);

            Console.WriteLine($"GenerateString={output}");
        }
    }
}
