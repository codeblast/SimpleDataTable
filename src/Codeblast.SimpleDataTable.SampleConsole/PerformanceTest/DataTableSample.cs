using System;
using System.Data;

namespace Codeblast.SimpleDataTable.SampleConsole.PerformanceTest
{
    class DataTableSample : DataTableSampleBase
    {
        public DataTableSample() : base("System.Data.DataTable")
        {
        }

        protected override void CreateTableAndAddSomeRows(int rowCount, int columnCount)
        {
            DataTable table = new DataTable();
            for (int col = 0; col < columnCount; col++)
            {
                table.Columns.Add("Column " + col);
            }
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                System.Data.DataRow row = table.NewRow();
                for (int colIndex = 0; colIndex < columnCount; colIndex++)
                {
                    row[colIndex] = GetColValue(rowIndex, colIndex);
                }
            }
        }
    }
}
