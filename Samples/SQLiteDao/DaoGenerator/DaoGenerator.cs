using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using Generazor;

namespace SQLiteDao.DaoGenerator
{
    class DaoGenerator
    {
        static async Task Main(string[] args)
        {
            var dbPath = args[0];
            var outputPath = args[1];
            var generatedNamespace = args[2];

            var filesToGenerate = new List<FileGenerationInfo>();

            using (var cn = new SQLiteConnection($"Data Source={dbPath}"))
            {
                cn.Open();

            }

            await new Generator().GenerateFilesAsync(filesToGenerate);
        }
    }
}
