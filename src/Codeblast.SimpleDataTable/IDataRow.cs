using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Codeblast.SimpleDataTable
{
    [ContractClass(typeof(IDataRowContract))]
    public interface IDataRow
    {
        IList<object> Data { get; }
        IList<string> ColumnNames { get; }
        bool IsNull(int columnIndex);
        bool IsNull(string columnName);
        object this[int columnIndex] { get; set; }
        object this[string columnName] { get; set; }
    }

    [ContractClassFor(typeof(IDataRow))]
    abstract class IDataRowContract : IDataRow
    {
        IList<object> IDataRow.Data
        {
            get 
            {
                Contract.Ensures(Contract.Result<IList<object>>() != null);
                return null;
            }
        }

        IList<string> IDataRow.ColumnNames
        {
            get 
            {
                Contract.Ensures(Contract.Result<IList<string>>() != null);
                return null;
            }
        }

        bool IDataRow.IsNull(int columnIndex)
        {
            Contract.Requires(columnIndex >= 0);
            return false;
        }

        bool IDataRow.IsNull(string columnName)
        {
            Contract.Requires(columnName != null);
            return false;
        }

        object IDataRow.this[int columnIndex]
        {
            get
            {
                Contract.Requires(columnIndex >= 0);
                return null;
            }
            set
            {
                Contract.Requires(columnIndex >= 0);
            }
        }

        object IDataRow.this[string columnName]
        {
            get
            {
                Contract.Requires(columnName != null);
                return null;
            }
            set
            {
                Contract.Requires(columnName != null);
            }
        }
    }
}
