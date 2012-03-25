using System;

namespace Codeblast.SimpleDataTable
{
    public class InvalidColumnIndexException : SimpleDataTableException
    {
        public InvalidColumnIndexException(int columnIndex, int columnCount)
            : base(string.Format("Invalid column index {0}, row has {1} columns", columnIndex, columnCount))
        {
        }
    }
}
