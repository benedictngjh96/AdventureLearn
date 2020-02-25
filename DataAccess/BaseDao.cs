using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using Dapper;

public class BaseDao <T>
{
	/// <summary>
	/// Generic method to return int result 1 if successful for execute sql query
	/// </summary>
	/// <param name="query"></param>
	/// <param name="t"></param>
	/// <returns></returns>
	public int ExecuteQuery(string query, Object obj)
	{
		int result = 0;
		using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
		{
			result =conn.Execute(query, obj);
		}
		return result;
	}
	/// <summary>
	/// Generic method for retrieve query to return generic object T 
	/// </summary>
	/// <param name="query"></param>
	/// <returns></returns>
	public T RetrieveQuery(string query)
	{
		T result;
		using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
		{
			result = conn.QueryFirstOrDefault<T>(query);
		}
		return result;
	}
	/// <summary>
	/// Generic method to return int result 1 if successful for scalar query with object passed into query
	/// </summary>
	/// <param name="query"></param>
	/// <param name="t"></param>
	/// <returns></returns>
	public T ExecuteScalar(string query, Object obj)
	{
		T result;
		
		using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
		{
			result = conn.ExecuteScalar<T>(query, obj);
		}
		return result;
	}
	/// <summary>
	/// Generic method to return int result 1 if successful for scalar query
	/// </summary>
	/// <param name="query"></param>
	/// <returns></returns>
	public T ExecuteScalar(string query)
	{
		T result;

		using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
		{
			result = conn.ExecuteScalar<T>(query);
		}
		return result;
	}

}
