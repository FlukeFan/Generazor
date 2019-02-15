using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Generazor
{
    public class Generator
    {
        private ViewAssemblyDictionary _viewAssemblies;

        public Generator() : this(new ViewAssemblyDictionary()) { }

        public Generator(ViewAssemblyDictionary viewAssemblies)
        {
            _viewAssemblies = viewAssemblies;
        }

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
            var page = _viewAssemblies.NewPage(view, model, textWriter);
            (page as IActivatePage<TModel>).Activate(textWriter, model);
            await page.ExecuteAsync();
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
