using System;
using Godot;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
public class Global : Node
{
	/// <summary>
	/// Global variables
	/// </summary>
	public static string StudentName { get; set; }
	public static int StudentId { get; set; }
	public static int WorldId { get; set; }
	public static int SectionId { get; set; }
	public static int LevelId { get; set; }
	public static int CustomLevelId { get; set; }
	public static int AssignmentId { get; set; }
	public static string TeacherName { get; set; }
	public static int TeacherId { get; set; }
	public static string CustomLevelName { get; set; }
	public static string AssignmentName { get; set; }
	public static int MonsterId { get; set; }
	public static int TimeLimit { get; set; }
	/// <summary>
	/// Global connection string
	/// </summary>
	public static MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder
	{
		Server = "35.198.238.34",
		UserID = "root",
		Password = "MpiPkr9y04xmg11h",
		Database = "AdventureLearn",
		SslMode = MySqlSslMode.None,
	};
	public static int CalculateScore(int timeRemaining, int timeLimit)
	{
		double levelScore = Convert.ToDouble(timeRemaining) / Convert.ToDouble(timeLimit) * 100;
		return Convert.ToInt32(levelScore);
	}
	public static void SetStudentId(int id)
	{
		StudentId = id;
	}
	public static void SetStudentName(string name)
	{
		StudentName = name;
	}
	public static void LoadSprite(string spritePath, AnimatedSprite animatedSprite, List<string> animationList)
	{
		SpriteFrames spriteFrames = new SpriteFrames();
		foreach (string animation in animationList)
		{
			var dir = new Directory();
			dir.Open(spritePath + animation);

			dir.ListDirBegin();
			var fileName = dir.GetNext();
			//string strFileExtention = System.IO.Path.GetExtension(fileName);
			spriteFrames.AddAnimation(animation);
			int count = 0;

			while (!String.IsNullOrEmpty(fileName))
			{
				//fileName = fileName.Replace(strFileExtention, "");
				var sprite = ResourceLoader.Load(spritePath + animation + "/" + fileName) as Texture;
				spriteFrames.AddFrame(animation, sprite);
				fileName = dir.GetNext();
				//GD.Print(fileName);
				count++;
			}
			animatedSprite.Frames = spriteFrames;
			animatedSprite.SpeedScale = 15;
		}
		animatedSprite.Play("Idle");
	}





}
