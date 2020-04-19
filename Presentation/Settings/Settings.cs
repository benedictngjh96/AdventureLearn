using Godot;
using System;

public class Settings : Node2D
{
	HSlider bgmVol;
	HSlider battleBgmVol;
	HSlider sfxVol;

	public override void _Ready()
	{
		bgmVol = GetNode<HSlider>("Menu/Bgm/BgmVolSlider");
		battleBgmVol = GetNode<HSlider>("Menu/BattleBgm/BattleBgmVolSlider");
		sfxVol = GetNode<HSlider>("Menu/Sfx/SfxVolSlider");
	}

	private void _on_BGMVolSlider_value_changed(float value)
	{
		GD.Print("BGM DB value: " + value);
		DefaultSound.audioPlayer.VolumeDb = value;
		Global.bgmVol = value;
	}

	private void _on_BattleBgmVolSlider_value_changed(float value)
	{
		Global.battleBgmVol = value;
	}

	private void _on_SfxVolSlider_value_changed(float value)
	{
		Global.sfxVol = value;
	}

}



