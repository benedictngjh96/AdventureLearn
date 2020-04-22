using System;
using Dapper;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

/// <summary>
/// Class to handle DAO operations for AssignmentScore
/// </summary>
public class AssignmentScoreDao
{
    /// <summary>
    /// Insert Student's score of completed Assignment
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="assignmentId"></param>
    /// <param name="assignmentScore"></param>
    /// <returns>Return int 1 if insertion query executed successfully</returns>
    public int InsertAssignmentScore(int studentId, int assignmentId, int assignmentScore)
    {
        BaseDao<Object> baseDao = new BaseDao<Object>();
        string query = "INSERT INTO AssignmentScore (StudentId , AssignmentId ,Score) " +
            "VALUES(@StudentId, @AssignmentId, @Score) " +
            "ON DUPLICATE KEY UPDATE Score = @Score";
        int result = baseDao.ExecuteQuery(query, new { StudentId = studentId, AssignmentId = assignmentId, Score = assignmentScore });
        return result;
    }
    /// <summary>
    /// Get all Student's completed Assignment scores
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns>Return list of AssignmentScore object</returns>
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