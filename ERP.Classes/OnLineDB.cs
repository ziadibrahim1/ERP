using System.Data;
using System.Data.SqlClient;

namespace ERP.Classes;

internal class OnLineDB
{
	private static SqlConnection connection;

	public static bool CreateConnection(string server, string database, string uid, string password)
	{
		string connectionString = " Server= " + server + " ; Database= " + database + " ;User Id= " + uid + " ; Password = " + password;
		connection = new SqlConnection(connectionString);
		try
		{
			connection.Open();
			connection.Close();
			return true;
		}
		catch (SqlException)
		{
			return false;
		}
	}

	public static bool OpenConnection()
	{
		connection.Open();
		return true;
	}

	public static bool CloseConnection()
	{
		connection.Close();
		return true;
	}

	public static void ExecuteNonQuery(string mySelectQuery)
	{
		SqlCommand sqlCommand = new SqlCommand(mySelectQuery, connection);
		sqlCommand.CommandTimeout = 0;
		OpenConnection();
		sqlCommand.ExecuteNonQuery();
		CloseConnection();
	}

	public static DataTable ExecuteQuery_DataTable(string mySelectQuery)
	{
		DataTable dataTable = new DataTable();
		OpenConnection();
		SqlCommand sqlCommand = new SqlCommand(mySelectQuery, connection);
		sqlCommand.CommandTimeout = 0;
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		for (int i = 0; i < sqlDataReader.FieldCount; i++)
		{
			dataTable.Columns.Add(sqlDataReader.GetName(i), sqlDataReader.GetFieldType(i));
		}
		while (sqlDataReader.Read())
		{
			DataRow dataRow = dataTable.NewRow();
			for (int j = 0; j < sqlDataReader.FieldCount; j++)
			{
				dataRow[sqlDataReader.GetName(j)] = sqlDataReader[sqlDataReader.GetName(j)];
			}
			dataTable.Rows.Add(dataRow);
		}
		sqlDataReader.Close();
		CloseConnection();
		return dataTable;
	}

	public static DataTable ExecuteQuery_DataTableDecoded(string mySelectQuery)
	{
		DataTable dataTable = new DataTable();
		OpenConnection();
		SqlCommand sqlCommand = new SqlCommand(mySelectQuery, connection);
		sqlCommand.CommandTimeout = 0;
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		for (int i = 0; i < sqlDataReader.FieldCount; i++)
		{
			dataTable.Columns.Add(sqlDataReader.GetName(i), sqlDataReader.GetFieldType(i));
		}
		while (sqlDataReader.Read())
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow[sqlDataReader.GetName(0)] = sqlDataReader[sqlDataReader.GetName(0)];
			dataRow["MinSerial"] = GlobalFunctions.DecodeText(sqlDataReader["MinSerial"].ToString());
			dataRow["MaxSerial"] = GlobalFunctions.DecodeText(sqlDataReader["MaxSerial"].ToString());
			dataRow["Comp"] = GlobalFunctions.DecodeText(sqlDataReader["Comp"].ToString());
			dataRow["DB"] = GlobalFunctions.DecodeText(sqlDataReader["DB"].ToString());
			dataRow["ServerName"] = GlobalFunctions.DecodeText(sqlDataReader["ServerName"].ToString());
			dataRow["Date"] = sqlDataReader["Date"];
			dataRow["ServerID"] = sqlDataReader["ServerID"];
			dataRow["Locked"] = sqlDataReader["Locked"];
			dataRow["IsNewCon"] = sqlDataReader["IsNewCon"];
			dataRow["VersionName"] = sqlDataReader["VersionName"];
			dataTable.Rows.Add(dataRow);
		}
		sqlDataReader.Close();
		CloseConnection();
		return dataTable;
	}
}
