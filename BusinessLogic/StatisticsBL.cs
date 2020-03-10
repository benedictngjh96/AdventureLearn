using Godot;
using System;
using System.Collections.Generic;
public class StatisticsBL : Node
{
    public bool CheckGoogleExist(string googleId)
    {
        StudentDao studentDao = new StudentDao();
        bool exist = studentDao.CheckGoogleExist(googleId);
        return exist;
    }
    public List<World> GetWorlds()
    {
        WorldDao worldDao = new WorldDao();
        return worldDao.GetWorlds();
    }
    public List<Section> GetSections(int worldId)
    {
        SectionDao sectionDao = new SectionDao();
        return sectionDao.GetWorldSections(worldId);

    }
    public List<Student> GetStudents()
    {
        StudentDao studentDao = new StudentDao();
        return studentDao.GetStudents();
    }
    public List<StudentScore> GetStudentScores(int worldId, int sectionId, int studentId)
    {
        StudentScoreDao studentScoreDao = new StudentScoreDao();
        Student student = studentScoreDao.GetStudentScores(worldId, sectionId, studentId);
        if (student == null)
        {
            return null;
        }
        else
            return student.StudentScore;
    }
    public List<StudentScore> GetAvgSectionScores(int worldId)
    {
        StudentScoreDao studentScoreDao = new StudentScoreDao();
        List<StudentScore> studentScores = studentScoreDao.GetAvgSectionScores(worldId);
        if (studentScores == null)
        {
            return null;
        }
        else
            return studentScores;
    }
    public List<StudentScore> GetAvgWorldScores()
    {
        StudentScoreDao studentScoreDao = new StudentScoreDao();
        List<StudentScore> studentScores = studentScoreDao.GetAvgWorldScores();
        if (studentScores == null)
        {
            return null;
        }
        else
            return studentScores;
    }
    public List<World> GetWorldSections()
    {
        WorldDao worldDao = new WorldDao();
        List<World> worlds = worldDao.GetWorldSections();

        if (worlds == null)
        {
            return null;
        }
        else
            return worlds;
    }
    public List<AssignmentScore> GetAvgAssignmentScore()
    {
        AssignmentScoreDao assignmentScoreDao = new AssignmentScoreDao();
        List<AssignmentScore> assignmentScore = assignmentScoreDao.GetAvgAssignmentScore();
        if (assignmentScore == null)
        {
            return null;
        }
        else
            return assignmentScore;
    }
    public AssignmentScore GetAvgAssignmentScore(int assignmentId)
    {
        AssignmentScoreDao assignmentScoreDao = new AssignmentScoreDao();
        AssignmentScore assignmentScore = assignmentScoreDao.GetAvgAssignmentScore(assignmentId);
        if (assignmentScore == null)
        {
            return null;
        }
        else
            return assignmentScore;
    }
    public AssignmentScore GetMaxAssignmentScore(int assignmentId)
    {
        AssignmentScoreDao assignmentScoreDao = new AssignmentScoreDao();
        AssignmentScore assignmentScore = assignmentScoreDao.GetMaxAssignmentScore(assignmentId);
        if (assignmentScore == null)
        {
            return null;
        }
        else
            return assignmentScore;
    }
    public AssignmentScore GetMinAssignmentScore(int assignmentId)
    {
        AssignmentScoreDao assignmentScoreDao = new AssignmentScoreDao();
        AssignmentScore assignmentScore = assignmentScoreDao.GetMinAssignmentScore(assignmentId);
        if (assignmentScore == null)
        {
            return null;
        }
        else
            return assignmentScore;
    }
    public List<Assignment> GetAssignments()
    {
        AssignmentDao assignmentDao = new AssignmentDao();
        List<Assignment> assignments = assignmentDao.GetAssignments();
        if (assignments == null)
        {
            return null;
        }
        else
            return assignments;
    }
    public List<AssignmentScore> GetStudentAssignmentScores(int studentId)
    {
        AssignmentScoreDao assignmentScoreDao = new AssignmentScoreDao();
        List<AssignmentScore> assignmentScore = assignmentScoreDao.GetStudentAssignmentScores(studentId);
        if (assignmentScore == null)
        {
            return null;
        }
        else
            return assignmentScore;
    }
}
