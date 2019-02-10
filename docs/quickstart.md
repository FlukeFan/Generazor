<h1>Quickstart</h1>

Execute the following:

    mkdir HelloWorld
    cd HelloWorld
    dotnet new console
    dotnet add package Generazor
    dotnet add package Microsoft.AspNetCore.Mvc

Open `HelloWorld.csproj`, and change:

    <Project Sdk="Microsoft.NET.Sdk">

to

    <Project Sdk="Microsoft.NET.Sdk.Razor">

First add a very simple model class `HelloWorldModel.cs`:

    namespace HelloWorld
    {
        public class HelloWorldModel
        {
            public string Message;
        }
    }

And add a very simple view `HelloWorld.cshtml`:

    @namespace HelloWorld
    @inherits Generazor.GenerazorPage<HelloWorldModel>
    @model HelloWorldModel
    @Model.Message

Because the package Microsoft.AspNetCore.Mvc was added, you get intellisense on the .cshtml files, and you get an assembly `HelloWorld.View.dll` generated.

Now update `Program.cs`:

    using System;
    using System.Threading.Tasks;
    using Generazor;

    namespace HelloWorld
    {
        class Program
        {
            static void Main(string[] args)
            {
                MainAsync(args).GetAwaiter().GetResult();
            }

            static async Task MainAsync(string[] args)
            {
                var model = new HelloWorldModel { Message = "Hello from generated template!" };

                var gen = new Generator();
                var output = await gen.GenerateStringAsync("/HelloWorld.cshtml", model);

                Console.WriteLine($"{output}");
            }
        }
    }

Now you can do:

    dotnet run

... and you should see the output:

    Hello from generated template!

Your code should look something similar to this:  <a href="https://github.com/FlukeFan/Generazor/tree/master/Samples/HelloWorld">HelloWorld Sample</a>
