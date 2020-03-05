using System;
using System.Collections.Generic;

public class SectionBL 
{
    /// <summary>
    /// Return list of Section according to worldId
    /// </summary>
    /// <param name="worldId"></param>
    /// <returns></returns>
    public List<Section> GetWorldSections(int worldId)
    {
        SectionDao sectionDao = new SectionDao();
        List<Section> sectionList = new List<Section>();
        sectionList = sectionDao.GetWorldSections(worldId);
        return sectionList;
    }
    
    /// <summary>
    /// Return Section object according to worldId and sectionId
    /// </summary>
    /// <param name="worldId"></param>
    /// <param name="sectionId"></param>
    /// <returns></returns>
    public Section GetSectionLevels(int worldId, int sectionId)
    {
        SectionDao sectionDao = new SectionDao();
        Section findSection = sectionDao.GetSectionLevels(worldId, sectionId);
        return findSection;
    }
    /// <summary>
    /// Return int result 1 if Student has cleared all levels in the section
    /// </summary>
    /// <param name="worldId"></param>
    /// <param name="sectionId"></param>
    /// <param name="studentId"></param>
    /// <returns></returns>
    public int CheckSectionCleared(int worldId, int sectionId, int studentId)
    {
        SectionDao sectionDao = new SectionDao();
        int result = sectionDao.CheckSectionCleared(worldId, sectionId-1, studentId);
        return result;
    }
}
