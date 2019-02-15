// This file was generated using Generazor:  https://flukefan.github.io/Generazor/
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace ConsoleApp
{
    public static class PlaylistTracks
    {
        public const string QueryAll = "SELECT PlaylistId, TrackId FROM playlist_track";

        public static async Task<IEnumerable<PlaylistTrack>> QueryAllPlaylistTrack(this IDbConnection cn)
        {
            return await cn.QueryAsync<PlaylistTrack>(QueryAll);
        }
    }
}
