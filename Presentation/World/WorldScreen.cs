using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class WorldScreen : Node
{
	TextureButton world1Btn, world2Btn, world3Btn, disabler;
	WorldBL worldBl;
	IEnumerable<int> inaccessibleWorlds;

	public override void _Ready()
	{
		//Testing
		Global.StudentId = 29;
		//Testing

		world1Btn = GetNode<TextureButton>("Bg/World1");
		world2Btn = GetNode<TextureButton>("Bg/World2");
		world3Btn = GetNode<TextureButton>("Bg/World3");

		worldBl = new WorldBL();

		disableInaccessibleWorlds();
	}

	private void disableInaccessibleWorlds()
	{
		inaccessibleWorlds = worldBl.getInaccessibleWorlds();

		foreach (int i in inaccessibleWorlds)
		{
			disabler = GetNode<TextureButton>("Bg/World" + i);
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





