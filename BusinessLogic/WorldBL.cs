using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class WorldBL : Node
{
    WorldDao worldDao = new WorldDao();

    public int getCompletedWorldCount()
    {
        return worldDao.getCompletedWorldCount();
    }

    public int getTotalWorldCount()
    {
        return worldDao.GetWorlds().Count;
    }

}
