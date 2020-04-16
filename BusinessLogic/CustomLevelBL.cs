using System;
using System.Collections.Generic;

public class CustomLevelBL
{
    /// <summary>
    /// Return CustomLevel object according to customLevelId
    /// </summary>
    /// <param name="customLevelId"></param>
    /// <returns></returns>
    public CustomLevel GetCustomLevel(int customLevelId)
    {
        CustomLevelDao customLevelDao = new CustomLevelDao();
        return customLevelDao.GetCustomLevel(customLevelId);
    }
    public List<CustomLevel> GetCustomLevels()
    {
        CustomLevelDao customLevelDao = new CustomLevelDao();
        return customLevelDao.GetCustomLevels();
    }
    public List<CustomLevel> GetStudentCustomLevel(int studentId)
    {
        CustomLevelDao customLevelDao = new CustomLevelDao();
        return customLevelDao.GetStudentCustomLevel(studentId);
    }
    public List<CustomLevelScore> GetClearedCustomLevels(int studentId)
    {
        CustomLevelDao customLevelDao = new CustomLevelDao();
        return customLevelDao.GetClearedCustomLevels(studentId);
    }
    public int DeleteCustomLevel(int customLevelId)
    {
        CustomLevelDao customLevelDao = new CustomLevelDao();
        return customLevelDao.DeleteCustomLevel(customLevelId);
    }
    public Monster GetCustomLevelMonster(int customLevelId)
    {
        CustomLevelDao customLevelDao = new CustomLevelDao();
        return customLevelDao.GetCustomLevelMonster(customLevelId);
    }

}
