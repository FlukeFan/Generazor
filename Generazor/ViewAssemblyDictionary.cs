using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Razor.Hosting;

namespace Generazor
{
    public class ViewAssemblyDictionary
    {
        private ConcurrentDictionary<Type, Assembly>                                        _assemblies     = new ConcurrentDictionary<Type, Assembly>();
        private ConcurrentDictionary<Assembly, IReadOnlyList<RazorCompiledItem>>            _assemblyItems  = new ConcurrentDictionary<Assembly, IReadOnlyList<RazorCompiledItem>>();
        private ConcurrentDictionary<IReadOnlyList<RazorCompiledItem>, RazorCompiledItem>   _items          = new ConcurrentDictionary<IReadOnlyList<RazorCompiledItem>, RazorCompiledItem>();

        public GenerazorPage<TModel> NewPage<TModel>(string view, TModel model, TextWriter textWriter)
        {
            var pageType = PageType<TModel>(view);
            var page = (GenerazorPage<TModel>)Activator.CreateInstance(pageType);
            return page;
        }

        public Assembly AssemblyFor<TModel>()
        {
            return _assemblies.GetOrAdd(typeof(TModel), t =>
            {
                var assemblyPath = t.Assembly.Location;
                var viewAssemblyPath = assemblyPath.Replace(".dll", ".Views.dll");
                var viewAssembly = Assembly.LoadFrom(viewAssemblyPath);
                return viewAssembly;
            });
        }

        public Type PageType<TModel>(string view)
        {
            var viewAssembly = AssemblyFor<TModel>();

            var items = _assemblyItems.GetOrAdd(viewAssembly, a =>
            {
                var itemLoader = new RazorCompiledItemLoader();
                var assemblyItems = itemLoader.LoadItems(viewAssembly);
                return assemblyItems;
            });

            var item = items.Where(i => i.Identifier == view).SingleOrDefault();

            if (item == null)
                throw new Exception($"Could not find view '{view}' in assembly {viewAssembly.Location}.  Valid views:\n{string.Join("\n", items.Select(i => i.Identifier))}");

            return item.Type;
        }
    }
}
