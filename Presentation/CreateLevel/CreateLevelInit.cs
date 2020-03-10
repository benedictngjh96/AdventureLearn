using Godot;
using System;

public class CreateLevelInit : Node2D
{
	OptionButton timeLimitBtn;
	LineEdit levelNameLine;
	OptionButton monsterIdBtn;
	Label errorMessageLabel;

	int[] timeLimitOptions;
	int[] monsterIdOptions;

	/// <summary>
	/// Initialization
	/// </summary>
	/// <returns></returns>
	public override void _Ready()
	{
		timeLimitOptions = new int[] { 60, 80, 100, 120 };
		monsterIdOptions = new int[] { 1, 2, 3, 4, 5 };

		timeLimitBtn = GetNode<OptionButton>("TimeLimit");
		levelNameLine = GetNode<LineEdit>("LevelName");
		monsterIdBtn = GetNode<OptionButton>("MonsterId");
		errorMessageLabel = GetNode<Label>("ErrorMessageLabel");

		addOptions();
	}

	/// <summary>
	/// Insert available timelimit and monster options
	/// </summary>
	/// <returns></returns>
	private void addOptions()
	{
		foreach (int i in timeLimitOptions)
			timeLimitBtn.AddItem(i.ToString());

		foreach (int i in monsterIdOptions)
			monsterIdBtn.AddItem(i.ToString());
	}

	/// <summary>
	/// Go to next step of level creation
	/// </summary>
	/// <returns></returns>
	private void _on_NextStepBtn_pressed()
	{
		string levelName = levelNameLine.Text;
		int monsterId = Int32.Parse(monsterIdBtn.Text);
		int timeLimit = Int32.Parse(timeLimitBtn.Text);

		GD.Print("Level Name: " + levelName + "\nMonsterId: " + monsterId + "\nTimeLimit: " + timeLimit);

		Global.AssignmentName = levelName;
		Global.CustomLevelName = levelName;
		Global.MonsterId = monsterId;
		Global.TimeLimit = timeLimit;

		//Global.StudentId = 1; //testing purposes, remember to remove
		GD.Print("\nStudent Id: " + Global.StudentId);

		if (levelName == "")
		{
			GD.Print("Level name field is empty!");
			errorMessageLabel.SetText("Level name field is empty!");
		}
		else
			GetTree().ChangeScene("res://Presentation/CreateLevel/CreateLevel.tscn");
	}

}

