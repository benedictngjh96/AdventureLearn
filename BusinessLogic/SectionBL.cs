using System;
using System.Collections.Generic;

public class SectionBL 
{
	public List<Section> GetWorldSections(int worldId)
	{
		SectionDao sectionDao = new SectionDao();
		List<Section> sectionList = new List<Section>();
		sectionList = sectionDao.GetWorldSections(worldId);
		return sectionList;
	}
	
	public Section DisplaySectionLevels(int worldId, int sectionId)
	{
		List<Section> sectionList = GetWorldSections(worldId);
		Section findSection  = sectionList.Find(x =>x.SectionId == sectionId);
		return findSection;
	}
	public Section GetSectionLevels(int worldId, int sectionId)
	{
		SectionDao sectionDao = new SectionDao();
		Section findSection = sectionDao.GetSectionLevels(worldId, sectionId);
		return findSection;
	}
	public int CheckSectionCleared(int worldId, int sectionId, int studentId)
	{
		SectionDao sectionDao = new SectionDao();
		int result = sectionDao.CheckSectionCleared(worldId, sectionId-1, studentId);
		return result;
	}
}
