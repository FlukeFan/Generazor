// This file was generated using Generazor:  https://flukefan.github.io/Generazor/
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace ConsoleApp
{
    public static class Employees
    {
        public const string QueryAll = "SELECT EmployeeId, LastName, FirstName, Title, ReportsTo, BirthDate, HireDate, Address, City, State, Country, PostalCode, Phone, Fax, Email FROM employees";

        public static async Task<IEnumerable<Employee>> QueryAllEmployees(this IDbConnection cn)
        {
            return await cn.QueryAsync<Employee>(QueryAll);
        }
    }
}
