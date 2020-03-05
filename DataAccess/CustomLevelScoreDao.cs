using System;

public class CustomLevelScoreDao
{
    public int InsertCustomLevelScore(int studentId, int customLevelId, int levelScore)
    {
        BaseDao<Object> baseDao = new BaseDao<Object>();
        string query = "INSERT INTO CustomLevelScore (StudentId , CustomLevelId ,LevelScore) " +
            "VALUES(@StudentId, @CustomLevelId, @LevelScore) " +
            "ON DUPLICATE KEY UPDATE LevelScore = @LevelScore";
        Godot.GD.Print(query);
        Godot.GD.Print(studentId);
        Godot.GD.Print(customLevelId);
        Godot.GD.Print(levelScore);
        int result = baseDao.ExecuteQuery(query, new { StudentId = studentId, CustomLevelId = customLevelId, LevelScore = levelScore });
        return result;
    }
}
