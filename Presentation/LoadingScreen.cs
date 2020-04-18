using Godot;
using System;
using System.Collections.Generic;

public class LoadingScreen : Control
{
	public static Control loadingScreenNode;
	Control visualNode;

	string spritePath;
	List<string> animationList;
	AnimatedSprite animatedSprite;

	public static AnimationPlayer animations;

	public override void _Ready()
	{
		visualNode = GetNode<Control>("Visual");
		GD.Print("Visual Node Name: " + visualNode.Name);

		loadingScreenNode = (Control)visualNode.GetParent();
		GD.Print("Loading Screen Node Name: " + loadingScreenNode.Name);

		animations = GetNode<AnimationPlayer>("Visual/Animations");

		spritePath = "res://Assets/LoadingScreen/";
		GD.Print(spritePath);

		animatedSprite = GetNode<AnimatedSprite>("Visual/LoadingCircle");
		GD.Print("Sprite name: " + animatedSprite.Name);

		animationList = new List<string>();
		animationList.Add("loadingCircle");

		Global.LoadSprite(spritePath, animatedSprite, animationList);

		//hide();
	}

	public static void hide()
	{
		animations.Play("Hide");
		loadingScreenNode.Visible = false;
	}


	public static void show()
	{
		loadingScreenNode.Visible = true;

		animations.Play("Show");
	}
}




