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
            var assemblyPath = typeof(TModel).Assembly.Location;
            var viewAssemblyPath = assemblyPath.Replace(".dll", ".Views.dll");
            var viewAssembly = Assembly.LoadFrom(viewAssemblyPath);

            var itemLoader = new RazorCompiledItemLoader();
            var items = itemLoader.LoadItems(viewAssembly);

            var item = items.Where(i => i.Identifier == view).SingleOrDefault();

            if (item == null)
                throw new Exception($"Could not find view '{view}' in assembly {viewAssembly.Location}.  Valid views:\n{string.Join("\n", items.Select(i => i.Identifier))}");

            var page = (GenerazorPage<TModel>)Activator.CreateInstance(item.Type);

            using (var stringWriter = new StringWriter())
            {
                (page as IActivatePage<TModel>).Activate(stringWriter, model);

                await page.ExecuteAsync();

                return stringWriter.ToString();
            }
        }

        public async Task GenerateFilesAsync(IList<FileGenerationInfo> fileGenerationInfos)
        {
            await Task.Delay(0);
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
