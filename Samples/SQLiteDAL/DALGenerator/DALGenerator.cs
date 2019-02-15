using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
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

            var filesToGenerate = new List<FileGenerator>();

            using (var cn = new SQLiteConnection($"Data Source={dbPath}"))
            {
                cn.Open();

                var tables = cn.GetSchema("Tables");
                var columns = cn.GetSchema("Columns");

                foreach (DataRow tableRow in tables.Rows)
                {
                    var tableName = (string)tableRow["TABLE_NAME"];

                    if (tableName == "sqlite_sequence" || tableName == "sqlite_stat1")
                        continue;

                    var model = new TableModel
                    {
                        Namespace = generatedNamespace,
                        Name = tableName,
                    };

                    columns.DefaultView.RowFilter = $"TABLE_NAME = '{tableName}'";
                    foreach (DataRowView columnRow in columns.DefaultView)
                    {
                        var columnName = (string)columnRow["COLUMN_NAME"];
                        var columnType = (string)columnRow["DATA_TYPE"];

                        model.Columns.Add(new ColumnModel
                        {
                            Name = columnName,
                            Type = columnType,
                        });
                    }

                    filesToGenerate.Add(FileGenerator.LazyFile("/Entity.cshtml", model, Path.Combine(outputPath, $"{tableName.ToCsNameSingular()}.cs")));
                }
            }

            await new Generator().GenerateFilesAsync(filesToGenerate);
        }
    }
}
