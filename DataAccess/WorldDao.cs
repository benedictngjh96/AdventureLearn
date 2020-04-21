using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

public class WorldDao
{
    /// <summary>
    /// Get all Worlds
    /// </summary>
    /// <returns>Return list of World object</returns>
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
    /// <summary>
    /// Get the total number of CompletedWorld for the Student
    /// </summary>
    /// <returns>Return the total count of Completed Worlds</returns>
    public int GetCompletedWorldCount()
    {
        string query = String.Format("SELECT COUNT(1) FROM World_LastSection_LastLevel w, StudentScore s " +
            "WHERE s.StudentId = {0} AND w.WorldId = s.WorldId AND w.LastSectionId = s.SectionId AND w.LastLevelId = s.LevelId;"
            , Global.StudentId);

        BaseDao<int> baseDao = new BaseDao<int>();
        return baseDao.RetrieveQuery(query);
    }
}
