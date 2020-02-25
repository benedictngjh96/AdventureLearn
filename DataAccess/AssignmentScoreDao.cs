using System;

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
        string query = "INSERT INTO AssignmentScore (StudentId , AssignmentId ,AssignmentScore) " +
            "VALUES(@StudentId, @AssignmentId, @AssignmentScore) " +
            "ON DUPLICATE KEY UPDATE AssignmentScore = @AssignmentScore";
        int result = baseDao.ExecuteQuery(query, new { StudentId =studentId, AssignmentId = assignmentId, AssignmentScore = assignmentScore});
        return result;
    }
}
