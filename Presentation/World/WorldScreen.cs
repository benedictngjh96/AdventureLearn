using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class WorldScreen : Node
{
	TextureButton world1Btn, world2Btn, world3Btn; 
	WorldBL worldBl;

	public override void _Ready()
	{
		//Testing
		//Global.StudentId = 29;
		//Testing

		world1Btn = GetNode<TextureButton>("Bg/World1");
		world2Btn = GetNode<TextureButton>("Bg/World2");
		world3Btn = GetNode<TextureButton>("Bg/World3");

		worldBl = new WorldBL();

		disableInaccessibleWorlds();

		GD.Print("Start injecting data");

		//QuestionId, WorldId, SectionId, LevelId
		//INSERT INTO CampaignQuestion VALUES(1, 1, 1, 1), (2, 2, 2, 2);

		string query = "INSERT INTO CampaignQuestion VALUES ";

		int w = 3;
		int s = 3;
		int l = 5;
		int q = 225;
		int e = 1;

		for(int a = 1; a <= w ; a++)
		{
			for(int b = 1; b <= s; b++)
			{
				for (int c = 1; c <= l; c++)
				{
					for(int i = 1; i <= 5; i++)
					{
						query += String.Format("({0}, {1}, {2}, {3}),", e, a, b, c);
						e++;
					}
						
					
				}
			}
		}
		string newQ = query.TrimEnd(',');
		newQ += ";";



		GD.Print(newQ);

		BaseDao<int> baseDao = new BaseDao<int>();
		int result = baseDao.ExecuteQuery(newQ);

		GD.Print("No crash super good");
	}

	private void disableInaccessibleWorlds()
	{

		int completedWorldCount = worldBl.getCompletedWorldCount();
		int totalWorldCount = worldBl.getTotalWorldCount();
		GD.Print("Number of Completed Worlds: " + completedWorldCount);
		GD.Print("Number of Total Worlds: " + totalWorldCount);

		for (int i = totalWorldCount; i > completedWorldCount + 1 ; i--)
		{
			TextureButton disabler = GetNode<TextureButton>("Bg/World" + i);
			disabler.Disabled = true;
		}
	}

	private void _on_World1_pressed()
	{
		Global.WorldId = 1;
		GetTree().ChangeScene("res://Presentation/LevelSelection/LevelSelection.tscn");
	}

	private void _on_World2_pressed()
	{
		Global.WorldId = 2;
		GetTree().ChangeScene("res://Presentation/LevelSelection/LevelSelection.tscn");
	}

	private void _on_World3_pressed()
	{
		Global.WorldId = 3;
		GetTree().ChangeScene("res://Presentation/LevelSelection/LevelSelection.tscn");
	}
}





