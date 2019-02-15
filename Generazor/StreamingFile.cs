using System.Threading.Tasks;

namespace Generazor
{
    public class StreamingFile<TModel> : FileGenerator
    {
        private string _view;
        private TModel _model;
        private string _file;

        public StreamingFile(string view, TModel model, string file)
        {
            _view = view;
            _model = model;
            _file = file;
        }

        public override async Task GenerateAsync(Generator generator)
        {
            await generator.GenerateFileAsync(_view, _model, _file);
        }
    }
}
