using System;
using System.Collections.Generic;
using Dapper;
using MySql.Data.MySqlClient;
using System.Linq;

/// <summary>
/// Class to handle DAO operations for CustomLevel
/// </summary>
public class CustomLevelDao
{
    /// <summary>
    /// Get selected CustomLevel
    /// </summary>
    /// <param name="customLevelId"></param>
    /// <returns>Return CustomLevel object containing monster and question object</returns>
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
    /// Get all custom levels
    /// </summary>
    /// <returns>Return list of CustomLevel object</returns>
    public List<CustomLevel> GetCustomLevels()
    {
        string query = String.Format("SELECT cl.CustomLevelId, cl.CustomLevelName , cl.TimeLimit , s.StudentId , s.StudentName " +
        "FROM CustomLevel cl  INNER JOIN Student s ON s.StudentId  = cl.StudentId");
        Godot.GD.Print(query);
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
    /// <summary>
    /// Get all custom levels which had been cleared by the Student
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns>Return list of CustomLevelScore object</returns>
    public List<CustomLevelScore> GetClearedCustomLevels(int studentId)
    {
        string query = String.Format("SELECT cls.CustomLevelId ,cls.LevelScore, cl.CustomLevelId ,cl.CustomLevelName ,cl.StudentId ,s.StudentName " +
         "FROM CustomLevelScore cls INNER JOIN CustomLevel cl ON cls.CustomLevelId  = cl.CustomLevelId  " +
        "INNER JOIN Student s ON cl.StudentId  = s.StudentId WHERE cls.StudentId  = {0}", studentId);
        Dictionary<int, CustomLevelScore> customLevelDict = null;
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
    /// <summary>
    /// Get CustomLevels that the Student has created
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns>Return CustomLevel object </returns>
    public List<CustomLevel> GetStudentCustomLevel(int studentId)
    {
        string query = String.Format("SELECT * FROM CustomLevel cl WHERE cl.StudentId = {0}", studentId);
        List<CustomLevel> customLevel;
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            customLevel = conn.Query<CustomLevel>(query).ToList();
        }
        return customLevel;
    }
    /// <summary>
    /// Delete selected CustomLevel
    /// </summary>
    /// <param name="customLevelId"></param>
    /// <returns>Return 1 if delete query has executed succesfully</returns>
    public int DeleteCustomLevel(int customLevelId)
    {
        BaseDao<Object> baseDao = new BaseDao<Object>();
        string query = String.Format("DELETE FROM CustomLevel WHERE CustomLevelId = @CustomLevelId", customLevelId);

        int result = baseDao.ExecuteQuery(query, new { CustomLevelId = customLevelId });
        return result;
    }
    /// <summary>
    /// Get Monster that belongs to selected CustomLevel
    /// </summary>
    /// <param name="customLevelId"></param>
    /// <returns>Return Monster object</returns>
    public Monster GetCustomLevelMonster(int customLevelId)
    {
        BaseDao<Monster> baseDao = new BaseDao<Monster>();
        string query = String.Format("SELECT m.MonsterId, m.MonsterName FROM CustomLevel cl NATURAL JOIN Monster m " +
        "WHERE cl.CustomLevelId = {0}", customLevelId);
        Monster monster = baseDao.RetrieveQuery(query);
        return monster;
    }
}
