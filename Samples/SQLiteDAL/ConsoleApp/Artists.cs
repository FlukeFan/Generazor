using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace ConsoleApp
{
    public static class Artists
    {
        public const string QueryAll = "SELECT ArtistId, Name FROM artists";

        public static async Task<IEnumerable<Artist>> QueryAllArtists(this IDbConnection cn)
        {
            return await cn.QueryAsync<Artist>(QueryAll);
        }
    }
}
