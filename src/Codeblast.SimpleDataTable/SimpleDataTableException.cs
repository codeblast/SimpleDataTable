using System;
using System.Diagnostics.Contracts;

namespace Codeblast.SimpleDataTable
{
    public class SimpleDataTableException : Exception
    {
        public SimpleDataTableException(string message)
            : base(message)
        {
            Contract.Requires(message != null);
        }
    }
}
