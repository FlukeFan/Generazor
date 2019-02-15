// This file was generated using Generazor:  https://flukefan.github.io/Generazor/
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace ConsoleApp
{
    public static class Customers
    {
        public const string QueryAll = "SELECT CustomerId, FirstName, LastName, Company, Address, City, State, Country, PostalCode, Phone, Fax, Email, SupportRepId FROM customers";

        public static async Task<IEnumerable<Customer>> QueryAllCustomers(this IDbConnection cn)
        {
            return await cn.QueryAsync<Customer>(QueryAll);
        }
    }
}
