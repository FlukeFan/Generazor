// This file was generated using Generazor:  https://flukefan.github.io/Generazor/
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace ConsoleApp
{
    public static class Tracks
    {
        public const string QueryAll = "SELECT TrackId, Name, AlbumId, MediaTypeId, GenreId, Composer, Milliseconds, Bytes, UnitPrice FROM tracks";

        public static async Task<IEnumerable<Track>> QueryAllTracks(this IDbConnection cn)
        {
            return await cn.QueryAsync<Track>(QueryAll);
        }
    }
}
