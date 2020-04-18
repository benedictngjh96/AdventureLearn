using Godot;
using System;

public class LoadTest : Node2D
{

	public override void _Ready()
	{
		
	}
	private void _on_Start_pressed()
	{
		LoadingScreen.show();
	}


	private void _on_End_pressed()
	{
		LoadingScreen.hide();
	}


}

