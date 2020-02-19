using Godot;
using System;

public class GamePlay : Node2D
{
	GamePlayBL gamePlayBL;
	public override void _Ready()
	{
		gamePlayBL = new GamePlayBL();
		var path = "res://CharSprites/Knight1/Idle/";

		LoadSprite(path);
	}
	public void LoadSprite(string spritePath)
	{
		var dir = new Directory();
		dir.Open(spritePath);

		dir.ListDirBegin();
		var fileName = dir.GetNext();
		string strFileExtention = System.IO.Path.GetExtension(fileName);
		SpriteFrames spriteFrames = new SpriteFrames();
		spriteFrames.AddAnimation("idle");
		int count = 0;

		while (!String.IsNullOrEmpty(fileName))
		{
			GD.Print(fileName);
			fileName = fileName.Replace(strFileExtention, "");
			var sprite = ResourceLoader.Load(spritePath + fileName) as Texture;
			spriteFrames.AddFrame("idle", sprite, 0);
			fileName = dir.GetNext();
			count++;
		}
		AnimatedSprite animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		animatedSprite.SetSpriteFrames(spriteFrames);
		animatedSprite.Play("idle");
		animatedSprite.SetSpeedScale(2);
	}
}

