using System.Collections.Generic;

namespace DALGenerator
{
    public class TableModel
    {
        public string               Namespace;
        public string               Name;
        public IList<ColumnModel>   Columns     = new List<ColumnModel>();
    }
}
