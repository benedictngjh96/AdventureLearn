using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

public class SectionDao 
{
    public List<Section> GetWorldSections(int worldId)
    {
        string query = String.Format("SELECT s.WorldId, s.SectionId, s.SectionName , l.LevelId FROM Section s INNER JOIN Level l ON s.SectionId = l.SectionId WHERE s.WorldId = {0}", worldId);
        //string query = "SELECT * FROM Section s INNER JOIN Level l ON s.SectionId = l.SectionId";
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            var lookup = new Dictionary<int, Section>();
            conn.Query<Section, Level, Section>(query, (s, l) =>
            {
                Section section;
                if (!lookup.TryGetValue(s.SectionId, out section))
                    lookup.Add(s.SectionId, section = s);
                if (section.Level == null)
                    section.Level = new List<Level>();
                section.Level.Add(l); /* Add locations to course */
                return section;
            }, splitOn: "SectionId, LevelId").AsQueryable();
            List<Section> sectionList = new List<Section>();
            sectionList.AddRange(lookup.Values);

            return sectionList;
        }
    }
    /*
    public Dictionary<int, World> GetWorldSectionLevels(int worldId, int sectionId)
    {
 
        string query = String.Format("SELECT w.WorldId, w.WorldName , s.SectionId , s.SectionName , l.LevelId " +
            " FROM World w INNER JOIN Section s ON w.WorldId = s.WorldId " +
            "INNER JOIN Level l ON s.SectionId = l.SectionId " +
            "WHERE w.WorldId = {0}", worldId);
        List<Level> levelList = new List<Level>();
        var worldDict = new Dictionary<int, World>();
        int i = 0;
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            conn.Query<World, Section, Level, World>(query, (w, s, l) =>
            {
                World world;
                    
                if (!worldDict.TryGetValue(w.WorldId, out world))
                {
                    world = w;
                    world.Section = new List<Section>();
                    worldDict.Add(world.WorldId, world);
                }
                //Section
                if (world.Section == null)
                    world.Section = new List<Section>();


                if (s != null) 
                {
                    if (!world.Section.Any(x => x.SectionId == s.SectionId))
                    {
                        world.Section.Add(s);
                    }
                }
                GD.Print(i);
                //Level
                if (world.Section[i].Level == null)
                    world.Section[i].Level = new List<Level>();
                if (l != null)
                {
                    if (!world.Section[i].Level.Any(x => x.LevelId == l.LevelId))
                    {
                        world.Section[i].Level.Add(l);
                    }
                }
                
                i++;
                return world;

            }, splitOn: "WorldId, SectionId, LevelId").Distinct().ToList();
        }
   
        return worldDict;

    }
    */
    public Section GetSectionLevels(int worldId, int sectionId)
    {
        string query = String.Format("SELECT s.WorldId, s.SectionId, s.SectionName , l.LevelId FROM Section s INNER JOIN Level l ON s.SectionId = l.SectionId WHERE s.WorldId = {0} AND s.sectionId = {1}", worldId, sectionId);
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            var lookup = new Dictionary<int, Section>();
            conn.Query<Section, Level, Section>(query, (s, l) =>
            {
                Section section;
                if (!lookup.TryGetValue(s.SectionId, out section))
                    lookup.Add(s.SectionId, section = s);
                if (section.Level == null)
                    section.Level = new List<Level>();
                section.Level.Add(l); /* Add locations to course */
                return section;
            }, splitOn: "SectionId, LevelId").AsQueryable();
            Section sect = new Section();
            if (lookup.Count() == 0)
                return null;

            sect = lookup.ElementAt(0).Value;
            return sect;
        }
    }
    public int CheckSectionCleared(int worldId, int sectionId, int studentId)
    {
        string query = String.Format("SELECT LevelId FROM ( SELECT l.LevelId FROM Level l WHERE l.WorldId = {0} AND l.SectionId = {1} UNION ALL " +
            "SELECT ss.LevelId FROM StudentScore ss WHERE ss.WorldId = {0} AND ss.SectionId = {1} AND ss.StudentId = {2}) tbl " +
            "GROUP BY LevelId HAVING count(*) = 1 ORDER BY LevelId", worldId, sectionId, studentId);
        BaseDao<int> baseDao = new BaseDao<int>();
        int result = baseDao.ExecuteScalar(query);
        return result;
    }

}
