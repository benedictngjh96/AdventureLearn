using System;
using System.Collections.Generic;

public class CampaignBL
{
    /// <summary>
    /// Return Level object according to worldId, sectionId and levelId
    /// </summary>
    /// <param name="worldId"></param>
    /// <param name="sectionId"></param>
    /// <param name="levelId"></param>
    /// <returns></returns>
    public Level GetLevel(int worldId, int sectionId, int levelId)
    {
        CampaignDao campaignDao = new CampaignDao();
        return campaignDao.GetLevel(worldId, sectionId, levelId);
    }

}
