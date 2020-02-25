using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

public class WorldDao 
{
    /// <summary>
    /// Get all worlds object and store inside List of world object
    /// </summary>
    /// <returns></returns>
    public List<World> GetWorlds()
    {
        List<World> worldList = new List<World>();
        string query = "SELECT * FROM World";
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            worldList = conn.Query<World>(query).ToList();
        }
        return worldList;
    }
}
