using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Generazor.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
                throw new Exception($"First parameter should be assembly with class(es) implementing ISetupGeneration");

            var generatorAssemblyPath = Path.GetFullPath(args[0]);
            var generatorAssembly = Assembly.LoadFile(generatorAssemblyPath);

            var setupGenerators = generatorAssembly.ExportedTypes
                .Where(t => t.GetInterfaces().Contains(typeof(ISetupGeneration)))
                .ToList();

            if (setupGenerators.Count == 0)
                throw new Exception($"Could not find any types implementing ISetupGeneration in {generatorAssembly.FullName}");

            var generatorArgs = args.Skip(1).ToArray();
            var generator = new Generator();

            foreach (var setupGeneratorType in setupGenerators)
            {
                var setupGenerator = (ISetupGeneration)Activator.CreateInstance(setupGeneratorType);
                var info = setupGenerator.Setup(generatorArgs);
                await generator.GenerateFilesAsync(info);
            }
        }
    }
}
