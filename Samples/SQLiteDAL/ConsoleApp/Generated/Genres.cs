// This file was generated using Generazor:  https://flukefan.github.io/Generazor/
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace ConsoleApp
{
    public static class Genres
    {
        public const string QueryAll = "SELECT GenreId, Name FROM genres";

        public static async Task<IEnumerable<Genre>> QueryAllGenres(this IDbConnection cn)
        {
            return await cn.QueryAsync<Genre>(QueryAll);
        }
    }
}
