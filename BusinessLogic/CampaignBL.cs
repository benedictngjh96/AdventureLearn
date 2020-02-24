using System;
using System.Collections.Generic;

public class CampaignBL 
{
	public Level GetLevel(int worldId, int sectionId, int levelId)
	{
		CampaignDao campaignDao = new CampaignDao();
		return campaignDao.GetLevel(worldId, sectionId, levelId);
	}
	
}
