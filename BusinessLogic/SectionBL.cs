using System;
using System.Collections.Generic;

public class SectionBL 
{
    /// <summary>
    /// Get all Section that belongs to selected World
    /// </summary>
    /// <param name="worldId"></param>
    /// <returns>Return list of Section object</returns>
    public List<Section> GetWorldSections(int worldId)
    {
        SectionDao sectionDao = new SectionDao();
        List<Section> sectionList = new List<Section>();
        sectionList = sectionDao.GetWorldSections(worldId);
        return sectionList;
    }

    /// <summary>
    /// Get all Levels that belong to selected Section
    /// </summary>
    /// <param name="worldId"></param>
    /// <param name="sectionId"></param>
    /// <returns>Return Section object containing list of Level object</returns>
    public Section GetSectionLevels(int worldId, int sectionId)
    {
        SectionDao sectionDao = new SectionDao();
        Section findSection = sectionDao.GetSectionLevels(worldId, sectionId);
        return findSection;
    }
    /// <summary>
    /// Check if Student has cleared the selected World's Section
    /// </summary>
    /// <param name="worldId"></param>
    /// <param name="sectionId"></param>
    /// <param name="studentId"></param>
    /// <returns>Return int result 1 if Student has cleared the section</returns>
    public int CheckSectionCleared(int worldId, int sectionId, int studentId)
    {
        SectionDao sectionDao = new SectionDao();
        int result = sectionDao.CheckSectionCleared(worldId, sectionId-1, studentId);
        return result;
    }
}
