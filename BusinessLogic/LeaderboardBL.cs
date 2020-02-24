using System;
using System.Collections.Generic;

public class LeaderboardBL 
{
    public List<Leaderboard> GetWorldLeaderboard(int worldId)
    {
        LeaderboardDao leaderboardDao = new LeaderboardDao();

        return leaderboardDao.GetLeaderboardScore(worldId);
    }
    public List<Leaderboard> GetLeaderboards()
    {
        LeaderboardDao leaderboardDao = new LeaderboardDao();

        return leaderboardDao.GetLeaderboardScore();
    }
    public List<World> GetWorlds()
    {
        WorldDao worldDao = new WorldDao();
        return worldDao.GetWorlds();
    }
}
