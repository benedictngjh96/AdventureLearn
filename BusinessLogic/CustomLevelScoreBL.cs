using Godot;
using System;

public class CustomLevelScoreBL : Node
{
    public int InsertCustomLevelScore(int studentId, int customLevelId, int timeRemaining, int timeLimit)
    {
        CustomLevelScoreDao customlevelScoreDao = new CustomLevelScoreDao();
        return customlevelScoreDao.InsertCustomLevelScore(studentId, customLevelId, Global.CalculateScore(timeRemaining, timeLimit));
    }
}
