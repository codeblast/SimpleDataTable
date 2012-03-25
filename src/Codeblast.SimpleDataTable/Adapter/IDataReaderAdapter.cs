using System;
using System.Data;
using System.Diagnostics.Contracts;
using Codeblast.SimpleDataTable;

namespace Codeblast.SimpleDataTable.Adapter
{
    [ContractClass(typeof(IDataReaderAdapterContract))]
    public interface IDataReaderAdapter
    {
        void Fill(ISimpleDataTable dataTable, IDataReader dataReader);
    }

    [ContractClassFor(typeof(IDataReaderAdapter))]
    abstract class IDataReaderAdapterContract : IDataReaderAdapter
    {
        void IDataReaderAdapter.Fill(ISimpleDataTable dataTable, IDataReader dataReader)
        {
            Contract.Requires(dataTable != null);
            Contract.Requires(dataReader != null);
        }
    }
}
