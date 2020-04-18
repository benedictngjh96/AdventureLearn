using Godot;
using System;

public class Settings : Node2D
{
	HSlider musicVol;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		musicVol = GetNode<HSlider>("Menu/MusicVol");
	}

	public float getMusicVol()
	{
		return (float)musicVol.Value;
	}

	private void _on_MusicVol_value_changed(float value)
	{
		DefaultSound.audio.VolumeDb = value;
		GD.Print("Music Db: " + value);
	}
}


