using System.Threading.Tasks;

namespace Generazor
{
    public abstract class FileGenerator
    {
        public abstract Task GenerateAsync(Generator generator);
    }
}
