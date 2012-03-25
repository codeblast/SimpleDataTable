using System;
using System.Collections.Generic;
using System.Data;
using Codeblast.SimpleDataTable.Adapter;
using Moq;
using Xunit;

namespace Codeblast.SimpleDataTable.Tests.Adapter
{
    public class DataReaderAdapterTests
    {
        public class Fill
        {
            [Fact]
            public void WhenDataReaderReturnsNoResults_ThenTheDataTableIsNotAffectedAtAll()
            {
                MockRepository mockRepository = new MockRepository(MockBehavior.Strict);

                Mock<IDataReader> dataReaderMock = mockRepository.Create<IDataReader>();
                dataReaderMock.Setup(x => x.Read()).Returns(false);

                Mock<ISimpleDataTable> dataTableMock = mockRepository.Create<ISimpleDataTable>();

                DataReaderAdapter target = new DataReaderAdapter();

                target.Fill(dataTableMock.Object, dataReaderMock.Object);

                dataTableMock.Verify(x => x.NewRow(), Times.Never());
            }

            [Fact]
            public void WhenDataReaderReturnsOneRowAndDataTableIsEmpty_ThenARowIsAddedAndColumnsAdded()
            {
                List<string> columns = new List<string>();

                MockRepository mockRepository = new MockRepository(MockBehavior.Strict);

                Mock<IDataReader> dataReaderMock = mockRepository.Create<IDataReader>();
                dataReaderMock.SetupSequence(x => x.Read()).Returns(true).Returns(false);
                dataReaderMock.SetupGet(x => x.FieldCount).Returns(2);
                dataReaderMock.Setup(x => x.IsDBNull(It.IsInRange<int>(0, 1, Range.Inclusive))).Returns(false);
                dataReaderMock.Setup(x => x.GetValue(It.IsInRange<int>(0, 1, Range.Inclusive))).Returns<int>(n => n);
                dataReaderMock.Setup(x => x.GetName(It.IsInRange<int>(0, 1, Range.Inclusive))).Returns<int>(n => "Col" + n);

                Mock<IDataRow> rowMock = mockRepository.Create<IDataRow>();
                rowMock.SetupSet(x => x[0] = 0);
                rowMock.SetupSet(x => x[1] = 1);

                Mock<ISimpleDataTable> dataTableMock = mockRepository.Create<ISimpleDataTable>();
                dataTableMock.SetupGet(x => x.ColumnNames).Returns(columns);
                dataTableMock.Setup(x => x.NewRow()).Returns(rowMock.Object);

                DataReaderAdapter target = new DataReaderAdapter();

                target.Fill(dataTableMock.Object, dataReaderMock.Object);

                dataTableMock.Verify(x => x.NewRow(), Times.Once());
                dataReaderMock.Verify(x => x.GetName(It.IsAny<int>()), Times.Exactly(2));

                Assert.Equal(2, columns.Count);
                Assert.Equal("Col0", columns[0]);
                Assert.Equal("Col1", columns[1]);
            }

            [Fact]
            public void WhenDataReaderGetNameReturnsNull_ThenTheColumnIndexIsUsedAsColumnName()
            {
                List<string> columns = new List<string>();

                MockRepository mockRepository = new MockRepository(MockBehavior.Strict);

                Mock<IDataReader> dataReaderMock = mockRepository.Create<IDataReader>();
                dataReaderMock.SetupSequence(x => x.Read()).Returns(true).Returns(false);
                dataReaderMock.SetupGet(x => x.FieldCount).Returns(2);
                dataReaderMock.Setup(x => x.IsDBNull(It.IsInRange<int>(0, 1, Range.Inclusive))).Returns(false);
                dataReaderMock.Setup(x => x.GetValue(It.IsInRange<int>(0, 1, Range.Inclusive))).Returns<int>(n => n);
                dataReaderMock.Setup(x => x.GetName(It.IsInRange<int>(0, 1, Range.Inclusive))).Returns((string)null);

                Mock<IDataRow> rowMock = mockRepository.Create<IDataRow>();
                rowMock.SetupSet(x => x[0] = 0);
                rowMock.SetupSet(x => x[1] = 1);

                Mock<ISimpleDataTable> dataTableMock = mockRepository.Create<ISimpleDataTable>();
                dataTableMock.SetupGet(x => x.ColumnNames).Returns(columns);
                dataTableMock.Setup(x => x.NewRow()).Returns(rowMock.Object);

                DataReaderAdapter target = new DataReaderAdapter();

                target.Fill(dataTableMock.Object, dataReaderMock.Object);

                dataTableMock.Verify(x => x.NewRow(), Times.Once());
                dataReaderMock.Verify(x => x.GetName(It.IsAny<int>()), Times.Exactly(2));

                Assert.Equal(2, columns.Count);
                Assert.Equal("0", columns[0]);
                Assert.Equal("1", columns[1]);
            }

            [Fact]
            public void WhenDataReaderReturnsOneRowAndDataTableAlreadyHasEnoughColumns_ThenARowIsAddedButColumnsNotModified()
            {
                List<string> columns = new List<string> { "col1", "col2" };

                MockRepository mockRepository = new MockRepository(MockBehavior.Strict);

                Mock<IDataReader> dataReaderMock = mockRepository.Create<IDataReader>();
                dataReaderMock.SetupSequence(x => x.Read()).Returns(true).Returns(false);
                dataReaderMock.SetupGet(x => x.FieldCount).Returns(2);
                dataReaderMock.Setup(x => x.IsDBNull(It.IsInRange<int>(0, 1, Range.Inclusive))).Returns(false);
                dataReaderMock.Setup(x => x.GetValue(It.IsInRange<int>(0, 1, Range.Inclusive))).Returns<int>(n => n);
                
                Mock<IDataRow> rowMock = mockRepository.Create<IDataRow>();
                rowMock.SetupSet(x => x[0] = 0);
                rowMock.SetupSet(x => x[1] = 1);

                Mock<ISimpleDataTable> dataTableMock = mockRepository.Create<ISimpleDataTable>();
                dataTableMock.SetupGet(x => x.ColumnNames).Returns(columns);
                dataTableMock.Setup(x => x.NewRow()).Returns(rowMock.Object);

                DataReaderAdapter target = new DataReaderAdapter();

                target.Fill(dataTableMock.Object, dataReaderMock.Object);

                dataTableMock.Verify(x => x.NewRow(), Times.Once());
                dataReaderMock.Verify(x => x.GetName(It.IsAny<int>()), Times.Never());

                Assert.Equal(2, columns.Count);
                Assert.Equal("col1", columns[0]);
                Assert.Equal("col2", columns[1]);
            }

            [Fact]
            public void WhenDataReaderIsDBNullReturnsTrue_ThenTheFieldIsNotSetInTheRow()
            {
                List<string> columns = new List<string>();

                MockRepository mockRepository = new MockRepository(MockBehavior.Strict);

                Mock<IDataReader> dataReaderMock = mockRepository.Create<IDataReader>();
                dataReaderMock.SetupSequence(x => x.Read()).Returns(true).Returns(false);
                dataReaderMock.SetupGet(x => x.FieldCount).Returns(2);
                dataReaderMock.Setup(x => x.IsDBNull(0)).Returns(false);
                dataReaderMock.Setup(x => x.IsDBNull(1)).Returns(true);
                dataReaderMock.Setup(x => x.GetValue(0)).Returns(0);
                dataReaderMock.Setup(x => x.GetName(It.IsInRange<int>(0, 1, Range.Inclusive))).Returns<int>(n => "Col" + n);

                Mock<IDataRow> rowMock = mockRepository.Create<IDataRow>();
                rowMock.SetupSet(x => x[0] = 0);

                Mock<ISimpleDataTable> dataTableMock = mockRepository.Create<ISimpleDataTable>();
                dataTableMock.SetupGet(x => x.ColumnNames).Returns(columns);
                dataTableMock.Setup(x => x.NewRow()).Returns(rowMock.Object);

                DataReaderAdapter target = new DataReaderAdapter();

                target.Fill(dataTableMock.Object, dataReaderMock.Object);

                dataTableMock.Verify(x => x.NewRow(), Times.Once());
                dataReaderMock.Verify(x => x.GetValue(0), Times.Once());
                dataReaderMock.Verify(x => x.GetValue(1), Times.Never());
                rowMock.VerifySet(x => x[0] = It.IsAny<int>(), Times.Once());
                rowMock.VerifySet(x => x[1] = It.IsAny<int>(), Times.Never());
            }
        }
    }
}
