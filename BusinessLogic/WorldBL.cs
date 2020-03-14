using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class WorldBL : Node
{
	WorldDao worldDao = new WorldDao();

	public IEnumerable<int> getInaccessibleWorlds()
	{
		List<int> availableWorlds = new List<int>();

		foreach (World w in worldDao.GetWorlds())
		{
			availableWorlds.Add(w.WorldId);
		}

		List<int> completedWorlds = worldDao.getCompletedWorlds();

		List<int> accessibleWorlds = completedWorlds;
		if (accessibleWorlds.Count < availableWorlds.Count)
		{
			if (accessibleWorlds.Count == 0)
				accessibleWorlds.Add(1);
			else
				accessibleWorlds.Add(accessibleWorlds[accessibleWorlds.Count - 1] + 1);
		}
			

		GD.Print("Accessible Worlds: ");
		foreach (int w in accessibleWorlds)
		{
			GD.Print(w);
		}

		IEnumerable<int> inaccessibleWorlds;
		inaccessibleWorlds = availableWorlds.Except(accessibleWorlds);

		GD.Print("Inaccessbile Worlds: ");
		foreach (int w in inaccessibleWorlds)
		{
			GD.Print(w);
		}

		return inaccessibleWorlds;
	}
	
}
