using System;

public class CustomLevelScoreDao
{
    public int InsertCustomLevelScore(string studentId, int customLevelId, int levelScore)
    {
        BaseDao<Object> baseDao = new BaseDao<Object>();
        string query = "INSERT INTO CustomLevelScore (StudentId , CustomLevelId ,LevelScore) " +
            "VALUES(@StudentId, @CustomLevelId, @LevelScore) " +
            "ON DUPLICATE KEY UPDATE LevelScore = @LevelScore";
        int result = baseDao.ExecuteQuery(query, new { StudentId = studentId, CustomLevelId = customLevelId, LevelScore = levelScore });
        return result;
    }
}
