using Godot;
using System.Collections.Generic;
using System;

/// <summary>
/// Class to handle Business Logic for CreateLevel
/// </summary>
public class CreateLevelBL : Node
{

	CreateLevelDAO createLevelDAO = new CreateLevelDAO();
	List<UserCreatedQuestion> TempQuestionList = new List<UserCreatedQuestion>();

	/// <summary>
	/// Get the Questions that are saved temporarily
	/// </summary>
	/// <returns>Return the list of Questions that are saved temporarily</returns>
	public List<UserCreatedQuestion> getTempQuestionList()
	{
		return TempQuestionList;
	}

	/// <summary>
	/// Reload the Questions that are save temporarily previously
	/// </summary>
	public void reloadTempQuestionList()
	{
		TempQuestionList = Global.QuestionList;
	}

	/// <summary>
	/// Intialize 5 Question objects
	/// </summary>
	public void initializeQuestions()
	{
		for (int i = 0; i < 5; i++)
			TempQuestionList.Add(new UserCreatedQuestion(i, "", "", "", "", 1, ""));
	}

	/// <summary>
	/// Save current question to a List as temporary storage
	/// </summary>
	/// <param name="questionId"></param>
	/// <param name="option1"></param>
	/// <param name="option2"></param>
	/// <param name="option3"></param>
	/// <param name="option4"></param>
	/// <param name="correctOption"></param>
	/// <param name="questionTitle"></param>
	public void saveQuestion(int questionId, string option1, string option2, string option3, string option4, int correctOption, string questionTitle)
	{
		TempQuestionList[questionId].Option1 = option1;
		TempQuestionList[questionId].Option2 = option2;
		TempQuestionList[questionId].Option3 = option3;
		TempQuestionList[questionId].Option4 = option4;
		TempQuestionList[questionId].CorrectOption = correctOption;
		TempQuestionList[questionId].QuestionTitle = questionTitle;

		listQuestions();
		GD.Print("-----------------------------------------------------");
	}

	/// <summary>
	/// Insert new level and all questions associated with it into database through DAO
	/// </summary>
	/// <param name="levelName"></param>
	/// <param name="monsterId"></param>
	/// <param name="timeLimit"></param>
	public void createLevel(string levelName, int monsterId, int timeLimit)
	{
		//student
		createLevelDAO.InsertCustomLevel(levelName, monsterId, timeLimit);

		//teacher
		//createLevelDAO.InsertAssignment(levelName, monsterId, timeLimit);

		foreach (UserCreatedQuestion q in TempQuestionList)
			createLevelDAO.InsertQuestion(q.Option1, q.Option2, q.Option3, q.Option4, q.CorrectOption, q.QuestionTitle);
	}

	/// <summary>
	/// Get Question object based on question number
	/// </summary>
	/// <param name="questionNumber"></param>
	/// <returns>Return the acquired Question if it exists, else return null if it does not exist</returns>
	public UserCreatedQuestion GetQuestion(int questionNumber)
	{
		return TempQuestionList[questionNumber - 1];
	}

	/// <summary>
	/// Find Question with empty fields
	/// </summary>
	/// <returns>Return the Question Number with empty fields, else return -1 if no empty fields are found</returns>
	public int checkEmptyFieldsExist()
	{
		foreach (UserCreatedQuestion q in TempQuestionList)
		{
			if (q.QuestionTitle == "" || q.Option1 == "" || q.Option2 == "" || q.Option3 == "" || q.Option4 == "")
				return q.QuestionId + 1;
		}
		return -1;
	}

	/// <summary>
	/// Check if LevelName already exists
	/// </summary>
	/// <param name="levelName"></param>
	/// <returns>Return 1 if there are no existing LevelName, else return -1 if there is an existing LevelName</returns>
	public static int checkValidLevelName(string levelName)
	{
		return CreateLevelDAO.checkValidLevelName(levelName);
	}

	/// <summary>
	/// Find question with duplication options
	/// </summary>
	/// <returns>Return the Question Number with duplicate options, else return -1 if no duplicate fields are found</returns>
	public int checkDuplicationOptions()
	{
		foreach (UserCreatedQuestion q in TempQuestionList)
		{
			if (q.Option1 == q.Option2 || q.Option1 == q.Option3 || q.Option1 == q.Option4 || q.Option2 == q.Option3 || q.Option2 == q.Option4
				|| q.Option3 == q.Option4)
				return q.QuestionId + 1;
		}

		return -1;
	}

	/// <summary>
	/// List all questions in List
	/// </summary>
	public void listQuestions()
	{
		foreach (UserCreatedQuestion q in TempQuestionList)
			GD.Print("\nQuestion Id: " + q.QuestionId
				+ "\nQuestion Title: " + q.QuestionTitle
				+ "\nOption 1: " + q.Option1
				+ "\nOption 2: " + q.Option2
				+ "\nOption 3: " + q.Option3
				+ "\nOption 4: " + q.Option4
				+ "\nCorrect Option: Option " + q.CorrectOption);
	}
}
