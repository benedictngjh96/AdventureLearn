using Godot;
using System;

public class CreateLevelDAO : Node
{
    string levelName;
    int monsterId;
    int timeLimit;

    /// <summary>
	/// Insert Question into database
	/// </summary>
    /// <param name="string option1"></param>
    /// <param name="string option2"></param>
    /// <param name="string option3"></param>
    /// <param name="string option4"></param>
    /// <param name="int correctOption"></param>
    /// <param name="string questionTitle"></param>
	/// <returns></returns>
    public void InsertQuestion(string option1, string option2, string option3, string option4, int correctOptionInt, string questionTitle)
    {
        switch (correctOptionInt)
        {
            case 1:
                formatForDatabaseInsertion(ref option4, ref option1);
                break;
            case 2:
                formatForDatabaseInsertion(ref option4, ref option2);
                break;
            case 3:
                formatForDatabaseInsertion(ref option4, ref option3);
                break;
            case 4:
                //formatForDatabaseInsertion(ref option4, ref option4);
                break;
        }

        string query = String.Format("INSERT INTO Question(Option1, Option2, Option3, CorrectOption, QuestionTitle) " +
                 "VALUES('{0}', '{1}', '{2}', '{3}', '{4}')", option1, option2, option3, option4, questionTitle); ;

        BaseDao<Question> baseDao = new BaseDao<Question>();
        int result = baseDao.ExecuteQuery(query);

        if (result <= 0)
            GD.Print("Error inserting question into database.");
        else
            GD.Print("Question inserted into database successfully.");

        //Student
        int questionId = getStudentQuestionId(option1, option2, option3, option4, questionTitle);
        GD.Print("QuestionId : " + questionId);
        int customLevelId = getCustomLevelId(option1, option2, option3, option4, questionTitle);
        GD.Print("CustomLevelId : " + customLevelId);
        InsertStudentCustomQuestion(questionId, customLevelId);

        //Teacher
        /*int questionId = getTeacherQuestionId(option1, option2, option3, option4, questionTitle);
        GD.Print("QuestionId : " + questionId);
        int assignmentId = getAssignmentId(option1, option2, option3, option4, questionTitle);
        GD.Print("AssignmentId : " + assignmentId);
        InsertTeacherCustomQuestion(questionId, assignmentId);*/
    }

   
    /// <summary>
    /// Insert new custom level into database
    /// </summary>
    /// <param name="string levelName"></param>
    /// <param name="int monsterId"></param>
    /// <param name="int timeLimit"></param>
    /// <returns></returns>
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
    /// <returns></returns>
    private void InsertStudentCustomQuestion(int questionId, int customLevelId)
    {
        string query = String.Format("INSERT INTO StudentCustomQuestion(QuestionId, CustomLevelId) " +
            "VALUES({0}, {1});", questionId, customLevelId);

        BaseDao<int> baseDao = new BaseDao<int>();
        int result = baseDao.ExecuteQuery(query);
    }

 

    /// <summary>
    /// Get the id of question created by student
    /// </summary>
    /// <returns></returns>
    private int getStudentQuestionId(string option1, string option2, string option3, string correctOption, string questionTitle)
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
    /// Get the custom level id
    /// </summary>
    /// <returns></returns>
    private int getCustomLevelId(string option1, string option2, string option3, string correctOption, string questionTitle)
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
    /// Format for database insertion
    /// </summary>
    /// <param name="ref string option4"></param>
    /// <param name="ref string correctOption"></param>
    /// <returns></returns>
    private void formatForDatabaseInsertion(ref string option4, ref string correctOption)
    {
        option4 = option4 + correctOption;
        correctOption = option4.Substring(0, (option4.Length - correctOption.Length));
        option4 = option4.Substring(correctOption.Length);
    }

    /// <summary>
    /// Check database for existing level name
    /// Return -1 if there is existing level name, else return 1
    /// </summary>
    /// <param name="string name"></param>
    /// <returns></returns>
    public static int checkValidLevelName(string name)
    {
        //student
        string query = String.Format("SELECT CustomLevelName FROM CustomLevel cl WHERE StudentId = '{0}' AND CustomLevelName = '{1}'; ", Global.StudentId, name);

        //teacher
        //string query = String.Format("SELECT AssignmentName FROM `Assignment` a WHERE TeacherId = {0} AND AssignmentName = '{1}'; ", Global.TeacherId, name);

        BaseDao<string> baseDao = new BaseDao<string>();
        name = baseDao.RetrieveQuery(query);

        if (name == null)
            return 1;
        else
            return -1;
    }
}
