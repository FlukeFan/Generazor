using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Hosting;

namespace Generazor
{
    public class Generator
    {
        public async Task<string> GenerateStringAsync<TModel>(TModel model)
        {
            var view = ViewFor(typeof(TModel));
            return await GenerateStringAsync(view, model);
        }

        public async Task<string> GenerateStringAsync<TModel>(string view, TModel model)
        {
            using (var stringWriter = new StringWriter())
            {
                await Generate(view, model, stringWriter);
                return stringWriter.ToString();
            }
        }

        public async Task GenerateFileAsync<TModel>(TModel model, string file)
        {
            var view = ViewFor(typeof(TModel));
            await GenerateFileAsync(view, model, file);
        }

        public async Task GenerateFileAsync<TModel>(string view, TModel model, string file)
        {
            using (var fileWriter = File.CreateText(file))
                await Generate(view, model, fileWriter);
        }

        public async Task Generate<TModel>(string view, TModel model, TextWriter textWriter)
        {
            var assemblyPath = typeof(TModel).Assembly.Location;
            var viewAssemblyPath = assemblyPath.Replace(".dll", ".Views.dll");
            var viewAssembly = Assembly.LoadFrom(viewAssemblyPath);

            var itemLoader = new RazorCompiledItemLoader();
            var items = itemLoader.LoadItems(viewAssembly);

            var item = items.Where(i => i.Identifier == view).SingleOrDefault();

            if (item == null)
                throw new Exception($"Could not find view '{view}' in assembly {viewAssembly.Location}.  Valid views:\n{string.Join("\n", items.Select(i => i.Identifier))}");

            var page = (GenerazorPage<TModel>)Activator.CreateInstance(item.Type);
            (page as IActivatePage<TModel>).Activate(textWriter, model);
            await page.ExecuteAsync();
        }

        public async Task GenerateFilesAsync<TModel>(IEnumerable<FileGenerator> fileGenerators)
        {
            foreach (var fileGenerator in fileGenerators)
                await fileGenerator.GenerateAsync(this);
        }

        public async Task GenerateFilesAsync(IEnumerable<FileGenerator> fileGenerators)
        {
            foreach (var fileGenerator in fileGenerators)
                await fileGenerator.GenerateAsync(this);
        }

        public string ViewFor(Type modelType)
        {
            var modelName = modelType.FullName;

            var assemblyName = modelType.Assembly.GetName().Name;

            if (modelName.StartsWith(assemblyName))
                modelName = modelName.Substring(assemblyName.Length + 1);

            if (modelName.EndsWith("Model"))
                modelName = modelName.Substring(0, modelName.Length - "Model".Length);

            var view = $"/{modelName.Replace(".", "/")}.cshtml";

            return view;
        }
    }
}
