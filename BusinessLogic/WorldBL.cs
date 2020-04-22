using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class WorldBL : Node
{
    WorldDao worldDao = new WorldDao();

    /// <summary>
    /// Get the total number of CompletedWorld for the Student from DAO
    /// </summary>
    /// <returns>Return the total count</returns>
    public int getCompletedWorldCount()
    {
        return worldDao.getCompletedWorldCount();
    }

    /// <summary>
    /// Get the total number of Worlds from DAO
    /// </summary>
    /// <returns>Return the total count</returns>
    public int getTotalWorldCount()
    {
        return worldDao.GetWorlds().Count;
    }

}
