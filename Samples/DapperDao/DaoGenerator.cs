using System.Collections.Generic;
using Generazor;

namespace DapperDao
{
    public class DaoGenerator : ISetupGeneration
    {
        public IList<FileGenerationInfo> Setup(string[] args)
        {
            return new List<FileGenerationInfo>();
        }
    }
}
