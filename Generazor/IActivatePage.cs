using System.IO;

namespace Generazor
{
    public interface IActivatePage<TModel>
    {
        void Activate(TextWriter textWriter, TModel model);
    }
}
