using System.Collections.Generic;

namespace Generazor
{
    public interface ISetupGeneration
    {
        IList<FileGenerationInfo> Setup(string[] args);
    }
}
