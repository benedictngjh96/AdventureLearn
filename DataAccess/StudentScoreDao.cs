using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

public class StudentScoreDao
{
    /// <summary>
    /// Get all Student scores in selected World and Section
    /// </summary>
    /// <param name="sectionId"></param>
    /// <param name="studentId"></param>
    /// <returns>Return Student object containing StudentScore object</returns>
    public Student GetStudentScores(int worldId, int sectionId, int studentId)
    {
        string query = String.Format("SELECT s.StudentId, ss.WorldId , ss.SectionId , ss.LevelId , " +
        "ss.LevelScore FROM Student s INNER JOIN StudentScore  ss ON s.StudentId  = ss.StudentId WHERE ss.WorldId = {0} AND " +
        "ss.SectionId = {1} AND s.StudentId = {2}", worldId, sectionId, studentId);
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            var lookup = new Dictionary<int, Student>();
            conn.Query<Student, StudentScore, Student>(query, (s, ss) =>
            {
                Student student;
                if (!lookup.TryGetValue(s.StudentId, out student))
                    lookup.Add(s.StudentId, student = s);
                if (student.StudentScore == null)
                    student.StudentScore = new List<StudentScore>();
                student.StudentScore.Add(ss); /* Add locations to course */
                return student;
            }, splitOn: "StudentId, WorldId, SectionId, LevelId").AsQueryable();
            Student stud = new Student();
            if (lookup.Count() == 0)
                return null;

            stud = lookup.ElementAt(0).Value;
            return stud;
        }
    }
    /// <summary>
    /// Get Student's average score in all Worlds
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns>Return list of StudentScore object</returns>
    public StudentScore GetAvgWorldScores(int studentId)
    {
        string query = String.Format("SELECT AVG(LevelScore) AS LevelScore , WorldId FROM StudentScore ss WHERE ss.StudentId = {0} GROUP BY ss.WorldId", studentId);
        List<StudentScore> studentScores;
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            studentScores = conn.Query<StudentScore>(query).ToList();
        }
        if (studentScores.Count <= 0)
            return null;
        else
            return studentScores[0];
    }
    /// <summary>
    /// Insert StudentScore of cleared level
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="worldId"></param>
    /// <param name="sectionId"></param>
    /// <param name="levelId"></param>
    /// <param name="levelScore"></param>
    /// <returns>Return int result 1 if insertion query has executed successfully</returns>
    public int InsertStudentScore(int studentId, int worldId, int sectionId, int levelId, int levelScore)
    {
        BaseDao<int> baseDao = new BaseDao<int>();
        string query = String.Format("INSERT INTO StudentScore (StudentId, WorldId, SectionId, LevelId, LevelScore) " +
            "VALUES (@StudentId, @WorldId, @SectionId, @LevelId, @LevelScore) ON DUPLICATE KEY UPDATE LevelScore = @LevelScore",
            studentId, worldId, sectionId, levelId, levelScore);
        int result = baseDao.ExecuteQuery(query, new { StudentId = studentId, WorldId = worldId, SectionId = sectionId, LevelId = levelId, LevelScore = levelScore });
        return result;
    }
    /// <summary>
    /// Get Student's campaign rank
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns>Return integer result of Student's campaign rank</returns>
    public int GetCampaignRanking(int studentId)
    {
        BaseDao<int> baseDao = new BaseDao<int>();
        string query = "SELECT (COUNT(0)+1) AS StudentRank FROM StudentLevelTotalScore s1, StudentLevelTotalScore s2 "+
        "WHERE s1.StudentId = @StudentId AND s2.totalLevelScore > s1.totalLevelScore";
        int result = baseDao.ExecuteScalar(query, new { StudentId = studentId });
        return result;
    }
  
}
