using System.IO;
using System.Threading.Tasks;

namespace Generazor
{
    public class LazyFile<TModel> : FileGenerator
    {
        private string _view;
        private TModel _model;
        private string _file;

        public LazyFile(string view, TModel model, string file)
        {
            _view = view;
            _model = model;
            _file = file;
        }

        public override async Task GenerateAsync(Generator generator)
        {
            if (!File.Exists(_file))
            {
                await generator.GenerateFileAsync(_view, _model, _file);
                return;
            }

            var existingContent = File.ReadAllText(_file);
            var newContent = await generator.GenerateStringAsync(_view, _model);

            if (newContent != existingContent)
                File.WriteAllText(_file, newContent);
        }
    }
}
