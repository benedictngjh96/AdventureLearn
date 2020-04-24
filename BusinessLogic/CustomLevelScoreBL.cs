using Godot;
using System;

/// <summary>
/// Class to handle Business Logic for CustomLevelScore
/// </summary>
public class CustomLevelScoreBL : Node
{
    /// <summary>
    /// Insert score of cleared CustomLevel
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="customLevelId"></param>
    /// <param name="timeRemaining"></param>
    /// <param name="timeLimit"></param>
    /// <returns>Return 1 if insert query has executed succcessfully</returns>
    public int InsertCustomLevelScore(int studentId, int customLevelId, int timeRemaining, int timeLimit)
    {
        CustomLevelScoreDaoImpl customlevelScoreDao = new CustomLevelScoreDaoImpl();
        return customlevelScoreDao.InsertCustomLevelScore(studentId, customLevelId, Global.CalculateScore(timeRemaining, timeLimit));
    }
}
