using System;
using System.Collections.Generic;
using Dapper;
using MySql.Data.MySqlClient;
using System.Linq;

public class CustomLevelDao
{
    /// <summary>
    /// Return CustomLevel object containing monster and question object
    /// </summary>
    /// <param name="customLevelId"></param>
    /// <returns></returns>
	public CustomLevel GetCustomLevel(int customLevelId)
	{
        string query = String.Format("SELECT cl.CustomLevelId, cl.CustomLevelName , cl.TimeLimit , m.MonsterId ,m.MonsterName , q.QuestionId ,q.QuestionTitle ," +
            "q.Option1 ,q.Option2 ,q.Option3, q.CorrectOption " +
            "FROM CustomLevel cl INNER JOIN StudentCustomQuestion scq ON cl.CustomLevelId  = scq.CustomLevelId " +
            "INNER JOIN Monster m ON m.MonsterId  = cl.MonsterId INNER JOIN Question q ON scq.QuestionId  = q.QuestionId " +
            "WHERE cl.CustomLevelId = 1", customLevelId);
        Dictionary<int, CustomLevel> customLevelDict;
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            customLevelDict = new Dictionary<int, CustomLevel>();
            var list = conn.Query<CustomLevel, Monster, Question, CustomLevel>(
                query,
                (cl, m, q) =>
                {
                    CustomLevel customLevel;
                    if (!customLevelDict.TryGetValue(cl.CustomLevelId, out customLevel))
                    {
                        customLevel = cl;
                        customLevel.Question = new List<Question>();
                        customLevelDict.Add(customLevel.CustomLevelId, customLevel);
                    }
                    customLevel.Question.Add(q);
                    customLevel.Monster = m;
                    return customLevel;
                }, splitOn: "CustomLevelId, MonsterId, QuestionId").Distinct().ToList();
        }
        return customLevelDict[customLevelId];
    }
    /// <summary>
    /// Return int value 1 if InsertCustomLevel has executed successfully
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="customLevelName"></param>
    /// <param name="monsterId"></param>
    /// <param name="timeLimit"></param>
    /// <param name="publicLevel"></param>
    /// <param name=""></param>
    /// <returns></returns>
    public int InsertCustomLevel(int studentId, string customLevelName , int monsterId, int timeLimit, int publicLevel, List<Question> questionList)
    {
        return 1;
    }
}
