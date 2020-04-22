using System;
using System.Collections.Generic;

/// <summary>
/// Class to handle Business Logic for Assignment
/// </summary>
public class AssignmentBL
{
    /// <summary>
    /// Returns Assignment object according to assignmentId
    /// </summary>
    /// <param name="assignmentId"></param>
    /// <returns></returns>
    public Assignment GetAssignment(int assignmentId)
    {
        AssignmentDao assignmentDao = new AssignmentDao();
        return assignmentDao.GetAssignment(assignmentId);
    }
    public List<PublishedAssignment> GetStudentAssignment(int studentId)
    {
        AssignmentDao assignmentDao = new AssignmentDao();
        return assignmentDao.GetStudentAssignment(studentId);
    }
    public Monster GetAssignmentMonster(int assignmentId)
    {
        AssignmentDao assignmentDao = new AssignmentDao();
        return assignmentDao.GetAssignmentMonster(assignmentId);
    }

}
