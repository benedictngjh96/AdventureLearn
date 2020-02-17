using Godot;
using System;
using System.Collections.Generic;

public class SectionBL : Node
{
    SectionDao sectionDao = new SectionDao();
    public List<Section> GetWorldSections()
    {
        List<Section> sectionList = new List<Section>();
        sectionList = sectionDao.GetSectionLevels(Global.worldId);
        return sectionList;
    }
    public Section DisplaySectionLevels()
    {
        List<Section> sectionList = GetWorldSections();
        Section findSection  = sectionList.Find(x =>x.SectionId == Global.sectionId);
        return findSection;
    }
}
