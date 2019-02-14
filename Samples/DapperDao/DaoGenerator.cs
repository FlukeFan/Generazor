using System.Collections.Generic;

namespace DapperDao
{
    public interface ISetupGeneration
    {
        IList<GenerateInfo> Setup(string[] args);
    }

    public class GenerateInfo
    {
    }

    public class DaoGenerator : ISetupGeneration
    {
        public IList<GenerateInfo> Setup(string[] args)
        {
            throw new System.Exception("Got to here");
        }
    }
}
