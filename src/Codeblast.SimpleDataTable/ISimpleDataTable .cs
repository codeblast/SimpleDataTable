using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Codeblast.SimpleDataTable
{
    [ContractClass(typeof(ISimpleDataTableContract))]
    public interface ISimpleDataTable
    {
        IList<string> ColumnNames { get; }
        IList<IDataRow> Rows { get; }
        IDataRow NewRow();
    }

    [ContractClassFor(typeof(ISimpleDataTable))]
    abstract class ISimpleDataTableContract : ISimpleDataTable
    {
        IList<string> ISimpleDataTable.ColumnNames
        {
            get 
            {
                Contract.Ensures(Contract.Result<IList<string>>() != null);
                return null;
            }
        }

        IList<IDataRow> ISimpleDataTable.Rows
        {
            get 
            {
                Contract.Ensures(Contract.Result<IList<IDataRow>>() != null);
                return null;
            }
        }

        IDataRow ISimpleDataTable.NewRow()
        {
            Contract.Ensures(Contract.Result<IDataRow>() != null);
            return null;
        }
    }
}
