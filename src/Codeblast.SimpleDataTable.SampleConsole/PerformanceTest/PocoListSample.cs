using System;
using System.Collections.Generic;

namespace Codeblast.SimpleDataTable.SampleConsole.PerformanceTest
{
    class MyPoco
    {
        public string c1 { get; set; }
        public string c2 { get; set; }
        public string c3 { get; set; }
        public string c4 { get; set; }
        public string c5 { get; set; }
        public string c6 { get; set; }
        public string c7 { get; set; }
        public string c8 { get; set; }
        public string c9 { get; set; }
        public string c10 { get; set; }
        public string c11 { get; set; }
        public string c12 { get; set; }
        public string c13 { get; set; }
        public string c14 { get; set; }
        public string c15 { get; set; }
        public string c16 { get; set; }
        public string c17 { get; set; }
        public string c18 { get; set; }
        public string c19 { get; set; }
        public string c20 { get; set; }
    }

    class PocoListSample : DataTableSampleBase
    {
        public PocoListSample() : base("List<MyPoco>")
        {
        }

        protected override void CreateTableAndAddSomeRows(int rowCount, int columnCount)
        {
            List<MyPoco> list = new List<MyPoco>();
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                MyPoco row = new MyPoco
                {
                    c1 = GetColValue(rowIndex, 1),
                    c2 = GetColValue(rowIndex, 2),
                    c3 = GetColValue(rowIndex, 3),
                    c4 = GetColValue(rowIndex, 4),
                    c5 = GetColValue(rowIndex, 5),
                    c6 = GetColValue(rowIndex, 6),
                    c7 = GetColValue(rowIndex, 7),
                    c8 = GetColValue(rowIndex, 8),
                    c9 = GetColValue(rowIndex, 9),
                    c10 = GetColValue(rowIndex, 10),
                    c11 = GetColValue(rowIndex, 11),
                    c12 = GetColValue(rowIndex, 12),
                    c13 = GetColValue(rowIndex, 13),
                    c14 = GetColValue(rowIndex, 14),
                    c15 = GetColValue(rowIndex, 15),
                    c16 = GetColValue(rowIndex, 16),
                    c17 = GetColValue(rowIndex, 17),
                    c18 = GetColValue(rowIndex, 18),
                    c19 = GetColValue(rowIndex, 19),
                    c20 = GetColValue(rowIndex, 20)
                };
                list.Add(row);
            }
        }
    }
}
