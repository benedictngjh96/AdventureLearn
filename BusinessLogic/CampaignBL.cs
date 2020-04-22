using System;
using System.Collections.Generic;

/// <summary>
/// Class to handle Business Logic for Campaign
/// </summary>
public class CampaignBL
{
    /// <summary>
    /// Get Level of selected World and Section
    /// </summary>
    /// <param name="worldId"></param>
    /// <param name="sectionId"></param>
    /// <param name="levelId"></param>
    /// <returns>Return Level object</returns>
    public Level GetLevel(int worldId, int sectionId, int levelId)
    {
        CampaignDao campaignDao = new CampaignDao();
        return campaignDao.GetLevel(worldId, sectionId, levelId);
    }

}
