using System;
using System.Collections.Generic;

public class LeaderboardBL
{
    /// <summary>
    /// Return list of Leaderboard object according to worldId
    /// </summary>
    /// <param name="worldId"></param>
    /// <returns></returns>
    public List<Leaderboard> GetWorldLeaderboard(int worldId)
    {
        LeaderboardDao leaderboardDao = new LeaderboardDao();

        return leaderboardDao.GetLeaderboardScore(worldId);
    }
    /// <summary>
    /// Return list of all Leaderboard objects
    /// </summary>
    /// <returns></returns>
    public List<Leaderboard> GetLeaderboards()
    {
        LeaderboardDao leaderboardDao = new LeaderboardDao();

        return leaderboardDao.GetLeaderboardScore();
    }
    /// <summary>
    /// Return list of  all World objects
    /// </summary>
    /// <returns></returns>
    public List<World> GetWorlds()
    {
        WorldDao worldDao = new WorldDao();
        return worldDao.GetWorlds();
    }
    /// <summary>
    /// Return leaderboard object of Student
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    public Leaderboard GetStudentLeaderboard(int studentId)
    {
        return null;
    }
}
