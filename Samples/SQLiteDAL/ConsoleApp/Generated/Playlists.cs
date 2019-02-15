// This file was generated using Generazor:  https://flukefan.github.io/Generazor/
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace ConsoleApp
{
    public static class Playlists
    {
        public const string QueryAll = "SELECT PlaylistId, Name FROM playlists";

        public static async Task<IEnumerable<Playlist>> QueryAllPlaylists(this IDbConnection cn)
        {
            return await cn.QueryAsync<Playlist>(QueryAll);
        }
    }
}
