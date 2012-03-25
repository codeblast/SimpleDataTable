using System;
using System.Data;
using System.Diagnostics.Contracts;

namespace Codeblast.SimpleDataTable.Adapter
{
    public class DataReaderAdapter : IDataReaderAdapter
    {
        public void Fill(ISimpleDataTable dataTable, IDataReader dataReader)
        {
            while (dataReader.Read())
            {
                UpdateColumns(dataTable, dataReader);
                IDataRow row = dataTable.NewRow();
                for (int n = 0; n < dataReader.FieldCount; n++)
                {
                    if (!dataReader.IsDBNull(n))
                    {
                        row[n] = dataReader.GetValue(n);
                    }
                }
            }
        }

        private void UpdateColumns(ISimpleDataTable dataTable, IDataReader dataReader)
        {
            Contract.Requires(dataTable != null);
            Contract.Requires(dataReader != null);
            if (dataTable.ColumnNames.Count < dataReader.FieldCount)
            {
                for (int columnIndex = dataTable.ColumnNames.Count; columnIndex < dataReader.FieldCount; columnIndex++)
                {
                    string columnName = dataReader.GetName(columnIndex);
                    if (string.IsNullOrEmpty(columnName))
                    {
                        columnName = columnIndex.ToString();
                    }
                    dataTable.ColumnNames.Add(columnName);
                }
            }
        }
    }
}
