using Godot;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

public class SectionDao : Node
{
    public List<Section> GetSectionLevels(int worldId)
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

}
