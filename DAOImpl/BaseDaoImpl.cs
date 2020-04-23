using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using Dapper;

/// <summary>
/// Class to handle DAO operations for Generic methods
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseDaoImpl<T>
{
    /// <summary>
    /// Generic method to query execution using obj parameter
    /// </summary>
    /// <param name="query"></param>
    /// <param name="t"></param>
    /// <returns>Return 1 if query has executed successfully</returns>
    public int ExecuteQuery(string query, Object obj)
    {
        int result = 0;
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            result = conn.Execute(query, obj);
        }
        return result;
    }
    /// <summary>
    /// Generic method for query execution
    /// </summary>
    /// <param name="query"></param>
    /// <returns>Return 1 if query has executed successfully </returns>
    public int ExecuteQuery(string query)
    {
        int result = 0;
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            result = conn.Execute(query);
        }
        return result;
    }
    /// <summary>
    /// Generic method for query sql command 
    /// </summary>
    /// <param name="query"></param>
    /// <returns>Return generic object T if query has executed successfully</returns>
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
    /// Generic method to execute sql command
    /// </summary>
    /// <param name="query"></param>
    /// <param name="t"></param>
    /// <returns>Return int result 1 if successful for scalar query with object passed into query</returns>
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
    /// Generic method to execute scalar sql command
    /// </summary>
    /// <param name="query"></param>
    /// <returns>Return generic object if query has executed successfully</returns>
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
