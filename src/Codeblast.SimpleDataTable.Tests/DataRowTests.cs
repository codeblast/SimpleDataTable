using System;
using System.Collections.Generic;
using Xunit;

namespace Codeblast.SimpleDataTable.Tests
{
    public class DataRowTests
    {
        public class Constructor
        {
            [Fact]
            public void ColumnNamesAssignedAndDataInitializedToEmptyList()
            {
                List<string> columns = new List<string> { "col1" };
                DataRow target = new DataRow(columns);

                Assert.Same(columns, target.ColumnNames);
                Assert.NotNull(target.Data);
                Assert.Equal(0, target.Data.Count);
            }
        }

        public class IsNullByColumnIndex
        {
            [Fact]
            public void WhenColumnIndexNegative_ThenThrowsInvalidColumnIndexException()
            {
                List<string> columns = new List<string> { "col1" };
                DataRow target = new DataRow(columns);

                Assert.Throws<InvalidColumnIndexException>(() =>
                    {
                        target.IsNull(-1);
                    });
            }

            [Fact]
            public void WhenColumnIndexTooHigh_ThenThrowsInvalidColumnIndexException()
            {
                List<string> columns = new List<string> { "col1" };
                DataRow target = new DataRow(columns);

                Assert.Throws<InvalidColumnIndexException>(() =>
                {
                    target.IsNull(1);
                });
            }

            [Fact]
            public void WhenNoDataAtIndex_ThenIsNullReturnsTrue()
            {
                List<string> columns = new List<string> { "col1" };
                DataRow target = new DataRow(columns);

                bool result = target.IsNull(0);

                Assert.True(result);
                Assert.Equal(0, target.Data.Count);
            }

            [Fact]
            public void WhenNullValueAtIndex_ThenIsNullReturnsTrue()
            {
                List<string> columns = new List<string> { "col1" };
                DataRow target = new DataRow(columns);

                target[0] = null;

                bool result = target.IsNull(0);

                Assert.True(result);
                Assert.Equal(1, target.Data.Count);
            }

            [Fact]
            public void WhenNonNullValueAtIndex_ThenIsNullReturnsFalse()
            {
                List<string> columns = new List<string> { "col1" };
                DataRow target = new DataRow(columns);

                target[0] = 123;

                bool result = target.IsNull(0);

                Assert.False(result);
                Assert.Equal(1, target.Data.Count);
            }
        }

        public class IsNullByColumnName
        {
            [Fact]
            public void WhenNullValueAtColumn_ThenIsNullReturnsTrue()
            {
                string columnName = "Col 1";
                List<string> columns = new List<string> { columnName };
                DataRow target = new DataRow(columns);

                target[columnName] = null;

                bool result = target.IsNull(columnName);

                Assert.True(result);
                Assert.Equal(1, target.Data.Count);
            }

            [Fact]
            public void WhenNonNullValueAtColumn_ThenIsNullReturnsFalse()
            {
                string columnName = "Col 1";
                List<string> columns = new List<string> { columnName };
                DataRow target = new DataRow(columns);

                target[columnName] = string.Empty;

                bool result = target.IsNull(columnName);

                Assert.False(result);
                Assert.Equal(1, target.Data.Count);
            }

            [Fact]
            public void WhenColumnNameInvalid_ThenThrowsInvalidColumnNameException()
            {
                List<string> columns = new List<string> { "col1" };
                DataRow target = new DataRow(columns);

                Assert.Throws<InvalidColumnNameException>(() =>
                {
                    target.IsNull("Column 2");
                });
            }
        }

        public class GetColumnIndex
        {
            [Fact]
            public void WhenColumnNameIsValid_ThenReturnsTheColumnIndex()
            {
                string columnName = "Second column";
                List<string> columns = new List<string> { "First column", columnName };
                DataRow target = new DataRow(columns);

                int index = target.GetColumnIndex(columnName);

                Assert.Equal(1, index);
            }

            [Fact]
            public void WhenColumnNameIsInvalid_ThenThrowsInvalidColumnNameException()
            {
                List<string> columns = new List<string> { "First column", "Second column" };
                DataRow target = new DataRow(columns);

                Assert.Throws<InvalidColumnNameException>(() =>
                    {
                        target.GetColumnIndex("Col 3");
                    });
            }
        }

        public class IndexerByColumnIndex
        {
            [Fact]
            public void WhenColumnIndexValid_ThenDataValueSetCorrectly()
            {
                List<string> columns = new List<string> { "col1" };
                DataRow target = new DataRow(columns);

                target[0] = 123;

                Assert.Equal(123, target[0]);
            }

            [Fact]
            public void WhenColumnIndexValidButNoValueSetYet_ThenGetterReturnsNull()
            {
                List<string> columns = new List<string> { "col1", "col2" };
                DataRow target = new DataRow(columns);

                object result = target[1];

                Assert.Null(result);
            }

            [Fact]
            public void WhenColumnIndexValidButNoValueSetYet_ThenSetterAddsMissingColumnsAsNecessary()
            {
                List<string> columns = new List<string> { "col1", "col2", "col3" };
                DataRow target = new DataRow(columns);

                target[1] = 123;

                Assert.Null(target[0]);
                Assert.Equal(123, target[1]);
                Assert.Equal(2, target.Data.Count);
                Assert.Equal(3, target.ColumnNames.Count);
            }

            [Fact]
            public void WhenColumnIndexNegative_ThenGetterThrowsInvalidColumnIndexException()
            {
                List<string> columns = new List<string> { "col1" };
                DataRow target = new DataRow(columns);

                Assert.Throws<InvalidColumnIndexException>(() =>
                {
                    object val = target[-1];
                });
            }

            [Fact]
            public void WhenColumnIndexNegative_ThenSetterThrowsInvalidColumnIndexException()
            {
                List<string> columns = new List<string> { "col1" };
                DataRow target = new DataRow(columns);

                Assert.Throws<InvalidColumnIndexException>(() =>
                {
                    target[-1] = 123;
                });
            }

            [Fact]
            public void WhenColumnIndexTooHigh_ThenGetterThrowsInvalidColumnIndexException()
            {
                List<string> columns = new List<string> { "col1" };
                DataRow target = new DataRow(columns);

                Assert.Throws<InvalidColumnIndexException>(() =>
                {
                    object val = target[1];
                });
            }

            [Fact]
            public void WhenColumnIndexTooHigh_ThenSetterThrowsInvalidColumnIndexException()
            {
                List<string> columns = new List<string> { "col1" };
                DataRow target = new DataRow(columns);

                Assert.Throws<InvalidColumnIndexException>(() =>
                {
                    target[1] = 123;
                });
            }
        }

        public class IndexerByColumnName
        {
            [Fact]
            public void WhenColumnNameValid_ThenDataValueSetCorrectly()
            {
                string columnName = "Column 1";
                List<string> columns = new List<string> { columnName };
                DataRow target = new DataRow(columns);

                target[columnName] = 123;

                Assert.Equal(123, target[columnName]);
            }

            [Fact]
            public void WhenColumnNameInvalid_ThenSetterThrowsInvalidColumnNameException()
            {
                string columnName = "Column 1";
                List<string> columns = new List<string> { columnName };
                DataRow target = new DataRow(columns);

                Assert.Throws<InvalidColumnNameException>(() =>
                   {
                       target["Col 2"] = 123;
                   });
            }

            [Fact]
            public void WhenColumnNameInvalid_ThenGetterThrowsInvalidColumnNameException()
            {
                string columnName = "Column 1";
                List<string> columns = new List<string> { columnName };
                DataRow target = new DataRow(columns);

                Assert.Throws<InvalidColumnNameException>(() =>
                {
                    object val = target["Col 2"];
                });
            }
        }
    }
}
