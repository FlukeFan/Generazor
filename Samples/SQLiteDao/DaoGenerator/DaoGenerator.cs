using System.Collections.Generic;
using Generazor;

namespace SQLiteDao.DaoGenerator
{
    public class DaoGenerator : ISetupGeneration
    {
        public IList<FileGenerationInfo> Setup(string[] args)
        {
            var db = args[0];

            if (db.Length > 0)
                throw new System.Exception($"Generating from DB: {db}");

            return new List<FileGenerationInfo>();
        }
    }
}
