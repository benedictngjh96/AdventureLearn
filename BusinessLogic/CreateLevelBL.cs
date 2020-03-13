using Godot;
using System.Collections.Generic;
using System;

public class CreateLevelBL : Node
{
	CreateLevelDAO createLevelDAO = new CreateLevelDAO();
	List<UserCreatedQuestion> TempQuestionList = new List<UserCreatedQuestion>();

	/// <summary>
	/// Intialize 5 questions
	/// </summary>
	/// <returns></returns>
	public void initializeQuestions()
	{
		for (int i = 0; i < 5; i++)
			TempQuestionList.Add(new UserCreatedQuestion(i, "", "", "", "", 1, ""));
	}

	/// <summary>
	/// Save current question to List
	/// </summary>
	/// <param name="int questionId"></param>
	/// <param name="string option1"></param>
	/// <param name="string option2"></param>
	/// <param name="string option3"></param>
	/// <param name="string option4"></param>
	/// <param name="int correctOption"></param>
	/// <param name="string questionTitle"></param>
	/// <returns></returns>
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
	/// Insert new level and all questions associated with it into database
	/// </summary>
	/// <param name="int questionNumber"></param>
	/// <returns></returns>
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
	/// Return null if Question object dont exist
	/// </summary>
	/// <param name="int questionNumber"></param>
	/// <returns></returns>
	public UserCreatedQuestion GetQuestion(int questionNumber)
	{
		return TempQuestionList[questionNumber - 1];
	}

	/// <summary>
	/// Find question with empty fields and return its question number
	/// Return -1 if no empty fields are found
	/// </summary>
	/// <returns></returns>
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
	/// Check if levelName already exists
	/// Return -1 if there is existing level name, else return 1
	/// </summary>
	/// <returns></returns>
	public static int checkValidLevelName(string levelName)
	{
		return CreateLevelDAO.checkValidLevelName(levelName);
	}

	/// <summary>
	/// List all questions in List
	/// </summary>
	/// <returns></returns>
	private void listQuestions()
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
