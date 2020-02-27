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
    public int InsertAssignmentScore(string studentId, int assignmentId, int assignmentScore)
    {
        BaseDao<Object> baseDao = new BaseDao<Object>();
        string query = "INSERT INTO AssignmentScore (StudentId , AssignmentId ,AssignmentScore) " +
            "VALUES(@StudentId, @AssignmentId, @AssignmentScore) " +
            "ON DUPLICATE KEY UPDATE AssignmentScore = @AssignmentScore";
        int result = baseDao.ExecuteQuery(query, new { StudentId =studentId, AssignmentId = assignmentId, AssignmentScore = assignmentScore});
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
    public List<AssignmentScore> GetAvgAssignmentScore(int assignmentId)
    {
        string query = String.Format("SELECT AssignmentId ,AVG(Score) AS Score FROM AssignmentScore a WHERE a.AssignmentId = {0} " +
        "GROUP BY AssignmentId", assignmentId);
        List<AssignmentScore> assignmentScores;
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            assignmentScores = conn.Query<AssignmentScore>(query).ToList();
        }
        return assignmentScores;
    }
    public List<AssignmentScore> GetStudentAssignmentScores(string studentId)
    {
        string query = String.Format("SELECT * FROM AssignmentScore a WHERE StudentId  = {0}", studentId);
        List<AssignmentScore> assignmentScores;
        using (MySqlConnection conn = new MySqlConnection(Global.csb.ConnectionString))
        {
            assignmentScores = conn.Query<AssignmentScore>(query).ToList();
        }
        return assignmentScores;
    }
}
