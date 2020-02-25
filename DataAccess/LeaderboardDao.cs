using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

public class LeaderboardDao 
{
    /// <summary>
    /// Return list of Leaderboard object
    /// </summary>
    /// <returns></returns>
    public List<Leaderboard> GetLeaderboardScore()
    {
        List<Leaderboard> leaderboardList;

        string query = "SELECT s.StudentName , c.CharName ,SUM(LevelScore) AS TotalScore " +
            "FROM StudentScore ss INNER JOIN Student s ON ss.StudentId = s.StudentId " +
            "INNER JOIN `Characters` c ON c.CharId  = s.CharId " +
            "GROUP BY ss.StudentId ORDER BY TotalScore DESC";
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            leaderboardList = conn.Query<Leaderboard>(query).ToList();
        }
        return leaderboardList;
    }
    /// <summary>
    /// Return list of leaderboard object filtered according to worldId
    /// </summary>
    /// <param name="worldId"></param>
    /// <returns></returns>
    public List<Leaderboard> GetLeaderboardScore(int worldId)
    {
        List<Leaderboard> leaderboardList;

        string query = String.Format("SELECT s.StudentName , c.CharName ,SUM(LevelScore) AS TotalScore " +
            "FROM StudentScore ss INNER JOIN Student s ON ss.StudentId = s.StudentId " +
            "INNER JOIN `Characters` c ON c.CharId  = s.CharId  WHERE ss.WorldId = {0}" +
            "GROUP BY ss.StudentId ORDER BY TotalScore DESC", worldId);
        
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            leaderboardList = conn.Query<Leaderboard>(query).ToList();
        }
        return leaderboardList;
    }
}
