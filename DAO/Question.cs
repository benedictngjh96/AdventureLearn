using Godot;
using System;
/// <summary>
/// DAO Object for Question
/// </summary>
public class Question : Node
{
    public int QuestionId { get; set; }
    public string Option1 { get; set; }
    public string Option2 { get; set; }
    public string Option3 { get; set; }
    public string CorrectOption { get; set; }
    public string QuestionTitle { get; set; }
	public Question(int QuestionId, string Option1, string Option2, string Option3, string CorrectOption, string QuestionTitle)
	{
		this.QuestionId = QuestionId;
		this.Option1 = Option1;
		this.Option2 = Option2;
		this.Option3 = Option3;
		this.CorrectOption = CorrectOption;
		this.QuestionTitle = QuestionTitle;
	}

	public Question(string Option1, string Option2, string Option3, string CorrectOption, string QuestionTitle)
	{
		this.Option1 = Option1;
		this.Option2 = Option2;
		this.Option3 = Option3;
		this.CorrectOption = CorrectOption;
		this.QuestionTitle = QuestionTitle;
	}
	public Question()
	{

	}
}
