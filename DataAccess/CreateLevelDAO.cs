using Godot;
using System;

/// <summary>
/// Class to handle DAO operations for CreateLevel
/// </summary>
public class CreateLevelDAO : Node
{
    string levelName;
    int monsterId;
    int timeLimit;

    /// <summary>
    /// Insert Question into database
    /// </summary>
    /// <param name="option1"></param>
    /// <param name="option2"></param>
    /// <param name="option3"></param>
    /// <param name="option4"></param>
    /// <param name="correctOptionInt"></param>
    /// <param name="questionTitle"></param>
    public void InsertQuestion(string option1, string option2, string option3, string option4, int correctOptionInt, string questionTitle)
    {
        switch (correctOptionInt)
        {
            case 1:
                FormatForDatabaseInsertion(ref option4, ref option1);
                break;
            case 2:
                FormatForDatabaseInsertion(ref option4, ref option2);
                break;
            case 3:
                FormatForDatabaseInsertion(ref option4, ref option3);
                break;
            default:
                break;
        }

        string query = String.Format("INSERT INTO Question(Option1, Option2, Option3, CorrectOption, QuestionTitle) " +
                 "VALUES('{0}', '{1}', '{2}', '{3}', '{4}')", option1, option2, option3, option4, questionTitle); ;
        BaseDao<Question> baseDao = new BaseDao<Question>();
        int result = baseDao.ExecuteQuery(query);
        int questionId = GetStudentQuestionId(option1, option2, option3, option4, questionTitle);
        int customLevelId = GetCustomLevelId(option1, option2, option3, option4, questionTitle);
        InsertStudentCustomQuestion(questionId, customLevelId);
    }

    /// <summary>
    /// Insert new custom level into database
    /// </summary>
    /// <param name="levelName"></param>
    /// <param name="monsterId"></param>
    /// <param name="timeLimit"></param>
    public void InsertCustomLevel(string levelName, int monsterId, int timeLimit)
    {
        this.levelName = levelName;
        this.monsterId = monsterId;
        this.timeLimit = timeLimit;

        string query = String.Format("INSERT INTO CustomLevel(StudentId, CustomLevelName, MonsterId, TimeLimit) " +
            "VALUES({0}, '{1}', {2}, {3});", Global.StudentId, levelName, monsterId, timeLimit);

        BaseDao<int> baseDao = new BaseDao<int>();
        int result = baseDao.ExecuteQuery(query);
    }

    /// <summary>
    /// Link questionId and customLevelId in database
    /// </summary>
    /// <param name="questionId"></param>
    /// <param name="customLevelId"></param>
    private void InsertStudentCustomQuestion(int questionId, int customLevelId)
    {
        string query = String.Format("INSERT INTO StudentCustomQuestion(QuestionId, CustomLevelId) " +
            "VALUES({0}, {1});", questionId, customLevelId);

        BaseDao<int> baseDao = new BaseDao<int>();
        int result = baseDao.ExecuteQuery(query);
    }

    /// <summary>
    /// Get the QuestionId of the question created by student
    /// </summary>
    /// <param name="option1"></param>
    /// <param name="option2"></param>
    /// <param name="option3"></param>
    /// <param name="correctOption"></param>
    /// <param name="questionTitle"></param>
    /// <returns>Return the acquired QuestionId </returns>
    private int GetStudentQuestionId(string option1, string option2, string option3, string correctOption, string questionTitle)
    {
        //student
        string query = String.Format(
            "SELECT DISTINCT QuestionId FROM Question NATURAL JOIN CustomLevel " +
            "WHERE StudentId = {0} AND CustomLevelName = '{1}' AND QuestionTitle = '{2}' " +
            "AND Option1 = '{3}' AND Option2 = '{4}' AND Option3 = '{5}' AND CorrectOption = '{6}' " +
            "AND MonsterId = {7} AND TimeLimit = {8};",
            Global.StudentId, levelName, questionTitle, option1,
            option2, option3, correctOption, monsterId, timeLimit);

        BaseDao<int> baseDao = new BaseDao<int>();
        int questionId = baseDao.RetrieveQuery(query);
        return questionId;
    }

    /// <summary>
    /// Get the CustomLevelId
    /// </summary>
    /// <param name="option1"></param>
    /// <param name="option2"></param>
    /// <param name="option3"></param>
    /// <param name="correctOption"></param>
    /// <param name="questionTitle"></param>
    /// <returns> Return the acquired CustomLevelId</returns>
    private int GetCustomLevelId(string option1, string option2, string option3, string correctOption, string questionTitle)
    {
        string query = String.Format(
            "SELECT DISTINCT CustomLevelId FROM Question NATURAL JOIN CustomLevel " +
            "WHERE StudentId = {0} AND CustomLevelName = '{1}' AND QuestionTitle = '{2}' " +
            "AND Option1 = '{3}' AND Option2 = '{4}' AND Option3 = '{5}' AND CorrectOption = '{6}' " +
            "AND MonsterId = {7} AND TimeLimit = {8};",
            Global.StudentId, levelName, questionTitle, option1,
            option2, option3, correctOption, monsterId, timeLimit);

        BaseDao<int> baseDao = new BaseDao<int>();
        int customLevelId = baseDao.RetrieveQuery(query);
        return customLevelId;
    }

    /// <summary>
    /// Format the Question for database insertion
    /// </summary>
    /// <param name="option4"></param>
    /// <param name="correctOption"></param>
    private void FormatForDatabaseInsertion(ref string option4, ref string correctOption)
    {
        option4 = option4 + correctOption;
        correctOption = option4.Substring(0, (option4.Length - correctOption.Length));
        option4 = option4.Substring(correctOption.Length);
    }

    /// <summary>
    /// Check database for existing Level Name
    /// </summary>
    /// <param name="name"></param>
    /// <returns>Return -1 if there is existing level name, else return 1</returns>
    public static int CheckValidLevelName(string name)
    {
        string query = String.Format("SELECT CustomLevelName FROM CustomLevel cl WHERE StudentId = '{0}' AND CustomLevelName = '{1}'; ", Global.StudentId, name);
        BaseDao<string> baseDao = new BaseDao<string>();
        name = baseDao.RetrieveQuery(query);

        if (name == null)
            return 1;
        else
            return -1;
    }
}
