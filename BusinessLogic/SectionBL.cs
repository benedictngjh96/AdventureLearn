using Godot;
using System;
using System.Collections.Generic;

public class SectionBL : Node
{
    SectionDao sectionDao = new SectionDao();
    public List<Section> GetWorldSections()
    {
        List<Section> sectionList = new List<Section>();
        sectionList = sectionDao.GetWorldSections(Global.worldId);
        return sectionList;
    }
    public Section DisplaySectionLevels()
    {
        List<Section> sectionList = GetWorldSections();
        Section findSection  = sectionList.Find(x =>x.SectionId == Global.sectionId);
        return findSection;
    }
    public Section GetSectionLevels()
    {
        Section findSection = sectionDao.GetSectionLevels(Global.worldId, Global.sectionId);
        return findSection;
    }
}
