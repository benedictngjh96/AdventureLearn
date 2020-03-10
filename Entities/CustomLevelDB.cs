using Godot;
using System;

public class CustomLevelDB : Node
{
	public int CustomLevelId { get; set; }
	public int StudentId { get; set; }
	public string CustomLevelName { get; set; }
	public int MonsterId { get; set; }
	public int TimeLimit { get; set; }
	public int PublicLevel { get; set; }

	public CustomLevelDB(int CustomLevelId, int StudentId, string CustomLevelName, int MonsterId, int TimeLimit, int PublicLevel)
	{
		this.CustomLevelId = CustomLevelId;
		this.StudentId = StudentId;
		this.CustomLevelName = CustomLevelName;
		this.MonsterId = MonsterId;
		this.TimeLimit = TimeLimit;
		this.PublicLevel = PublicLevel;
	}

	public CustomLevelDB()
	{

	}
}
