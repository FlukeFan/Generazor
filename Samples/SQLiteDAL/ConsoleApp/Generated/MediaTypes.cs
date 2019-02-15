// This file was generated using Generazor:  https://flukefan.github.io/Generazor/
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace ConsoleApp
{
    public static class MediaTypes
    {
        public const string QueryAll = "SELECT MediaTypeId, Name FROM media_types";

        public static async Task<IEnumerable<MediaType>> QueryAllMediaTypes(this IDbConnection cn)
        {
            return await cn.QueryAsync<MediaType>(QueryAll);
        }
    }
}
