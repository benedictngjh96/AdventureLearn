using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

public class CampaignDao 
{
    /// <summary>
    /// Return level object based on worldId, sectionId, levelId
    /// </summary>
    /// <param name="worldId"></param>
    /// <param name="sectionId"></param>
    /// <param name="levelId"></param>
    /// <returns></returns>
    public Level GetLevel(int worldId, int sectionId, int levelId)
    {
        string query = String.Format("SELECT l.LevelId , l.TimeLimit, m.MonsterId , m.MonsterName , q.QuestionId, q.QuestionTitle, q.Option1, q.Option2, q.Option3, q.CorrectOption " +
            "FROM Level l INNER JOIN Monster m ON l.MonsterId = m.MonsterId INNER JOIN CampaignQuestion cq ON cq.LevelId  = l.LevelId " +
            "INNER JOIN Question q ON q.QuestionId  = cq.QuestionId WHERE l.WorldId ={0} AND l.SectionId = {1} AND l.LevelId = {2}"
            , worldId, sectionId, levelId);
        Dictionary<int, Level> levelDict;

        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            levelDict = new Dictionary<int, Level>();
            var list = conn.Query<Level, Monster, Question, Level>(
                query,
                (l, m, q) =>
                {
                    Level level;
                    if (!levelDict.TryGetValue(l.LevelId, out level))
                    {
                        level = l;
                        level.Question = new List<Question>();
                        levelDict.Add(level.LevelId, level);
                    }
                    level.Question.Add(q);
                    level.Monster = m;
                    return level;
                }, splitOn: "LevelId, MonsterId, QuestionId").Distinct().ToList();
        }
        return levelDict[levelId];
    }
 

}
