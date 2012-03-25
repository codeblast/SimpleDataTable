using System;
using System.Diagnostics;

namespace Codeblast.SimpleDataTable.SampleConsole.PerformanceTest
{
    abstract class DataTableSampleBase
    {
        private string _title;

        public DataTableSampleBase(string title)
        {
            _title = title;
        }

        public void Run()
        {
            Console.WriteLine("Running test for {0}", _title);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int n = 0; n < 10000; n++)
            {  
                CreateTableAndAddSomeRows(100, 20);
            }

            stopwatch.Stop();
            Console.WriteLine("Total elapsed time in ms: {0}", stopwatch.ElapsedMilliseconds);
            Console.WriteLine("".PadRight(70, '-'));
            Console.WriteLine();
        }

        protected abstract void CreateTableAndAddSomeRows(int rowCount, int columnCount);

        protected string GetColValue(int rowIndex, int columnIndex)
        {
            return string.Format("Row {0} and Column {1}", rowIndex, columnIndex);
        }
    }
}
