using Godot;
using System;

public class UserCreatedQuestion : Node
{
	public int QuestionId { get; set; }
	public string Option1 { get; set; }
	public string Option2 { get; set; }
	public string Option3 { get; set; }
	public string Option4 { get; set; }
	public int CorrectOption { get; set; }
	public string QuestionTitle { get; set; }

	public UserCreatedQuestion(int QuestionId, string Option1, string Option2, string Option3, string Option4, int CorrectOption, string QuestionTitle)
	{
		this.QuestionId = QuestionId;
		this.Option1 = Option1;
		this.Option2 = Option2;
		this.Option3 = Option3;
		this.Option4 = Option4;
		this.CorrectOption = CorrectOption;
		this.QuestionTitle = QuestionTitle;
	}
	public UserCreatedQuestion(int QuestionId)
	{
		this.QuestionId = QuestionId;
	}
}
