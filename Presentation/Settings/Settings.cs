using Godot;
using System;

public class Settings : Node2D
{
	HSlider BgmVol;
	HSlider BattleBgmVol;
	HSlider SfxVol;

	public override void _Ready()
	{
		BgmVol = GetNode<HSlider>("Menu/Bgm/BgmVolSlider");
		BattleBgmVol = GetNode<HSlider>("Menu/BattleBgm/BattleBgmVolSlider");
		SfxVol = GetNode<HSlider>("Menu/Sfx/SfxVolSlider");
	}

	private void _on_BGMVolSlider_value_changed(float value)
	{
		DefaultSound.audioPlayer.VolumeDb = value;
		Global.BgmVol = value;
	}

	private void _on_BattleBgmVolSlider_value_changed(float value)
	{
		Global.BattleBgmVol = value;
	}

	private void _on_SfxVolSlider_value_changed(float value)
	{
		Global.SfxVol = value;
	}

}



