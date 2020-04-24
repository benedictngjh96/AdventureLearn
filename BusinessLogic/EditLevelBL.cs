using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Class to handle Business Logic for EditLevel
/// </summary>
public class EditLevelBL : Node
{
	EditLevelDaoImpl editLevelDao = new EditLevelDaoImpl();
	List<UserCreatedQuestion> TempQuestionList = new List<UserCreatedQuestion>();
	List<UserCreatedQuestion> OriginalQuestionList = new List<UserCreatedQuestion>();

	/// <summary>
	/// Get the Questions that are saved temporarily
	/// </summary>
	/// <returns>Return the list of Questions that are saved temporarily</returns>
	public List<UserCreatedQuestion> GetTempQuestionList()
	{
		return TempQuestionList;
	}

	/// <summary>
	/// Get the orignal Questions that have not been edited
	/// </summary>
	/// <returns>Return the original Questions in a List</returns>
	public List<UserCreatedQuestion> GetOrignalQuestionList()
	{
		return OriginalQuestionList;
	}

	/// <summary>
	/// Reload the Questions that are save temporarily previously
	/// </summary>
	public void ReloadTempQuestionList()
	{
		TempQuestionList = Global.QuestionList;
	}

	/// <summary>
	/// Save current Question to a List as temporary storage
	/// </summary>
	/// <param name="questionId"></param>
	/// <param name="option1"></param>
	/// <param name="option2"></param>
	/// <param name="option3"></param>
	/// <param name="option4"></param>
	/// <param name="correctOption"></param>
	/// <param name="questionTitle"></param>
	public void SaveQuestion(int questionId, string option1, string option2, string option3, string option4, int correctOption, string questionTitle)
	{
		TempQuestionList[questionId].Option1 = option1;
		TempQuestionList[questionId].Option2 = option2;
		TempQuestionList[questionId].Option3 = option3;
		TempQuestionList[questionId].Option4 = option4;
		TempQuestionList[questionId].CorrectOption = correctOption;
		TempQuestionList[questionId].QuestionTitle = questionTitle;

		//ListQuestions();
		//GD.Print("-----------------------------------------------------");
	}

	/// <summary>
	/// Update questions in the selected CustomLevel into database through DAO
	/// </summary>
	public void UpdateLevel()
	{
		foreach (UserCreatedQuestion q in TempQuestionList)
			editLevelDao.UpdateQuestion(q.Option1, q.Option2, q.Option3, q.Option4, q.CorrectOption, q.QuestionTitle, q.QuestionId);
	}

	/// <summary>
	/// List all questions in the List that is used as temporary storage
	/// </summary>
	private void ListQuestions()
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
	/// Check if LevelName already exists
	/// </summary>
	/// <param name="oldName"></param>
	/// <param name="newName"></param>
	/// <returns>Return 1 if there are no existing LevelName, else return -1 if there is an existing LevelName</returns>
	public static int CheckValidLevelName(string oldName, string newName)
	{
		return EditLevelDaoImpl.CheckValidLevelName(oldName, newName);
	}

	/// <summary>
	/// Get Question object based on Question Number
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
	public int CheckEmptyFieldsExist()
	{
		foreach (UserCreatedQuestion q in TempQuestionList)
		{
			if (q.QuestionTitle == "" || q.Option1 == "" || q.Option2 == "" || q.Option3 == "" || q.Option4 == "")
				return q.QuestionId + 1;
		}
		return -1;
	}

	/// <summary>
	/// Load selected CustomLevel information from DAO
	/// </summary>
	/// <returns>Return the acquired information in a CustomLevel object</returns>
	public CustomLevel LoadCustomLevelInfo()
	{
		int i = 0;

		CustomLevel levelInfo = editLevelDao.GetLevelInfo();
		//GD.Print(levelInfo.Question.Count);

		List<Question> levelQuestions = levelInfo.Question;
		foreach (Question q in levelQuestions)
		{
			TempQuestionList.Add(new UserCreatedQuestion(i, q.Option1, q.Option2, q.Option3, q.CorrectOption, 4, q.QuestionTitle));
			OriginalQuestionList.Add(new UserCreatedQuestion(i, q.Option1, q.Option2, q.Option3, q.CorrectOption, 4, q.QuestionTitle));
			i++;
		}
		return levelInfo;
	}

	/// <summary>
	/// Find question with duplication options
	/// </summary>
	/// <returns>Return the Question Number with duplicate options, else return -1 if no duplicate fields are found</returns>
	public int CheckDuplicationOptions()
	{
		foreach(UserCreatedQuestion q in TempQuestionList)
		{
			if (q.Option1 == q.Option2 || q.Option1 == q.Option3 || q.Option1 == q.Option4 || q.Option2 == q.Option3 || q.Option2 == q.Option4
				|| q.Option3 == q.Option4)
				return q.QuestionId + 1;
		}

		return -1;
	}

	/// <summary>
	/// Updates the LevelName, Monster, and TimeLimit
	/// </summary>
	/// <param name="levelName"></param>
	/// <param name="monsterId"></param>
	/// <param name="timeLimit"></param>
	public void UpdateLevelInitInfo(string levelName, int monsterId, int timeLimit)
	{
		editLevelDao.UpdateLevelInitInfo(levelName, monsterId, timeLimit);
	}
}
