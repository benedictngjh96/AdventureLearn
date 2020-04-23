using System;
using System.Collections.Generic;

/// <summary>
/// Class to handle Business Logic for Leaderboard
/// </summary>
public class LeaderboardBL
{
    /// <summary>
    /// Get all Leaderboard scores of selected World
    /// </summary>
    /// <param name="worldId"></param>
    /// <returns>Return list of Leaderboard object</returns>
    public List<Leaderboard> GetWorldLeaderboard(int worldId)
    {
        LeaderboardDaoImpl leaderboardDao = new LeaderboardDaoImpl();

        return leaderboardDao.GetLeaderboardScore(worldId);
    }
    /// <summary>
    /// Get all Leaderboard score of all Worlds
    /// </summary>
    /// <returns>Return list of Leaderboard object</returns>
    public List<Leaderboard> GetLeaderboards()
    {
        LeaderboardDaoImpl leaderboardDao = new LeaderboardDaoImpl();

        return leaderboardDao.GetLeaderboardScore();
    }
    /// <summary>
    /// Get all Worlds
    /// </summary>
    /// <returns>Return list of World object</returns>
    public List<World> GetWorlds()
    {
        WorldDaoImpl worldDao = new WorldDaoImpl();
        return worldDao.GetWorlds();
    }
    
}
