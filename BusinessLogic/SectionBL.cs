using Godot;
using System;
using System.Collections.Generic;

public class SectionBL : Node
{
	SectionDao sectionDao = new SectionDao();
	public List<Section> GetWorldSections()
	{
		List<Section> sectionList = new List<Section>();
		sectionList = sectionDao.GetWorldSections(Global.WorldId);
		return sectionList;
	}
	public Section DisplaySectionLevels()
	{
		List<Section> sectionList = GetWorldSections();
		Section findSection  = sectionList.Find(x =>x.SectionId == Global.SectionId);
		return findSection;
	}
	public Section GetSectionLevels()
	{
		Section findSection = sectionDao.GetSectionLevels(Global.WorldId, Global.SectionId);
		return findSection;
	}
	public int CheckSectionCleared()
	{
		int result = sectionDao.CheckSectionCleared(Global.WorldId, Global.SectionId-1, Global.StudentId);
		return result;
	}
}
