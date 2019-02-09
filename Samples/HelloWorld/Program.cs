using System;
using Generazor;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = new HelloWorldModel { Message = "Hello from generated template!" };
            var gen = new Generator();

            Console.WriteLine($"GenerateString={gen.GenerateString("HelloWorld", model)}");
        }
    }
}
