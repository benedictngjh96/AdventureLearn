using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

public class WorldDao
{
    /// <summary>
    /// Get all worlds object and store inside List of world object
    /// </summary>
    /// <returns></returns>
    public List<World> GetWorlds()
    {
        List<World> worldList = new List<World>();
        string query = "SELECT * FROM World";
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            worldList = conn.Query<World>(query).ToList();
        }
        return worldList;
    }

    public List<World> GetWorldSections()
    {
        string query = "SELECT w.WorldId ,w.WorldName ,s.SectionId ,s.SectionName FROM World w INNER JOIN "
        + "Section s ON s.WorldId  = w.WorldId";
        //string query = "SELECT * FROM Section s INNER JOIN Level l ON s.SectionId = l.SectionId";
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            var lookup = new Dictionary<int, World>();
            conn.Query<World, Section, World>(query, (w, s) =>
            {
                World world;
                if (!lookup.TryGetValue(w.WorldId, out world))
                    lookup.Add(w.WorldId, world = w);
                if (world.Section == null)
                    world.Section = new List<Section>();
                world.Section.Add(s); /* Add locations to course */
                return world;
            }, splitOn: "WorldId, SectionId").AsQueryable();
            List<World> worldList = new List<World>();
            worldList.AddRange(lookup.Values);

            return worldList;
        }
    }

    /// <summary>
    /// Return all the list of Completed Worlds
    /// </summary>
    /// <returns></returns>
    public List<int> getCompletedWorlds()
    {
        List<int> worldList = new List<int>();
        string query = String.Format("SELECT WorldId FROM StudentScore WHERE StudentId = {0} AND SectionId = 3 AND LevelId = 5; "
            , Global.StudentId);
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            worldList = conn.Query<int>(query).ToList();
        }
        return worldList;
    }
}
