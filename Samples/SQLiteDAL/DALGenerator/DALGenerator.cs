using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using Generazor;

namespace DALGenerator
{
    class DALGenerator
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

                var tables = cn.GetSchema("Tables");
                var columns = cn.GetSchema("Columns");
            }

            await new Generator().GenerateFilesAsync(filesToGenerate);
        }
    }
}
