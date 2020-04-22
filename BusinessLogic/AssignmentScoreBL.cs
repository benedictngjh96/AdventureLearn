using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Class to handle Business Logic for AssignmentScore
/// </summary>
public class AssignmentScoreBL : Node
{
    /// <summary>
    /// Return int result 1 if InsertAssignmentScore is successful
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="assignmentId"></param>
    /// <param name="timeRemaining"></param>
    /// <param name="timeLimit"></param>
    /// <returns></returns>
    public int InsertAssignmentScore(int studentId, int assignmentId, int timeRemaining, int timeLimit)
    {
        AssignmentScoreDao assignmentScoreDao = new AssignmentScoreDao();
        return assignmentScoreDao.InsertAssignmentScore(studentId, assignmentId, Global.CalculateScore(timeRemaining,timeLimit));
    }
    public List<AssignmentScore> GetStudentCompletedAssignment(int studentId){
        AssignmentScoreDao assignmentScoreDao = new AssignmentScoreDao();
        List<AssignmentScore> assignmentList = assignmentScoreDao.GetStudentCompletedAssignment(studentId);
        GD.Print(assignmentList.Count);
        return assignmentList;
    }
}
