SimpleDataTable
---------------

Sometimes I feel that System.Data.DataTable is too "heavy-weight" for my needs.  I just need a simple data structure to keep read-only tabular data in memory, and List&lt;List&lt;object&gt;&gt; might even do.

SimpleDataTable looks similar to DataTable, but is just a glorified List&lt;List&lt;object&gt;&gt;.

Of course if your data is strongly typed then you don't even need this kind of thing and something simpler like List&lt;MyPoco&gt; will work even better.

Example of using SimpleDataTable with an ADO.NET data reader:

```c#
SimpleDataTable table = new SimpleDataTable();
using (SqlDataReader reader = cmd.ExecuteReader())
{
	DataReaderAdapter adapter = new DataReaderAdapter();
	adapter.Fill(table, reader);
}
foreach (DataRow row in table.Rows)
{
	Console.WriteLine("{0} {1}", row["id"], row["name"]);
}
```
