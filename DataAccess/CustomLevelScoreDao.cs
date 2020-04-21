using System;

public class CustomLevelScoreDao
{
    /// <summary>
    /// Insert Student's CustomLevelScore of cleared CustomLevel
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="customLevelId"></param>
    /// <param name="levelScore"></param>
    /// <return>Return 1 if query has executed successfully</returns>
    public int InsertCustomLevelScore(int studentId, int customLevelId, int levelScore)
    {
        BaseDao<Object> baseDao = new BaseDao<Object>();
        string query = "INSERT INTO CustomLevelScore (StudentId , CustomLevelId ,LevelScore) " +
            "VALUES(@StudentId, @CustomLevelId, @LevelScore) " +
            "ON DUPLICATE KEY UPDATE LevelScore = @LevelScore";
        int result = baseDao.ExecuteQuery(query, new { StudentId = studentId, CustomLevelId = customLevelId, LevelScore = levelScore });
        return result;
    }
}
