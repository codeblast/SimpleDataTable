using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Codeblast.SimpleDataTable
{
    public class SimpleDataTable : ISimpleDataTable
    {
        public IList<string> ColumnNames { get; private set; }
        public IList<IDataRow> Rows { get; private set; }

        public SimpleDataTable()
        {
            ColumnNames = new List<string>();
            Rows = new List<IDataRow>();
        }

        public IDataRow NewRow()
        {
            IDataRow row = new DataRow(ColumnNames);
            Rows.Add(row);
            return row;
        }

        [ContractInvariantMethod]
        private void ContractInvariantMethod()
        {
            Contract.Invariant(ColumnNames != null);
            Contract.Invariant(Rows != null);
        }
    }
}
