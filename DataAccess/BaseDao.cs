using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using Dapper;

public class BaseDAO <T>
{
	public int ExecuteQuery(string query, T t)
	{
		int result = 0;
		using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
		{
			result =conn.Execute(query, t);
		}
		return result;
	}
	public bool ExecuteScalar(string query, Object t)
	{
		bool result;
		
		using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
		{
			result = conn.ExecuteScalar<bool>(query, t);
		}
		return result;
	}

}
