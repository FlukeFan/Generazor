// This file was generated using Generazor:  https://flukefan.github.io/Generazor/
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace ConsoleApp
{
    public static class InvoiceItems
    {
        public const string QueryAll = "SELECT InvoiceLineId, InvoiceId, TrackId, UnitPrice, Quantity FROM invoice_items";

        public static async Task<IEnumerable<InvoiceItem>> QueryAllInvoiceItems(this IDbConnection cn)
        {
            return await cn.QueryAsync<InvoiceItem>(QueryAll);
        }
    }
}
