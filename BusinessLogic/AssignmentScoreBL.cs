using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Class to handle Business Logic for AssignmentScore
/// </summary>
public class AssignmentScoreBL : Node
{
    /// <summary>
    /// Insert Student's score of completed Assignment
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="assignmentId"></param>
    /// <param name="timeRemaining"></param>
    /// <param name="timeLimit"></param>
    /// <returns>Return int result 1 if InsertAssignmentScore has executed successfully</returns>
    public int InsertAssignmentScore(int studentId, int assignmentId, int timeRemaining, int timeLimit)
    {
        AssignmentScoreDaoImpl assignmentScoreDao = new AssignmentScoreDaoImpl();
        return assignmentScoreDao.InsertAssignmentScore(studentId, assignmentId, Global.CalculateScore(timeRemaining,timeLimit));
    }
    /// <summary>
    /// Get all AssignmentScores of Student's completed assignments
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns>/Return list of AssignmentScore object</returns>
    public List<AssignmentScore> GetStudentCompletedAssignment(int studentId){
        AssignmentScoreDaoImpl assignmentScoreDao = new AssignmentScoreDaoImpl();
        List<AssignmentScore> assignmentList = assignmentScoreDao.GetStudentCompletedAssignment(studentId);
        return assignmentList;
    }
}
