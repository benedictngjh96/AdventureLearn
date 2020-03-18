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
		Global.StudentId = 27;
		//Testing

		world1Btn = GetNode<TextureButton>("World1");
		world2Btn = GetNode<TextureButton>("World2");
		world3Btn = GetNode<TextureButton>("World3");

		worldBl = new WorldBL();

		disableInaccessibleWorlds();
	}

	private void disableInaccessibleWorlds()
	{

		int completedWorldCount = worldBl.getCompletedWorldCount();
		int totalWorldCount = worldBl.getTotalWorldCount();
		GD.Print("Number of Completed Worlds: " + completedWorldCount);
		GD.Print("Number of Total Worlds: " + totalWorldCount);

		int unavailableWorldCount = totalWorldCount - (completedWorldCount + 1);  //(completedWorldCount + 1) = totalAvailableWorld
		for (int i = 0; i < unavailableWorldCount ; i++)												
		{
			TextureButton disableWorld = GetNode<TextureButton>("World" + (totalWorldCount - i));
			Sprite darkFrame = GetNode<Sprite>("World" + (totalWorldCount - i) + "/BorderDark");
			Sprite lockIcon = GetNode<Sprite>("World" + (totalWorldCount - i) + "/Lock");

			disableWorld.Disabled = true;
			darkFrame.Visible = true;
			lockIcon.Visible = true;
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







