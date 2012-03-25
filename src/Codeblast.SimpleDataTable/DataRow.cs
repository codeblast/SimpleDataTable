using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Codeblast.SimpleDataTable
{
    public class DataRow : IDataRow
    {
        public IList<object> Data { get; private set; }
        public IList<string> ColumnNames { get; private set; }

        public DataRow(IList<string> columns)
        {
            Contract.Requires(columns != null);
            ColumnNames = columns;
            Data = new List<object>();
        }

        public bool IsNull(int columnIndex)
        {
            ValidateColumnIndex(columnIndex);
            return columnIndex < Data.Count ? Data[columnIndex] == null : true;
        }

        public bool IsNull(string columnName)
        {
            return IsNull(GetColumnIndex(columnName));
        }

        public int GetColumnIndex(string columnName)
        {
            Contract.Requires(columnName != null);
            Contract.Ensures(Contract.Result<int>() >= 0);
            int columnIndex = -1;
            for (int n = 0; n < ColumnNames.Count; n++)
            {
                if (string.Compare(ColumnNames[n], columnName, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    columnIndex = n;
                    break;
                }
            }
            if (columnIndex < 0)
            {
                throw new InvalidColumnNameException(columnName);
            }
            return columnIndex;
        }

        public object this[int columnIndex]
        {
            get
            {
                return GetValue(columnIndex);
            }
            set
            {
                SetValue(columnIndex, value);
            }
        }

        public object this[string columnName]
        {
            get
            {
                return GetValue(columnName);
            }
            set
            {
                SetValue(columnName, value);
            }
        }

        private object GetValue(int columnIndex)
        {
            Contract.Requires(columnIndex >= 0);
            ValidateColumnIndex(columnIndex);
            if (columnIndex < Data.Count)
            {
                return Data[columnIndex];
            }
            else
            {
                return null;
            }
        }

        private void SetValue(int columnIndex, object value)
        {
            Contract.Requires(columnIndex >= 0);
            ValidateColumnIndex(columnIndex);
            while (Data.Count <= columnIndex)
            {
                Data.Add(null);
            }
            Data[columnIndex] = value;
        }

        private object GetValue(string columnName)
        {
            Contract.Requires(columnName != null);
            return GetValue(GetColumnIndex(columnName));
        }

        private void SetValue(string columnName, object value)
        {
            Contract.Requires(columnName != null);
            SetValue(GetColumnIndex(columnName), value);
        }

        private void ValidateColumnIndex(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= ColumnNames.Count)
            {
                throw new InvalidColumnIndexException(columnIndex, ColumnNames.Count);
            }
        }

        [ContractInvariantMethod]
        private void ContractInvariantMethod()
        {
            Contract.Invariant(ColumnNames != null);
            Contract.Invariant(Data != null);
        }
    }
}
