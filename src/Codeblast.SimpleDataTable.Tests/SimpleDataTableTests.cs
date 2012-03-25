using System;
using Xunit;

namespace Codeblast.SimpleDataTable.Tests
{
    public class SimpleDataTableTests
    {
        public class Constructor
        {
            [Fact]
            public void SetsColumnNamesToEmplyListAndSetsRowsToEmpyList()
            {
                SimpleDataTable target = new SimpleDataTable();

                Assert.NotNull(target.ColumnNames);
                Assert.NotNull(target.Rows);
                Assert.Equal(0, target.ColumnNames.Count);
                Assert.Equal(0, target.Rows.Count);
            }
        }

        public class NewRow
        {
            [Fact]
            public void WhenNoColumnsAddedToTableYet_ThenReturnsRowWithNoColumns()
            {
                SimpleDataTable target = new SimpleDataTable();

                IDataRow result = target.NewRow();

                Assert.NotNull(result);
                Assert.Equal(0, target.ColumnNames.Count);
                Assert.Equal(0, result.ColumnNames.Count);
            }

            [Fact]
            public void WhenColumnsAddedToTable_ThenReturnsRowWithSameColumnsAsTable()
            {
                SimpleDataTable target = new SimpleDataTable();
                target.ColumnNames.Add("Col1");
                target.ColumnNames.Add("Col2");

                IDataRow result = target.NewRow();

                Assert.NotNull(result);
                Assert.Equal(2, target.ColumnNames.Count);
                Assert.Same(target.ColumnNames, result.ColumnNames);
            }
        }
    }
}
