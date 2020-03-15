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





