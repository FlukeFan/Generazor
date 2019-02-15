using System.Threading.Tasks;

namespace Generazor
{
    public abstract class FileGenerator
    {
        public abstract Task GenerateAsync(Generator generator);

        public static StreamingFile<TModel> StreamingFile<TModel>(TModel model, string file)
        {
            var view = Generator.ViewFor(typeof(TModel));
            return new StreamingFile<TModel>(view, model, file);
        }

        public static StreamingFile<TModel> StreamingFile<TModel>(string view, TModel model, string file)
        {
            return new StreamingFile<TModel>(view, model, file);
        }
    }
}
