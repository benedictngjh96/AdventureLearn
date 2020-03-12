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
            "WHERE cl.CustomLevelId = {0}", customLevelId);

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
    public int InsertCustomLevel(int studentId, string customLevelName, int monsterId, int timeLimit, int publicLevel, List<Question> questionList)
    {
        return 1;
    }
    public List<CustomLevel> GetCustomLevels()
    {
        string query = "SELECT cl.CustomLevelId, cl.CustomLevelName , cl.TimeLimit , s.StudentId , s.StudentName " +
        "FROM CustomLevel cl  INNER JOIN Student s ON s.StudentId  = cl.StudentId WHERE PublicLevel = true";
        Dictionary<int, CustomLevel> customLevelDict;
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            customLevelDict = new Dictionary<int, CustomLevel>();
            var list = conn.Query<CustomLevel, Student, CustomLevel>(
                query,
                (cl, s) =>
                {
                    CustomLevel customLevel;
                    if (!customLevelDict.TryGetValue(cl.CustomLevelId, out customLevel))
                    {
                        customLevel = cl;
                        customLevel.Question = new List<Question>();
                        customLevelDict.Add(customLevel.CustomLevelId, customLevel);
                    }
                    customLevel.Student = s;
                    return customLevel;
                }, splitOn: "CustomLevelId, StudentId").Distinct().ToList();
        }
        List<CustomLevel> customLevels = new List<CustomLevel>();
        customLevels.AddRange(customLevelDict.Values);

        return customLevels;
    }
    public List<CustomLevelScore> GetClearedCustomLevels(int studentId)
    {
        string query = String.Format("SELECT cls.CustomLevelId ,cls.LevelScore, cl.CustomLevelId ,cl.CustomLevelName ,cl.StudentId ,s.StudentName " +
         "FROM CustomLevelScore cls INNER JOIN CustomLevel cl ON cls.CustomLevelId  = cl.CustomLevelId  " +
        "INNER JOIN Student s ON cl.StudentId  = s.StudentId WHERE cls.StudentId  = {0}", studentId);
        Dictionary<int, CustomLevelScore> customLevelDict = null;
        Godot.GD.Print(query);
        int count = 0;
        try
        {
            using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
            {
                customLevelDict = new Dictionary<int, CustomLevelScore>();
                var list = conn.Query<CustomLevelScore, CustomLevel, Student, CustomLevelScore>(
                    query,
                    (cls, cl, s) =>
                    {
                        CustomLevelScore customLevel;
                        if (!customLevelDict.TryGetValue(count, out customLevel))
                        {
                            customLevel = cls;
                            customLevelDict.Add(count, customLevel);
                        }
                        customLevel.CustomLevel = cl;
                        customLevel.Student = s;
                        count++;
                        return customLevel;
                    }, splitOn: "CustomLevelId,StudentId").Distinct().ToList();
            }

        }
        catch (Exception ex)
        {
            Godot.GD.Print(ex.Message);
        }
        List<CustomLevelScore> customLevels = new List<CustomLevelScore>();
        customLevels.AddRange(customLevelDict.Values);
        return customLevels;
    }
}
