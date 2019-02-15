using System.Collections.Generic;
using System.Linq;

namespace DALGenerator
{
    public class TableModel
    {
        public string               Namespace;
        public string               Name;
        public IList<ColumnModel>   Columns     = new List<ColumnModel>();

        public IEnumerable<string> ColumnNames()
        {
            return Columns.Select(c => c.Name);
        }
    }
}
