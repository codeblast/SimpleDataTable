using System;

namespace Codeblast.SimpleDataTable.SampleConsole.PerformanceTest
{
    class SimpleDataTableSample : DataTableSampleBase
    {
        public SimpleDataTableSample() : base("Codeblast.SimpleDataTable")
        {
        }

        protected override void CreateTableAndAddSomeRows(int rowCount, int columnCount)
        {
            SimpleDataTable table = new SimpleDataTable();
            for (int col = 0; col < columnCount; col++)
            {
                table.ColumnNames.Add("Column " + col);
            }
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                IDataRow row = table.NewRow();
                for (int colIndex = 0; colIndex < columnCount; colIndex++)
                {
                    row[colIndex] = GetColValue(rowIndex, colIndex);
                }
            }
        }
    }
}
