using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SQLiteDao.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            var currentDirectory = Environment.CurrentDirectory;

            if (File.Exists("Generazor.sln"))
                currentDirectory = Path.Combine(currentDirectory, "Samples/SQLiteDao/ConsoleApp");

            var dbPath = Path.Combine(currentDirectory, "../chinook.db");

            using (var cn = new SQLiteConnection($"Data Source={dbPath}"))
            {
                cn.Open();

                var allArtists = await cn.QueryAllArtists();
                var orderedArtists = allArtists.OrderBy(a => a.Name).ToList();

                var someNames = new List<string>();

                for (var i = 0; i < 10; i++)
                    someNames.Add(allArtists.Skip(i * 10).First().Name);

                Console.WriteLine($"10 artist names:\n{string.Join("\n", someNames)}");
            }
        }
    }
}
