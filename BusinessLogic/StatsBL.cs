using System;
using System.Collections.Generic;
using Godot;

public class StatsBL: Node
{
    public List<World> GetWorlds(){
        WorldDao worldDao = new WorldDao();
        return worldDao.GetWorlds();
    }
    public List<Section> GetSections(int worldId){
        SectionDao sectionDao = new SectionDao();
        return sectionDao.GetWorldSections(worldId);

    }
    public static List<Student> GetStudents(){
        StudentDao studentDao = new StudentDao();
        return studentDao.GetStudents();
    }
    public void printda(){
        GD.Print("gdsgdsgdsgdsg");
    }
}
