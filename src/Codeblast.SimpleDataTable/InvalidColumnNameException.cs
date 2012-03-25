using System;
using System.Diagnostics.Contracts;

namespace Codeblast.SimpleDataTable
{
    public class InvalidColumnNameException : SimpleDataTableException
    {
        public InvalidColumnNameException(string columnName)
            : base("Invalid column name: " + columnName)
        {
            Contract.Requires(columnName != null);
        }
    }
}
