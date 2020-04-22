using System;
using System.Collections.Generic;

public class LeaderboardBL
{
    /// <summary>
    /// Get all Leaderboard scores of selected World
    /// </summary>
    /// <param name="worldId"></param>
    /// <returns>Return list of Leaderboard object</returns>
    public List<Leaderboard> GetWorldLeaderboard(int worldId)
    {
        LeaderboardDao leaderboardDao = new LeaderboardDao();

        return leaderboardDao.GetLeaderboardScore(worldId);
    }
    /// <summary>
    /// Get all Leaderboard score of all Worlds
    /// </summary>
    /// <returns>Return list of Leaderboard object</returns>
    public List<Leaderboard> GetLeaderboards()
    {
        LeaderboardDao leaderboardDao = new LeaderboardDao();

        return leaderboardDao.GetLeaderboardScore();
    }
    /// <summary>
    /// Get all Worlds
    /// </summary>
    /// <returns>Return list of World object</returns>
    public List<World> GetWorlds()
    {
        WorldDao worldDao = new WorldDao();
        return worldDao.GetWorlds();
    }
    
}
