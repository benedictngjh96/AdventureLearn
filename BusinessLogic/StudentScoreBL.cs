using System;
using System.Collections.Generic;

/// <summary>
/// Class to handle Business Logic for StudentScore
/// </summary>
public class StudentScoreBL
{
    StudentScoreDaoImpl studentScoreDao = new StudentScoreDaoImpl();

    /// <summary>
    /// Get all Student scores in selected World and Section
    /// </summary>
    /// <param name="sectionId"></param>
    /// <param name="studentId"></param>
    /// <returns>Return Student object containing StudentScore object</returns>
    public Student GetStudentScores(int worldId, int sectionId, int studentId)
    {
        Student student = studentScoreDao.GetStudentScores(worldId, sectionId, studentId);
        return student;
    }

    /// <summary>
    /// Insert StudentScore of cleared level
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="worldId"></param>
    /// <param name="sectionId"></param>
    /// <param name="levelId"></param>
    /// <param name="timeRemaining"></param>
    /// <param name="timeLimit"></param>
    /// <returns>Return int result 1 if insertion query has executed successfully</returns>
    public int InsertStudentScore(int studentId, int worldId, int sectionId, int levelId, int timeRemaining, int timeLimit)
    {
        StudentScoreDaoImpl studentScoreDao = new StudentScoreDaoImpl();
        return studentScoreDao.InsertStudentScore(studentId, worldId, sectionId, levelId, Global.CalculateScore(timeRemaining, timeLimit));
    }
    /// <summary>
    /// Get Student's average score in all Worlds
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns>Return list of StudentScore object</returns>
    public StudentScore GetAvgWorldScores(int studentId)
    {
        StudentScoreDaoImpl studentScoreDao = new StudentScoreDaoImpl();
        return studentScoreDao.GetAvgWorldScores(studentId);
    }
    /// <summary>
    /// Get Student's campaign rank
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns>Return integer result of Student's campaign rank</returns>
    public int GetCampaignRanking(int studentId)
    {
        StudentScoreDaoImpl studentScoreDao = new StudentScoreDaoImpl();
        return studentScoreDao.GetCampaignRanking(studentId);
    }
    

}
