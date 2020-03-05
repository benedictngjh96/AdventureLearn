using System;
using Dapper;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
public class AssignmentScoreDao
{
    /// <summary>
    /// Return int 1 if InsertAssignmentScore is successful
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="assignmentId"></param>
    /// <param name="assignmentScore"></param>
    /// <returns></returns>
    public int InsertAssignmentScore(int studentId, int assignmentId, int assignmentScore)
    {
        BaseDao<Object> baseDao = new BaseDao<Object>();
        string query = "INSERT INTO AssignmentScore (StudentId , AssignmentId ,Score) " +
            "VALUES(@StudentId, @AssignmentId, @Score) " +
            "ON DUPLICATE KEY UPDATE Score = @Score";
        int result = baseDao.ExecuteQuery(query, new { StudentId = studentId, AssignmentId = assignmentId, Score = assignmentScore });
        return result;
    }
    public List<AssignmentScore> GetAvgAssignmentScore()
    {
        string query = "SELECT AssignmentId ,AVG(Score) AS Score FROM AssignmentScore a GROUP BY AssignmentId";
        List<AssignmentScore> assignmentScores;
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            assignmentScores = conn.Query<AssignmentScore>(query).ToList();
        }
        return assignmentScores;
    }
    public AssignmentScore GetAvgAssignmentScore(int assignmentId)
    {
        BaseDao<AssignmentScore> baseDao = new BaseDao<AssignmentScore>();
        string query = String.Format("SELECT AssignmentId ,AVG(Score) AS Score FROM AssignmentScore a WHERE a.AssignmentId = {0} " +
        "GROUP BY AssignmentId", assignmentId);
        AssignmentScore assignmentScore = baseDao.RetrieveQuery(query);
        return assignmentScore;
    }
    public AssignmentScore GetMaxAssignmentScore(int assignmentId)
    {
        BaseDao<AssignmentScore> baseDao = new BaseDao<AssignmentScore>();
        string query = String.Format("SELECT AssignmentId ,MAX(Score) AS Score FROM AssignmentScore a WHERE a.AssignmentId = {0} " +
        "GROUP BY AssignmentId", assignmentId);
        AssignmentScore assignmentScore = baseDao.RetrieveQuery(query);
        return assignmentScore;
    }

    public AssignmentScore GetMinAssignmentScore(int assignmentId)
    {
        BaseDao<AssignmentScore> baseDao = new BaseDao<AssignmentScore>();
        string query = String.Format("SELECT AssignmentId , MIN(Score) AS Score FROM AssignmentScore a WHERE a.AssignmentId = {0} " +
        "GROUP BY AssignmentId", assignmentId);
        AssignmentScore assignmentScore = baseDao.RetrieveQuery(query);
        return assignmentScore;
    }
    public List<AssignmentScore> GetStudentAssignmentScores(int studentId)
    {
        string query = String.Format("SELECT * FROM AssignmentScore a WHERE StudentId  = {0}", studentId);
        List<AssignmentScore> assignmentScores;
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            assignmentScores = conn.Query<AssignmentScore>(query).ToList();
        }
        return assignmentScores;
    }
    public List<AssignmentScore> GetStudentCompletedAssignment(int studentId)
    {
        string query = String.Format("SELECT Score,AssignmentId ,DueDate,  TeacherId ,TeacherName , AssignmentId , AssignmentName " +
        "FROM PublishedAssignment pa NATURAL JOIN AssignmentScore NATURAL JOIN `Assignment` NATURAL JOIN Teacher WHERE StudentId = {0}", studentId);
        int count = 0;
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            var lookup = new Dictionary<int, AssignmentScore>();


            conn.Query<AssignmentScore, PublishedAssignment, Teacher, Assignment, AssignmentScore>(query, (as2, pa, t, a) =>
            {
                AssignmentScore assignmentScore;
                if (!lookup.TryGetValue(count, out assignmentScore))
                {
                    lookup.Add(count, assignmentScore = as2);
                }
                assignmentScore.PublishedAssignment = pa;
                assignmentScore.PublishedAssignment.Assignment = a;
                assignmentScore.PublishedAssignment.Assignment.Teacher = t;
                count++;
                return assignmentScore;
            }, splitOn: "AssignmentId ,TeacherId, AssignmentId").AsQueryable();

            List<AssignmentScore> assignmentList = new List<AssignmentScore>();
            assignmentList.AddRange(lookup.Values);

            return assignmentList;
        }

    }
}