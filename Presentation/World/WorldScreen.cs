using Godot;
using System;

public class WorldScreen : Node
{
 
	public override void _Ready()
	{
		

	}
	private void _on_Btn_world_pressed()
	{
		Global.worldId = 1;
		GetTree().ChangeScene("res://Presentation/Section/Section.tscn");
	}
}
