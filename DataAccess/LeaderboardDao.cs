using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

/// <summary>
/// Class to handle DAO operations for Leaderboard
/// </summary>
public class LeaderboardDao
{
    /// <summary>
    /// Get all Leaderboard scores
    /// </summary>
    /// <returns>Return list of Leaderboard object</returns>
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
    /// Get all Leaderboard score on selected World
    /// </summary>
    /// <param name="worldId"></param>
    /// <returns>Return list of leaderboard object </returns>
    public List<Leaderboard> GetLeaderboardScore(int worldId)
    {
        List<Leaderboard> leaderboardList;

        string query = String.Format("SELECT s.StudentName , c.CharName ,SUM(LevelScore) AS TotalScore " +
            "FROM StudentScore ss INNER JOIN Student s ON ss.StudentId = s.StudentId " +
            "INNER JOIN `Characters` c ON c.CharId  = s.CharId  WHERE ss.WorldId = {0} " +
            "GROUP BY ss.StudentId ORDER BY TotalScore DESC", worldId);
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            leaderboardList = conn.Query<Leaderboard>(query).ToList();
        }
        return leaderboardList;
    }
}
