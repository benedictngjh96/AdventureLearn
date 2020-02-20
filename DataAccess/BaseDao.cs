using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using Dapper;

public class BaseDao <T>
{
	public int ExecuteQuery(string query, Object t)
	{
		int result = 0;
		using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
		{
			result =conn.Execute(query, t);
		}
		return result;
	}
	public T RetrieveQuery(string query)
	{
		T result;
		using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
		{
			result = conn.QueryFirstOrDefault<T>(query);
		}
		return result;
	}
	public T ExecuteScalar(string query, Object t)
	{
		T result;
		
		using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
		{
			result = conn.ExecuteScalar<T>(query, t);
		}
		return result;
	}

}
