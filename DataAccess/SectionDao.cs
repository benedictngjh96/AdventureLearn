using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

/// <summary>
/// Class to handle DAO operations for SectionDao
/// </summary>
public class SectionDao 
{
    /// <summary>
    /// Get all Sections of selected World
    /// </summary>
    /// <param name="worldId"></param>
    /// <returns>Return list of Section objects</returns>
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
    /// <summary>
    /// Get all levels that belongs to selected Section
    /// </summary>
    /// <param name="worldId"></param>
    /// <param name="sectionId"></param>
    /// <returns>Return Section object containing list of Level object</returns>
    public Section GetSectionLevels(int worldId, int sectionId)
    {
        string query = String.Format("SELECT WorldId, SectionId, SectionName , LevelId FROM World NATURAL JOIN Section NATURAL JOIN Level WHERE World.WorldId  = {0} AND Section.SectionId  = {1}", worldId, sectionId);
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
                section.Level.Add(l);
                return section;
            }, splitOn: "SectionId, LevelId").AsQueryable();
            Section sect = new Section();
            if (lookup.Count() == 0)
                return null;

            sect = lookup.ElementAt(0).Value;
            return sect;
        }
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
        string query = String.Format("SELECT LevelId FROM ( SELECT l.LevelId FROM Level l WHERE l.WorldId = {0} AND l.SectionId = {1} UNION ALL " +
            "SELECT ss.LevelId FROM StudentScore ss WHERE ss.WorldId = {0} AND ss.SectionId = {1} AND ss.StudentId = {2}) tbl " +
            "GROUP BY LevelId HAVING count(*) = 1 ORDER BY LevelId", worldId, sectionId, studentId);
        BaseDao<int> baseDao = new BaseDao<int>();
        int result = baseDao.ExecuteScalar(query);
        return result;
    }

}
