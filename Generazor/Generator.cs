using System.Reflection;
using Microsoft.AspNetCore.Razor.Hosting;

namespace Generazor
{
    public class Generator
    {
        public string GenerateString<TModel>(string view, TModel model)
        {

            var assemblyPath = typeof(TModel).Assembly.Location;
            var viewAssemblyPath = assemblyPath.Replace(".dll", ".Views.dll");
            var viewAssembly = Assembly.LoadFile(viewAssemblyPath);

            var rcil = new RazorCompiledItemLoader();

            return "empty";
        }
    }
}
