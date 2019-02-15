// This file was generated using Generazor:  https://flukefan.github.io/Generazor/
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace ConsoleApp
{
    public static class Albums
    {
        public const string QueryAll = "SELECT AlbumId, Title, ArtistId FROM albums";

        public static async Task<IEnumerable<Album>> QueryAllAlbums(this IDbConnection cn)
        {
            return await cn.QueryAsync<Album>(QueryAll);
        }
    }
}
