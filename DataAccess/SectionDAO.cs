using Godot;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

public class SectionDao : Node
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
	public Section GetSectionLevels(int worldId, int sectionId)
	{
		string query = String.Format("SELECT s.WorldId, s.SectionId, s.SectionName , l.LevelId FROM Section s INNER JOIN Level l ON s.SectionId = l.SectionId WHERE s.WorldId = {0} AND s.sectionId = {1}", worldId, sectionId);
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
			Section sect = new Section();
			if (lookup.Count() == 0)
				return null;

			sect = lookup.ElementAt(0).Value;
			return sect;
		}
	}
	public int CheckSectionCleared(int worldId, int sectionId, int studentId)
	{
		string query = "SELECT COUNT(l.LevelId) FROM Level l WHERE l.WorldId = @WorldId " +
			"AND l.SectionId = @SectionId AND l.LevelId NOT IN (SELECT ss.LevelId " +
			"FROM StudentScore ss WHERE ss.WorldId = l.WorldId AND ss.SectionId = l.SectionId " +
			"AND ss.StudentId = @StudentId)";

		BaseDao<int> baseDao = new BaseDao<int>();
		var queryObj = new { WorldId = worldId, SectionId = sectionId, StudentId = studentId};

		int result = baseDao.ExecuteScalar(query, queryObj);
		return result;
	}

}
