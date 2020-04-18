using Godot;
using System;

public class DefaultSound : Node2D
{
	static public AudioStreamPlayer audio;
	public override void _Ready()
	{
		audio = GetNode<AudioStreamPlayer>("DefaultBGM");
		audio.VolumeDb = ((float)-30);
		audio.Play();
	}

}
