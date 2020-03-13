using Godot;
using System;
using System.Collections.Generic;

public class EditLevelBL : Node
{
	EditLevelDao editLevelDao = new EditLevelDao();
	List<UserCreatedQuestion> TempQuestionList = new List<UserCreatedQuestion>();
		
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
	/// Update questions in the selected custom level
	/// </summary>
	/// <param name="int questionNumber"></param>
	/// <returns></returns>
	public void updateLevel()
	{
		foreach (UserCreatedQuestion q in TempQuestionList)
			editLevelDao.updateQuestion(q.Option1, q.Option2, q.Option3, q.Option4, q.CorrectOption, q.QuestionTitle, q.QuestionId);
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

	/// <summary>
	/// Check if levelName already exists
	/// Return -1 if there is existing level name, else return 1
	/// </summary>
	/// <param name="string oldName"></param>
	/// <param name="string newName"></param>
	/// <returns></returns>
	public static int checkValidLevelName(string oldName, string newName)
	{
		return EditLevelDao.checkValidLevelName(oldName, newName);
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
	/// Load selected custom level information
	/// </summary>
	/// <returns></returns>
	public CustomLevel loadCustomLevelInfo()
	{
		int i = 0;

		CustomLevel levelInfo = editLevelDao.getLevelInfo();
		//GD.Print(levelInfo.Question.Count);

		List<Question> levelQuestions = levelInfo.Question;
		foreach (Question q in levelQuestions)
		{
			TempQuestionList.Add(new UserCreatedQuestion(i, q.Option1, q.Option2, q.Option3, q.CorrectOption, 4, q.QuestionTitle));
			i++;
		}

		/*GD.Print("\nCustom Level Id: " + levelInfo.CustomLevelId + "\nMonster Id: " + levelInfo.Monster.MonsterId + 
			"\nTimeLimit: " + levelInfo.TimeLimit);*/

		/*foreach (Question q in levelQuestions)
		{
			GD.Print("Question id: " + q.QuestionId + "\nQuestionTitle: " + q.QuestionTitle
				+ "\nOption1: " + q.Option1 + "\nOption2: " + q.Option2 + "\nOption3:" + q.Option3 + "\nCorrectOption: " + q.CorrectOption + "\n");
		}*/

		//listQuestions();

		return levelInfo;
	}

	/// <summary>
	/// Find question with duplication options
	/// Return -1 if no empty fields are found
	/// </summary>
	/// <returns></returns>
	public int checkDuplicationOptions()
	{
		foreach(UserCreatedQuestion q in TempQuestionList)
		{
			if (q.Option1 == q.Option2 || q.Option1 == q.Option3 || q.Option1 == q.Option4 || q.Option2 == q.Option3 || q.Option2 == q.Option4
				|| q.Option3 == q.Option4)
				return q.QuestionId + 1;
		}

		return -1;
	}
}
