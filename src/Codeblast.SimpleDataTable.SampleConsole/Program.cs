using System;
using Codeblast.SimpleDataTable.SampleConsole.PerformanceTest;

namespace Codeblast.SimpleDataTable.SampleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PocoListSample samplePoco = new PocoListSample();
            samplePoco.Run();

            SimpleDataTableSample sampleSimple = new SimpleDataTableSample();
            sampleSimple.Run();
            
            DataTableSample sampleFull = new DataTableSample();
            sampleFull.Run();
        }
    }
}
