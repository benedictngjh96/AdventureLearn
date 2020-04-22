using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Class to handle Business Logic for World
/// </summary>
public class WorldBL : Node
{
    WorldDao worldDao = new WorldDao();
    /// <summary>
    /// Get the total number of CompletedWorld for the Student
    /// </summary>
    /// <returns>Return the total count of Completed Worlds</returns>
    public int GetCompletedWorldCount()
    {
        return worldDao.GetCompletedWorldCount();
    }
    /// <summary>
    /// Get the number of Worlds
    /// </summary>
    /// <returns>Return integer value of number of Worlds</returns>
    public int GetTotalWorldCount()
    {
        return worldDao.GetWorlds().Count;
    }

}
