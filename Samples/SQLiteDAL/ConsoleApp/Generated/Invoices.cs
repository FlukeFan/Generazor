// This file was generated using Generazor:  https://flukefan.github.io/Generazor/
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace ConsoleApp
{
    public static class Invoices
    {
        public const string QueryAll = "SELECT InvoiceId, CustomerId, InvoiceDate, BillingAddress, BillingCity, BillingState, BillingCountry, BillingPostalCode, Total FROM invoices";

        public static async Task<IEnumerable<Invoice>> QueryAllInvoices(this IDbConnection cn)
        {
            return await cn.QueryAsync<Invoice>(QueryAll);
        }
    }
}
